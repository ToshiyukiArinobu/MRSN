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

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
    public partial class DLY10010 : RibbonWindowViewBase
	{
		/// <summary>
		///　メンバー変数
		/// </summary>
		#region Member
		private enum ChackItems
		{
			TokuisakiSitei,
			SiharaisakiSitei,
			UtiwakeSitei,
			SeikyuusakiSitei,
			SyouhinNm,
			HottiNm,
			TyakutiNm
		}

		private enum OrderNextNumber
		{
			First,
			second,
			third,
			Fourth,
			Fifth
		}


		//表示している表示順序指定数
		public int OutputCount = 0;

		private const string serverNm = @"WIN-F8CCJD7V57B\SQLEXPRESS";
		private const string DataTabelNm = "T01_TRN";
	
		private const string SyaryouTabelNm = "M05_CAR";
		private const string SyasyuTabelNm = "M06_SYA";
		private const string HottiTabelNm = "M08_TIK";
		private const string SyouhinTabelNm = "M09_HIN";
		private const string JyoumuinTabelNm = "M04_DRV";
		private const string TyakutiTabelNm = "M08_TIK";

		private const string TokuisakiTabelNm = "M05_CAR";
		private const string SiharaisakiTabelNm = "M05_CAR";

		/// <summary>
		/// 自社部門マスタ
		/// </summary>
		private const string HouseDepartmentTabelNm = "M71_BUM";
	
		//表示順序指定の各テキストFlag
		public bool OutputImageFlag1 = false;
		public bool OutputImageFlag2 = false;
		public bool OutputImageFlag3 = false;
		public bool OutputImageFlag4 = false;
		public bool OutputImageFlag5 = false;
		/// <summary>
		/// OrederByされる項目数
		/// </summary>
		private int OrederByCount = 0;
		/// <summary>
		/// OrderByのクエリ
		/// </summary>
		private string OrderQuery = "";

		//ピックアップ指定の表示フラグ
		public bool PickUpFlag = false;
		public bool PickUpFlag_Ribbon = false;

		//ピックアップ指定の切り替えフラグ
		public bool PickUpChangeFlag = false;
		public bool PickUpChangeFlag_Ribbon = false;

		//ピックアップVer3の切り替えフラグ
		public bool PickUp3ComboFlag = false;

		public class Printf
		{
			public int Nember { get; set; }
			public string Title { get; set; }
		}

		#endregion

        #region BindingMember

        private DataSet dsMain = new DataSet();
        private DataTable _dUriageData = null;
        private DataTable _dLogData = null;
        private DataTable _dKeihiData = null;

        public DataTable DUriageData
        {
            get
            {
                return this._dUriageData;
            }
            set
            {
                this._dUriageData = value;
                NotifyPropertyChanged();
            }
        }
        public DataTable DLogData
        {
            get
            {
                return this._dLogData;
            }
            set
            {
                this._dLogData = value;
                NotifyPropertyChanged();
            }
        }
        public DataTable DKeihiData
        {
            get
            {
                return this._dKeihiData;
            }
            set
            {
                this._dKeihiData = value;
                NotifyPropertyChanged();
            }
        }

        private int isDetailsNumber = 0;
        public int IsDetailsNumber
        {
            get
            {
                return this.isDetailsNumber;
            }
            set
            {
                this.isDetailsNumber = value;
                NotifyPropertyChanged();
            }
        }

        private int isLineNumber = 0;
        public int IsLineNumber
        {
            get
            {
                return isLineNumber;
            }
            set
            {
                this.isLineNumber = value;
                NotifyPropertyChanged();
            }
        }

        private string driverID;
        public string DriverID
        {
            get
            {
                return this.driverID;
            }
            set
            {
                this.driverID = value;
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

        private string _message = string.Empty;
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
                NotifyPropertyChanged();
            }
        }

        private int? _得意先ID = null;
        [BindableAttribute(true)]
        public int? 得意先ID
        {
            set
            {
                _得意先ID = value;
                NotifyPropertyChanged();
            }
            get { return _得意先ID; }
        }

        private int? _請求内訳ID = null;
        [BindableAttribute(true)]
        public int? 請求内訳ID
        {
            set
            {
                _請求内訳ID = value;
                NotifyPropertyChanged();
            }
            get { return _請求内訳ID; }
        }

        private int? _支払先ID = null;
        [BindableAttribute(true)]
        public int? 支払先ID
        {
            set
            {
                _支払先ID = value;
                NotifyPropertyChanged();
            }
            get { return _支払先ID; }
        }

        private DateTime? _請求日付From = null;
        [BindableAttribute(true)]
        public DateTime? 請求日付From
        {
            set
            {
                _請求日付From = value;
                NotifyPropertyChanged();
            }
            get { return _請求日付From; }
        }

        private DateTime? _請求日付To = null;
        [BindableAttribute(true)]
        public DateTime? 請求日付To
        {
            set
            {
                _請求日付To = value;
                NotifyPropertyChanged();
            }
            get { return _請求日付To; }
        }

        private DateTime? _支払日付From = null;
        [BindableAttribute(true)]
        public DateTime? 支払日付From
        {
            set
            {
                _支払日付From = value;
                NotifyPropertyChanged();
            }
            get { return _支払日付From; }
        }

        private DateTime? _支払日付To = null;
        [BindableAttribute(true)]
        public DateTime? 支払日付To
        {
            set
            {
                _支払日付To = value;
                NotifyPropertyChanged();
            }
            get { return _支払日付To; }
        }

        private DateTime? _配送日付From = null;
        [BindableAttribute(true)]
        public DateTime? 配送日付From
        {
            set
            {
                _配送日付From = value;
                NotifyPropertyChanged();
            }
            get { return _配送日付From; }
        }

        private DateTime? _配送日付To = null;
        [BindableAttribute(true)]
        public DateTime? 配送日付To
        {
            set
            {
                _配送日付To = value;
                NotifyPropertyChanged();
            }
            get { return _配送日付To; }
        }

        private int? _自社部門ID = null;
        [BindableAttribute(true)]
        public int? 自社部門ID
        {
            set
            {
                _自社部門ID = value;
                NotifyPropertyChanged();
            }
            get { return _自社部門ID; }
        }

        private string _発地名 = "";
        [BindableAttribute(true)]
        public string 発地名
        {
            set
            {
                _発地名 = value;
                NotifyPropertyChanged();
            }
            get { return _発地名; }
        }
        private string _着地名 = "";
        [BindableAttribute(true)]
        public string 着地名
        {
            set
            {
                _着地名 = value;
                NotifyPropertyChanged();
            }
            get { return _着地名; }
        }

        private string _商品名 = "";
        [BindableAttribute(true)]
        public string 商品名
        {
            set
            {
                _商品名 = value;
                NotifyPropertyChanged();
            }
            get { return _商品名; }
        }

        private string _請求摘要 = "";
        [BindableAttribute(true)]
        public string 請求摘要
        {
            set
            {
                _請求摘要 = value;
                NotifyPropertyChanged();
            }
            get { return _請求摘要; }
        }

        private string _社内備考 = "";
        [BindableAttribute(true)]
        public string 社内備考
        {
            set
            {
                _社内備考 = value;
                NotifyPropertyChanged();
            }
            get { return _社内備考; }
        }
        
        private int? _売上未定区分 = null;
        [BindableAttribute(true)]
        public int? 売上未定区分
        {
            set
            {
                _売上未定区分 = value;
                NotifyPropertyChanged();
            }
            get { return _売上未定区分; }
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
        private DataTable makeTable = new DataTable();
        public DataTable MakeTable
        {
            get
            {
                return this.makeTable;
            }
            set
            {
                this.makeTable = value;
                NotifyPropertyChanged();
            }
        }
   
        #endregion

		/// <summary>
		/// 売上明細問合せ
		/// </summary>
		public DLY10010()
		{
			InitializeComponent();		
		}

		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
			
			//MasterTabel Sarch
			//TokuisakiSitei.KeyCd = TokuisakiSitei.FirstText_Text;
			
			//ComboBoxに値を設定する
            GetComboBoxItems();

			GetMasterTabel();
		}

		//<summary>
		//キーボードで押下されたkeyはここで拾えます
		//PreviewKeyDown="Window_PreviewKeyDown"をxamlのwindowプロパティに追加
		//</summary>
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Control s = e.Source as Control;
			switch (e.Key)
			{
			case Key.F1:
				//	ボタンのクリック時のイベントを呼び出します
				this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
				break;
			case Key.F5:
				this.CsvSyuturyoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CsvSyuturyoku));
				break;
			case Key.F8:
				this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
				break;
			}

		}


		/// <summary>
		/// コンボボックスのアイテムをDBから取得
		/// </summary>
		private void GetComboBoxItems()
		{
			//自社部門IDの取得
            //OleDbDataAdapter CarAdapter = new OleDbDataAdapter("Select 自社部門ID,自社部門名 From " + HouseDepartmentTabelNm, objConn);
            //CarAdapter.Fill(DataSt, HouseDepartmentTabelNm);
            //CarAdapter.FillSchema(DataSt, SchemaType.Source, HouseDepartmentTabelNm);
            //DataTable BumTb = DataSt.Tables[HouseDepartmentTabelNm];
            //BumTb.Rows.Add(0, "(全部門検索対象)");

            //Framework.Windows.Controls.UcLabelComboBox cCombo = new Framework.Windows.Controls.UcLabelComboBox();
            //DataTable SortTabel = cCombo.TableSort(BumTb,"自社部門ID");

            //this.BumonSitei.Combo_ItemsSource = SortTabel;

		}

		/// <summary>
		/// ピックアップ指定のGridの読み込み
		/// </summary>
		private void GetMasterTabel()
		{
			try
			{
                //OleDbDataAdapter CarAdapter = new OleDbDataAdapter("Select 車輌ID,車輌番号 From " + SyaryouTabelNm, objConn);
                //CarAdapter.Fill(DataSt, SyaryouTabelNm);
                //CarAdapter.FillSchema(DataSt, SchemaType.Source, SyaryouTabelNm);
                //DataTable CarTb = DataSt.Tables[SyaryouTabelNm];

                //this.CarDataGrid.ItemSources = CarTb;

                //OleDbDataAdapter SyaAdapter = new OleDbDataAdapter("Select 車種ID,車種名 From " + SyasyuTabelNm, objConn);
                //SyaAdapter.Fill(DataSt, SyasyuTabelNm);
                //SyaAdapter.FillSchema(DataSt, SchemaType.Source, SyasyuTabelNm);
                //DataTable SyaTb = DataSt.Tables[SyasyuTabelNm];

                //this.SyaDataGrid.ItemSources = SyaTb;

                //OleDbDataAdapter TikAdapter = new OleDbDataAdapter("Select 発着地ID,発着地名 From " + HottiTabelNm, objConn);
                //TikAdapter.Fill(DataSt, HottiTabelNm);
                //TikAdapter.FillSchema(DataSt, SchemaType.Source, HottiTabelNm);
                //DataTable TikTb = DataSt.Tables[HottiTabelNm];

                //this.TikDataGrid.ItemSources = TikTb;

                //OleDbDataAdapter HinAdapter = new OleDbDataAdapter("Select 商品ID,商品名 From " + SyouhinTabelNm, objConn);
                //HinAdapter.Fill(DataSt, SyouhinTabelNm);
                //HinAdapter.FillSchema(DataSt, SchemaType.Source, SyouhinTabelNm);
                //DataTable HinTb = DataSt.Tables[SyouhinTabelNm];

                //this.HinDataGrid.ItemSources = HinTb;

                //OleDbDataAdapter DrvAdapter = new OleDbDataAdapter("Select 乗務員ID,乗務員名 From " + JyoumuinTabelNm, objConn);
                //DrvAdapter.Fill(DataSt, JyoumuinTabelNm);
                //DrvAdapter.FillSchema(DataSt, SchemaType.Source, JyoumuinTabelNm);
                //DataTable DrvTb = DataSt.Tables[JyoumuinTabelNm];

                //this.DrvDataGrid.ItemSources = DrvTb;
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

        public override void OnReveivedError(CommunicationObject message)
        {
            //base.OnReveivedError(message);
            //this.Message = base.ErrorMesasge;
        }

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
			case "":
                this.MakeTable = data as DataTable;
				break;
			}

            this.Message = string.Format("取得データ {0} 件", this.MakeTable.Rows.Count);

        }

		/// <summary>
		/// 検索ボタン クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{		
            //GetOrderCount(OrderNextNumber.First,0);

			//SearchColumn(ControlItemCheak(), GetDaySelection(), GetClassification());

            KeyPropertyInput();

            CommunicationObject com;
            com = new CommunicationObject(MessageType.RequestData, "DLY10010",
                new object[] { _得意先ID,_請求内訳ID,_支払先ID,_請求日付From,_請求日付To,_支払日付From,_支払日付To,_配送日付From, _配送日付To,_自社部門ID,_発地名,_着地名,_商品名,_請求摘要,_社内備考,_売上未定区分});
			
			base.SendRequest(com);
		}

		/// <summary>
		/// 検索する日付の取得
		/// </summary>
		/// <returns></returns>
		private string GetDaySelection()
		{
			string Day ="";

			switch (this.DaySelectionComboBox.SelectedIndex)
			{
			case 0:
				Day = "請求日付";
				break;
			case 1:
				Day = "支払日付";
				break;
			case 2:
				Day = "配送日付";
				break;
			}

			return Day;
		}

		/// <summary>
		/// 未定区分のクエリを作成する
		/// </summary>
		/// <returns>整形後のクエリ</returns>
		private string GetClassification()
		{
			string Query = "";

			switch (this.MiteiKubun.SelectedIndex)
			{
			//未定のみ
			case 1:
				Query = " AND (T01_TRN.売上未定区分 = '1' OR T01_TRN.支払未定区分 = '1' ) ";
				break;
			//確定のみ
			case 2:
				Query = " AND (T01_TRN.売上未定区分 = '0' AND T01_TRN.支払未定区分 = '0' ) ";
				break;
			//金額が未入力のみ
			case 3:
				Query = " AND T01_TRN.売上金額 = '0' ";
				break;

			}

			return Query;
		}

		/// <summary>
		/// Query作成
		/// </summary>
		/// <returns>作成後のクエリ</returns>
		private string ControlItemCheak()
		{
			//整形後に格納する変数
			string Query = "";

			Query = TagetQueryAddAnd(Query, TokuisakiSiteiControlCheck());
			Query = TagetQueryAddAnd(Query, SiharaisakiControlCheck());
			Query = TagetQueryAddAnd(Query, UtiwakeSiteiControlCheck());
		//	Query = TagetQueryAddAnd(Query, SeikyuusakiSiteiControlCheck());

			if (this.SyouhinNm.Text != "")
			{
                Query = string.Format(Query + " AND 商品名 LIKE '%{0}%'", this.SyouhinNm.Text);
			}

            if (this.HottiNm.Text != "")
			{
                Query = string.Format(Query + " AND 発地名 LIKE '%{0}%'", this.HottiNm.Text);
			}

            if (this.TyakutiNm.Text != "")
			{
                Query = string.Format(Query + " AND 着地名 LIKE '%{0}%'", this.TyakutiNm.Text);
			}

			if (Query != "")
			{
				Query = " AND " + Query;
			}

			return Query;
		}

		/// <summary>
		/// クエリにANDを加える
		/// </summary>
		/// <param name="Query"></param>
		/// <param name="TempQuery"></param>
		/// <returns></returns>
		private string TagetQueryAddAnd(string Query, string TempQuery)
		{
			if (TempQuery != "")
			{
				if (Query != "")
				{
					Query = Query +" AND " + TempQuery;
				}
				else
				{
					Query = TempQuery;
				}
			}

			return Query;
		}


		/// <summary>
		/// 昇順降順の項目数の獲得
		/// </summary>
		/// <param name="Index">チェックする項目番号</param>
		/// <param name="OrderByCount">値が入っている項目数</param>
		private void GetOrderCount(OrderNextNumber Index,int OrderByCount)
		{

			bool NotNullCheck = false;
	
			OrderNextNumber NextNember = OrderNextNumber.First;
			
			switch (Index)
			{
			case OrderNextNumber.First:
				if (HyoujiText1.Text != "")
				{
					OrederByCount++;
					NotNullCheck = true;
					NextNember = OrderNextNumber.second;
				}
				break;
			case OrderNextNumber.second:
				if (HyoujiText2.Text != "")
				{
					OrederByCount++;
					NotNullCheck = true;
					NextNember = OrderNextNumber.third;
				}
				break;
			case OrderNextNumber.third:

				if (HyoujiText3.Text != "")
				{
					OrederByCount++;
					NotNullCheck = true;
					NextNember = OrderNextNumber.Fourth;
				}

				break;
			case OrderNextNumber.Fourth:
				if (HyoujiText4.Text != "")
				{
					OrederByCount++;
					NotNullCheck = true;
					NextNember = OrderNextNumber.Fifth;
				}
				break;
			case OrderNextNumber.Fifth:
				if (HyoujiText5.Text != "")
				{
					OrederByCount++;
					NotNullCheck = true;
				}
				break;
			}

			if (NotNullCheck == true)
			{
				GetOrderCount(NextNember,OrederByCount);
			}
			if (OrderByCount != 0)
			{
				OrderQuery = GetOrderQuery(1, OrederByCount, "");
			}
		}
		
		/// <summary>
		/// 昇順降順のクエリを作成
		/// </summary>
		/// <param name="NextIndex">次の項目</param>
		/// <param name="OrderItemCount">項目数</param>
		/// <param name="OrderByQuery">クエリ</param>
		/// <returns></returns>
		private string GetOrderQuery(int NextIndex, int OrderItemCount, string OrderByQuery)
		{
			string subQuery = "";

			if (NextIndex <= OrderItemCount)
			{
				switch (NextIndex)
				{
				case 1:

					subQuery = " ORDER BY " + HyoujiText1.Text;

					if (OutputImageFlag1 == false)
					{
						subQuery = subQuery + " DESC";
					}
					OrderByQuery = subQuery;
					break;
				case 2:

					subQuery = OrderByQuery + " , " + HyoujiText2.Text;

					if (OutputImageFlag2 == false)
					{
						subQuery = subQuery + " DESC";
					}
					OrderByQuery = subQuery;

					break;
				case 3:
					subQuery = OrderByQuery + " , " + HyoujiText3.Text;

					if (OutputImageFlag3 == false)
					{
						subQuery = subQuery + " DESC ";
					}
					OrderByQuery = subQuery;

					break;
				case 4:

					subQuery = OrderByQuery + " , " + HyoujiText4.Text;

					if (OutputImageFlag4 == false)
					{
						subQuery = subQuery + " DESC ";
					}
					OrderByQuery = subQuery;
					break;
				case 5:

					subQuery = OrderByQuery + " , " + HyoujiText5.Text;

					if (OutputImageFlag5 == false)
					{
						subQuery = subQuery + " DESC ";
					}
					OrderByQuery = subQuery;
					break;
				}
				
			}

			if (NextIndex < OrderItemCount)
			{
				GetOrderQuery(NextIndex + 1, OrderItemCount, OrderByQuery);
			}
	
			return OrderByQuery;
		}
		
		/// <summary>
		/// 検索条件における得意先指定の整形
		/// </summary>
		/// <returns>整形後のクエリ文</returns>
		private string TokuisakiSiteiControlCheck()
		{
			string Query = "";

            if (this.TokuisakiSitei.Text1 != "")
			{
                Query = string.Format(" 得意先ID = {0}", this.TokuisakiSitei.Text1);
			}
		
			return Query;
		}

		/// <summary>
		/// 検索条件における支払先指定の整形
		/// </summary>
		/// <returns>整形後のクエリ文</returns>
		private string SiharaisakiControlCheck()
		{
			string Query = "";

			if (this.SiharaisakiSitei.Text1 != "")
			{
                Query = string.Format(" 支払先ID = {0}", this.SiharaisakiSitei.Text1);
			}

			return Query;
		}

		/// <summary>
		/// 検索条件における内訳先指定整形
		/// </summary>
		/// <returns>整形後のクエリ文</returns>
		private string UtiwakeSiteiControlCheck()
		{
			string Query = "";

			if (this.UtiwakeSitei.Text1 != "")
			{	
				Query = string.Format(" 請求内訳ID = {0}", this.UtiwakeSitei.Text1);
			}
			return Query;
		}

		/// <summary>
		/// 検索条件における請求先指定整形
		/// </summary>
		/// <returns>整形後のクエリ文</returns>
		private string SeikyuusakiSiteiControlCheck()
		{
			string Query = "";

		//	if (this.SeikyuuSaki.FirstText_Text != "")
		//	{

		//			Query = string.Format(" 請求先ID = {0}", this.SeikyuuSaki.FirstText_Text);
		//	}

		return Query;
		}

		/// <summary>
		/// データを検索する
		/// </summary>
		/// <param name="Query">整形後のQuery条検文</param>
		/// <param name="DaySelect">検索する日付</param>
		/// <param name="ClassificationSerect">未定区分クエリ</param>
		private void SearchColumn(string Query, string DaySelect, string ClassificationSerect)
		{
            ////データテーブル初期化
            //if (DataSt.Tables[DataTabelNm] != null)
            //{
            //    DataSt.Tables[DataTabelNm].Clear();
            //}

            // 表示順
            string SqlOrder = this.HyoujiText1.Text + (OutputImageFlag1 == false ? (this.HyoujiText1.Text != string.Empty ? " DESC" : string.Empty) : string.Empty)
                    + (this.HyoujiText2.Text != string.Empty ? " ," + this.HyoujiText2.Text : string.Empty) + (OutputImageFlag2 == false ? (this.HyoujiText2.Text != string.Empty ? " DESC" : string.Empty) : string.Empty)
                    + (this.HyoujiText3.Text != string.Empty ? " ," + this.HyoujiText3.Text : string.Empty) + (OutputImageFlag3 == false ? (this.HyoujiText3.Text != string.Empty ? " DESC" : string.Empty) : string.Empty)
                    + (this.HyoujiText4.Text != string.Empty ? " ," + this.HyoujiText4.Text : string.Empty) + (OutputImageFlag4 == false ? (this.HyoujiText4.Text != string.Empty ? " DESC" : string.Empty) : string.Empty)
                    + (this.HyoujiText5.Text != string.Empty ? " ," + this.HyoujiText5.Text : string.Empty) + (OutputImageFlag5 == false ? (this.HyoujiText5.Text != string.Empty ? " DESC" : string.Empty) : string.Empty);

            string DataStSql = string.Format(@"
SELECT
	T01_TRN.明細番号,
	T01_TRN.明細行,
	T01_TRN.請求日付,
	T01_TRN.支払日付,
	T01_TRN.配送日付,
	T01_TRN.配送時間,
	T01_TRN.得意先ID,
	M01_TOK.略称名 AS 得意先略称名,
	T01_TRN.請求内訳ID,
	M10_UHK.請求内訳名,
	T01_TRN.車輌ID,
	T01_TRN.車輌番号,
	T01_TRN.車種ID,
	M06_SYA.車種名,
	T01_TRN.支払先ID,
	M01_TOK_2.略称名 AS 支払先略称名,
	T01_TRN.乗務員ID,
	M04_DRV.乗務員名,
	T01_TRN.支払先名２次,
	T01_TRN.実運送乗務員,
	T01_TRN.乗務員連絡先,
	T01_TRN.数量,
	T01_TRN.単位,
	T01_TRN.重量,
	T01_TRN.走行ＫＭ,
	T01_TRN.実車ＫＭ,
	T01_TRN.売上単価,
	T01_TRN.売上金額,
	T01_TRN.通行料,
	T01_TRN.請求割増１,
	T01_TRN.請求割増２,
	T01_TRN.請求消費税,
	T01_TRN.支払単価,
	T01_TRN.支払金額,
	T01_TRN.支払通行料,
	T01_TRN.支払割増１,
	T01_TRN.支払割増２,
	T01_TRN.支払消費税,
	T01_TRN.社内区分,
	T01_TRN.請求税区分,
	T01_TRN.支払税区分,
	T01_TRN.売上未定区分,
	T01_TRN.支払未定区分,
	T01_TRN.商品名,
	T01_TRN.発地名,
	T01_TRN.着地名,
	T01_TRN.請求摘要,
	T01_TRN.社内備考
FROM
	T01_TRN
	LEFT JOIN M01_TOK ON T01_TRN.得意先ID = M01_TOK.取引先ID
	LEFT JOIN M10_UHK ON T01_TRN.請求内訳ID = M10_UHK.請求内訳ID
	LEFT JOIN M06_SYA ON T01_TRN.車種ID = M06_SYA.車種ID
	LEFT JOIN M01_TOK AS M01_TOK_2 ON T01_TRN.支払先ID = M01_TOK.取引先ID
	LEFT JOIN M04_DRV ON T01_TRN.乗務員ID = M04_DRV.乗務員ID
WHERE
	({0} IS NULL OR T01_TRN.得意先ID = {0})
AND ({1} IS NULL OR T01_TRN.請求内訳ID = {1})
AND ({2} IS NULL OR T01_TRN.支払先ID = {2})
AND (({4} IS NULL AND {5} IS NULL) OR (CONVERT(VARCHAR, T01_TRN.{3}, 112) BETWEEN '{4}' AND '{5}'))
AND	T01_TRN.発地名 LIKE '%{6}%'
AND T01_TRN.着地名 LIKE '%{7}%'
AND	T01_TRN.商品名 LIKE '%{8}%'
AND (T01_TRN.請求摘要 IS NULL OR T01_TRN.請求摘要 LIKE '%{9}%')
AND (T01_TRN.社内備考 IS NULL OR T01_TRN.社内備考 LIKE '%{10}%')
AND ({11} = 0 OR T01_TRN.自社部門ID = {11})
{12}
ORDER BY {13}
;"
                , this.TokuisakiSitei.Text1 == string.Empty ? "NULL" : this.TokuisakiSitei.Text1
                , this.UtiwakeSitei.Text1 == string.Empty ? "NULL" : this.UtiwakeSitei.Text1
                , this.SiharaisakiSitei.Text1 == string.Empty ? "NULL" : this.SiharaisakiSitei.Text1
                , this.DaySelectionComboBox.Text
                , this.SearchDay.FirstDate_SelectedDate == null ? "NULL" : string.Format("{0:yyyyMMdd}", this.SearchDay.FirstDate_SelectedDate)
                , this.SearchDay.SecondDate_SelectedDate == null ? "NULL" : string.Format("{0:yyyyMMdd}", this.SearchDay.SecondDate_SelectedDate)
                , this.HottiNm.Text
                , this.TyakutiNm.Text
                , this.SyouhinNm.Text
                , this.SeikyuTekiyou.Text
                , this.SyanaiBikou.Text
				, this.BumonSitei.Combo_SelectedValue
				, ClassificationSerect
                , SqlOrder == string.Empty ? " T01_TRN.明細番号, T01_TRN.明細行" : SqlOrder
                );
      



            //OleDbCommand selectCmd = new OleDbCommand();
            //selectCmd.Connection = objConn;
            //selectCmd.CommandText = DataStSql;

            ////SqlDataAdapter adapter = new SqlDataAdapter(DataSt, objConn);
            //OleDbDataAdapter adapter = new OleDbDataAdapter();
            //adapter.SelectCommand = selectCmd;

            //adapter.FillSchema(DataSt, SchemaType.Source, DataTabelNm);
            //adapter.Fill(DataSt, DataTabelNm);

            ////DataGridに表示対象のDataTabelとAdapterを渡す
            //this.cDataGrid.ItemSources = DataSt.Tables[DataTabelNm];
		}

        /// <summary>
        /// データアクセスのKeyとなる項目を格納していく
        /// </summary>
        private void KeyPropertyInput()
        {
            //this.得意先ID = string.IsNullOrEmpty(this.TokuisakiSitei.FirstText_Text) ? null as int? : int.Parse(this.TokuisakiSitei.FirstText_Text);
            //this.請求内訳ID = string.IsNullOrEmpty(this.SiharaisakiSitei.FirstText_Text) ? null as int? : int.Parse(this.SiharaisakiSitei.FirstText_Text);
            //this.支払先ID = string.IsNullOrEmpty(this.UtiwakeSitei.FirstText_Text) ? null as int? : int.Parse(this.UtiwakeSitei.FirstText_Text);
            
            TargetDayInitializeComponent();

            switch (DaySelectionComboBox.SelectedIndex)
            {
                //請求日付
                case 1:
                    this.請求日付From = this.SearchDay.FirstDate_SelectedDate == null ? null : this.SearchDay.FirstDate_SelectedDate;
                    this.請求日付To = this.SearchDay.SecondDate_SelectedDate == null ? null : this.SearchDay.SecondDate_SelectedDate;
                    break;
                    //支払日付
                case 2:
                    this.支払日付From = this.SearchDay.FirstDate_SelectedDate == null ? null : this.SearchDay.FirstDate_SelectedDate;
                    this.支払日付To = this.SearchDay.SecondDate_SelectedDate == null ? null : this.SearchDay.SecondDate_SelectedDate;             
                    break;
                    //配送日付
                case 3:
                    this.配送日付From = this.SearchDay.FirstDate_SelectedDate == null ? null : this.SearchDay.FirstDate_SelectedDate;
                    this.配送日付To = this.SearchDay.SecondDate_SelectedDate == null ? null : this.SearchDay.SecondDate_SelectedDate;     
                    break;
            }

            this._自社部門ID = BumonSitei.Combo_SelectedValue as int?;
            this._売上未定区分 = this.MiteiKubun.SelectedIndex;
            //this._発地名 = this.HottiNm.FirstText_Text;
            //this._着地名 = this.TyakutiNm.FirstText_Text;
            //this._商品名 = this.SyouhinNm.FirstText_Text;
            //this._請求摘要 = this.SeikyuTekiyou.FirstText_Text;
            //this._社内備考 = this.SyanaiBikou.FirstText_Text;
            //this._売上未定区分 = this.MiteiKubun.SelectedIndex;

        }

        /// <summary>
        /// 日付プロパティの初期化
        /// </summary>
        private void TargetDayInitializeComponent()
        {
            this.請求日付From = null;
            this.請求日付To = null;
            this.支払日付From = null;
            this.支払日付To = null;
            this.配送日付From = null;
            this.配送日付To = null;
        }


		/// <summary>
		/// 表示順序指定ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiJyun_Click_1(object sender, RoutedEventArgs e)
		{
			this.HyoujiList.Visibility = Visibility.Visible;
			this.HyoujiButtonGrid.Visibility = Visibility.Visible;
			this.HyoujiTojiru.Visibility = Visibility.Visible;
			this.HyoujiCria.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// 表示閉じる押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiTojiru_Click_1(object sender, RoutedEventArgs e)
		{

			this.HyoujiList.Visibility = Visibility.Collapsed;
			this.HyoujiButtonGrid.Visibility = Visibility.Collapsed;
			this.HyoujiTojiru.Visibility = Visibility.Collapsed;
			this.HyoujiCria.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Listをダブルクリック時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiList_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
		{
			string selectItem = string.Empty;

			//選択項目を取得
			foreach (ListBoxItem aitem in HyoujiList.SelectedItems)
			{
				selectItem += aitem.Content;
			}

			//順に格納
			switch (OutputCount)
			{
			case 0:
				this.HyoujiText1.Text = selectItem;
				OutputCount++;
				break;
			case 1:
				this.HyoujiText2.Text = selectItem;
				OutputCount++;
				break;
			case 2:
				this.HyoujiText3.Text = selectItem;
				OutputCount++;
				break;
			case 3:
				this.HyoujiText4.Text = selectItem;
				OutputCount++;
				break;
			case 4:
				this.HyoujiText5.Text = selectItem;
				OutputCount = 0;
				break;
			}
		}

		/// <summary>
		/// 表示イメージ1押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiImage1_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			OrderImageChange(1);
		}

		/// <summary>
		/// 表示イメージ2押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiImage2_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			OrderImageChange(2);
		}

		/// <summary>
		/// 表示イメージ3押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiImage3_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			OrderImageChange(3);
		}

		/// <summary>
		/// 表示イメージ4押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiImage4_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			OrderImageChange(4);
		}

		/// <summary>
		/// 表示イメージ5押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiImage5_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			OrderImageChange(5);
		}

		///<summary>
		///昇順･降順制御
		///引数1:押下されたボタン番号
		///</summary>
		private void OrderImageChange(int index)
		{
			switch (index)
			{
			case 1:
				//ボタン1
				if (this.OutputImageFlag1 == false)
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
					this.HyoujiImage1.Source = bi2;
					this.OutputImageFlag1 = true;
				}
				else
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
					this.HyoujiImage1.Source = bi2;
					this.OutputImageFlag1 = false;
				}

				break;
			case 2:
				//ボタン2
				if (this.OutputImageFlag2 == false)
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
					this.HyoujiImage2.Source = bi2;
					this.OutputImageFlag2 = true;
				}
				else
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
					this.HyoujiImage2.Source = bi2;
					this.OutputImageFlag2 = false;
				}

				break;
			case 3:
				//ボタン3
				if (this.OutputImageFlag3 == false)
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
					this.HyoujiImage3.Source = bi2;
					this.OutputImageFlag3 = true;
				}
				else
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
					this.HyoujiImage3.Source = bi2;
					this.OutputImageFlag3 = false;
				}
				break;
			case 4:
				//ボタン4
				if (this.OutputImageFlag4 == false)
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
					this.HyoujiImage4.Source = bi2;
					this.OutputImageFlag4 = true;
				}
				else
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
					this.HyoujiImage4.Source = bi2;
					this.OutputImageFlag4 = false;
				}
				break;
			case 5:
				//ボタン4
				if (this.OutputImageFlag5 == false)
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
					this.HyoujiImage5.Source = bi2;
					this.OutputImageFlag5 = true;
				}
				else
				{
					BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
					this.HyoujiImage5.Source = bi2;
					this.OutputImageFlag5 = false;
				}
				break;
			}
		}

		/// <summary>
		/// クリアボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiCria_Click_1(object sender, RoutedEventArgs e)
		{
			this.HyoujiText1.Text = "";
			this.HyoujiText2.Text = "";
			this.HyoujiText3.Text = "";
			this.HyoujiText4.Text = "";
			this.HyoujiText5.Text = "";
			this.OutputImageFlag1 = false;
			this.OutputImageFlag2 = false;
			this.OutputImageFlag3 = false;
			this.OutputImageFlag4 = false;
			this.OutputImageFlag5 = false;
			TextChange();
			this.OutputCount = 0;
		}

		/// <summary>
		/// 順序表示テキスト変化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HyoujiText1_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			TextChange();
		}

		/// <summary>
		/// 表示順の文字変更時処理
		/// </summary>
		private void TextChange()
		{
			//テキスト1
			if (this.HyoujiText1.Text == "" && this.OutputImageFlag1 == false)
			{
				HyoujiImage1.Visibility = Visibility.Collapsed;
			}
			else
			{
				HyoujiImage1.Visibility = Visibility.Visible;
			}

			//テキスト2
			if (this.HyoujiText2.Text == "" && this.OutputImageFlag2 == false)
			{
				HyoujiImage2.Visibility = Visibility.Collapsed;
			}
			else
			{
				HyoujiImage2.Visibility = Visibility.Visible;
			}


			//テキスト3
			if (this.HyoujiText3.Text == "" && this.OutputImageFlag3 == false)
			{
				HyoujiImage3.Visibility = Visibility.Collapsed;
			}
			else
			{
				HyoujiImage3.Visibility = Visibility.Visible;
			}

			//テキスト3
			if (this.HyoujiText4.Text == "" && this.OutputImageFlag4 == false)
			{
				HyoujiImage4.Visibility = Visibility.Collapsed;
			}
			else
			{
				HyoujiImage4.Visibility = Visibility.Visible;
			}

			//テキスト3
			if (this.HyoujiText5.Text == "" && this.OutputImageFlag5 == false)
			{
				HyoujiImage5.Visibility = Visibility.Collapsed;
			}
			else
			{
				HyoujiImage5.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// PickUpGridの非表示
		/// </summary>
		private void PickUpGridVisibilityChange()
		{
			this.DataGridPickUp_Tokuisaki.Visibility = Visibility.Collapsed;
			this.DataGridPickUp_SeikyuSaki.Visibility = Visibility.Collapsed;
			this.DataGridPickUp_Siharaisaki.Visibility = Visibility.Collapsed;
			this.DrvDataGrid.Visibility = Visibility.Collapsed;
			this.CarDataGrid.Visibility = Visibility.Collapsed;
			this.SyaDataGrid.Visibility = Visibility.Collapsed;
			this.TikDataGrid.Visibility = Visibility.Collapsed;
			this.DataGridPickUp_Tyakuti.Visibility = Visibility.Collapsed;
			this.HinDataGrid.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Ribbonピックアップタブの　　反映ボタン押下時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonTabPickUP_HaneiButton_Click_1(object sender, RoutedEventArgs e)
		{
			this.PickUpFlag_Ribbon = false;
		}

		/// <summary>
		/// Ribbon条件指定ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PickUpRibbon_Click_1(object sender, RoutedEventArgs e)
		{
			if (this.PickUpFlag_Ribbon == false)
			{
				this.Header.Visibility = Visibility.Collapsed;
				BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/\Input.png", UriKind.Relative));
				this.PickUpFlag_Ribbon = true;
			}
			else{

				this.Header.Visibility = Visibility.Visible;
				BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/\InputTure.png", UriKind.Relative));

				this.PickUpFlag_Ribbon = false;
			}

		}
		
		/// <summary>
		/// ピックアップバージョン3Buttonクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PickUpFlagButton3_Click_1(object sender, RoutedEventArgs e)
		{
			if (this.PickUpFlag_Ribbon == false && this.PickUpChangeFlag_Ribbon == false)
			{
				this.PickUpJyoukenBorder3.Visibility = Visibility.Visible;
				this.PickUpDataGridGrid3.Visibility = Visibility.Visible;
				this.PickUpFlagButton3.Content = "【Close】";
				this.PickUpChangeFlag_Ribbon = true;
			}
			else if ((this.PickUpFlag_Ribbon == false && this.PickUpChangeFlag_Ribbon == true))
			{
				this.PickUpJyoukenBorder3.Visibility = Visibility.Hidden;
				this.PickUpDataGridGrid3.Visibility = Visibility.Hidden;
				this.PickUpFlagButton3.Content = "ピックアップ指定";
				this.PickUpChangeFlag_Ribbon = false;
			}
		}

		/// <summary>
		/// バージョン3　ComboBoxでの表示の切り替え
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PickUplan3ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
		{
			if (PickUpChangeFlag_Ribbon == true)
			{

				PickUpGridVisibilityChange();
				
				int Index = this.PickUplan3ComboBox.SelectedIndex;
				switch (Index)
				{
				case 0:
					this.DataGridPickUp_Tokuisaki.Visibility = Visibility.Visible;
					break;
				case 1:
					this.DataGridPickUp_SeikyuSaki.Visibility = Visibility.Visible;
					break;
				case 2:
					this.DataGridPickUp_Siharaisaki.Visibility = Visibility.Visible;
					break;
				case 3:
					this.DrvDataGrid.Visibility = Visibility.Visible;
					break;
				case 4:
					this.CarDataGrid.Visibility = Visibility.Visible;
					break;
				case 5:
					this.SyaDataGrid.Visibility = Visibility.Visible;
					break;
				case 6:
					this.TikDataGrid.Visibility = Visibility.Visible;
					break;
				case 7:
					this.DataGridPickUp_Tyakuti.Visibility = Visibility.Visible;
					break;
				case 8:
					this.HinDataGrid.Visibility = Visibility.Visible;
					break;
				}
			}
		}

		/// <summary>
		/// F1　リボン検索ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
		{
			SCH04010 page = new SCH04010();
			page.Show();
		}

		/// <summary>
		/// F5 リボン　CSV出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CsvSyuturyoku_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("CSV出力ボタンが押されました。");
		}

		/// <summary>
		/// F8 リボン　印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Insatu_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("印刷ボタンが押されました。");
		}

		/// <summary>
		/// F11　リボン終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Syuuryou_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

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
	
	}
}
