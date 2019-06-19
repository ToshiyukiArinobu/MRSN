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


using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
    /// 車輌マスタ問合せ
	/// </summary>
	public partial class MST06020 : WindowMasterMainteBase
	{
        private const string TargetTableNm = "M05_CAR_ICHIRAN";

        private string _車輌IDFROM = string.Empty;
        public string 車輌IDFROM
		{
            get { return this._車輌IDFROM; }
            set { this._車輌IDFROM = value; NotifyPropertyChanged(); }
		}
        private string _車輌IDTO = string.Empty;
        public string 車輌IDTO
		{
            get { return this._車輌IDTO; }
            set { this._車輌IDTO = value; NotifyPropertyChanged(); }
		}

        private string _自社部門IDFROM = string.Empty;
        public string 自社部門IDFROM
        {
            get { return this._自社部門IDFROM; }
            set { this._自社部門IDFROM = value; NotifyPropertyChanged(); }
        }
        private string _自社部門IDTO = string.Empty;
        public string 自社部門IDTO
        {
            get { return this._自社部門IDTO; }
            set { this._自社部門IDTO = value; NotifyPropertyChanged(); }
        }

        private string _廃車区分 = "1";
        public string 廃車区分
        {
            get { return this._廃車区分; }
            set { this._廃車区分 = value; NotifyPropertyChanged(); }
        }

        private string _廃車区分表示 = string.Empty;
        public string 廃車区分表示
        {
            get { return this._廃車区分表示; }
            set { this._廃車区分表示 = value; NotifyPropertyChanged(); }
        }

        private string _表示区分 = "0";
		public string 表示区分
		{
            get { return this._表示区分; }
            set { this._表示区分 = value; NotifyPropertyChanged(); }
		}

        //データグリッドバインド用データテーブル
        private DataTable _mSTData;
		public DataTable MstData
		{
			get { return this._mSTData; }
			set
			{
				this._mSTData = value;
				NotifyPropertyChanged();
			}
		}


		/// <summary>
		/// 車輌マスタ問合せ
		/// </summary>
        public MST06020()
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
            GridOutPut();
		}

        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            
            try
            {
                //車輌マスタ(暫定版)
                SearchColumn();
            }
            catch (Exception)
            {
                return;
            }
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
			case Key.F7:
				this.Purebyu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Purebyu));
				break;
			case Key.F8:
				this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
				break;
			}
		}


		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			SearchColumn();
		}

		/// <summary>
		/// 接続とDataSetの作成
		/// </summary>
		void SearchColumn()
		{
			try
			{
                int syaryouFrom = int.MinValue;
                int syaryouTo = int.MaxValue;
                int bumonFrom = int.MinValue;
                int bumonTo = int.MaxValue;

                if(!int.TryParse(車輌IDFROM,out syaryouFrom))                
                {
                   syaryouFrom = int.MinValue;
                }

                if (!int.TryParse(車輌IDTO, out syaryouTo))
                {
                    syaryouTo = int.MaxValue;
                }

                if (!int.TryParse(自社部門IDFROM, out bumonFrom))
                {
                    bumonFrom = int.MinValue;
                }

                if (!int.TryParse(自社部門IDTO, out bumonTo))
                {
                    bumonTo = int.MaxValue;
                }


                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { syaryouFrom, syaryouTo , bumonFrom, bumonTo , 廃車区分,表示区分}));
				
			}
			catch (Exception)
			{
				return;
			}
		}

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
                case TargetTableNm:
                    if (tbl.Columns.Contains("登録日時"))
                    {
                        tbl.Columns.Remove("登録日時");
                    }
                    if (tbl.Columns.Contains("更新日時"))
                    {
                        tbl.Columns.Remove("更新日時");
                    }

                    //Gridのバインド変数に代入
                    MstData = tbl;

                    break;
			}
            //this.MSTData = null;
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}
        

        #region リボン
        /// <summary>
        /// F7 リボン　プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF7Key(object sender, KeyEventArgs e)
        {
            KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();

            SetMstData();

            view.MakeReport("車輌マスタプレビュー", "..\\..\\..\\HakobouDEMO\\Files\\MST06010.rpt", 0, 0, 0);
            view.SetReportData(MstData);
            view.ShowPreview();

        }

        private void SetMstData()
        {
            if (!MstData.Columns.Contains("自社部門名"))
            {
                MstData.Columns.Add("自社部門名");
            }

            if (!MstData.Columns.Contains("車種名"))
            {
                MstData.Columns.Add("車種名");
            }

            //乗務員
            if (!MstData.Columns.Contains("乗務員名"))
            {
                MstData.Columns.Add("乗務員名");
            }

            //運輸局
            if (!MstData.Columns.Contains("運輸局名"))
            {
                MstData.Columns.Add("運輸局名");
            }

            for (int i = 0; i < MstData.Rows.Count; i++)
            {
                MstData.Rows[i]["自社部門名"] = "テスト部門";
                MstData.Rows[i]["車種名"] = "テスト車種名";
                MstData.Rows[i]["乗務員名"] = "テスト乗務員名";
                MstData.Rows[i]["運輸局名"] = "テスト運輸局名";
            }
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
        }

        /// <summary>
        /// F10　リボン入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
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



	}
}
