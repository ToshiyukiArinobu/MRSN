﻿using System;
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
using System.Data;
using System.Data.SqlClient;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{

	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
	public partial class SCH06010 : WindowMasterSearchBase
	{

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigSCH06010 : FormConfigBase
        {
            public int Combo { get; set; }
            public int VehicleCombo { get; set; }

        }
        /// ※ 必ず public で定義する。
        public ConfigSCH06010 frmcfg = null;

        #endregion

        private const string TabelNm = "M05_CAR_SCH";
        private const string COLUM_ID = "車輌ID";
        private const string COLUM_NAME = "車輌番号";
        private const string COLUM_KANA = "乗務員名";

        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }

        private bool _廃車表示 = false;
        public bool 廃車表示
        {
            get { return this._廃車表示; }
            set { this._廃車表示 = value; NotifyPropertyChanged(); }
        }

        private int _廃車区分 = 0;
        public int 廃車区分
        {
            get { return this._廃車区分; }
            set { this._廃車区分 = value; NotifyPropertyChanged(); }
        }


        //データグリッドバインド用データテーブル
        private DataTable _carData = null;
        public DataTable CarData
        {
            get { return this._carData; }
            set
            {
                this._carData = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedCodeList = string.Empty;
        private bool _multiSelect = false;
        public bool MultiSelect
        {
            get { return this._multiSelect; }
            set
            {
                this._multiSelect = value;
                if (value == true)
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Extended;
                }
                else
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Single;
                }
            }
        }

        public SCH06010()
		{
			InitializeComponent();
			this.DataContext = this;
			this.Topmost = true;
		}

		/// <summary>
		/// Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{

            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            // 初期フォーカスの設定を行う
            this.KakuteiCode.Focus();
            GridOutPut();

            //画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            //メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                //画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            //コンボボックスのSelectionChangedを設定する。
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;
            this.Vehicle.SelectionChanged += this.Vehicle_SelectionChanged;

            AppCommon.SetutpComboboxList(this.OrderColumn, false);
            AppCommon.SetutpComboboxList(this.Vehicle, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH06010)ucfg.GetConfigValue(typeof(ConfigSCH06010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH06010();
                ucfg.SetConfigValue(frmcfg);
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
                this.OrderColumn.SelectedIndex = frmcfg.Combo;
                this.Vehicle.SelectedIndex = frmcfg.VehicleCombo;
            }
            #endregion


		}

		/// <summary>
		/// データの取得
		/// </summary>
		private void GridOutPut()
		{
            int syaid = -1;
            if (!int.TryParse(this._確定コード, out syaid))
            {
                syaid = -1;
            }

            try
            {
                //車輌マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { syaid, this._廃車区分 }));
            }
            catch (Exception)
            {
                return;
            }
		}

        /// <summary>
        /// データ取得エラー時処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
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
			case TabelNm:
                    CarData = tbl;

                    DataView view = new DataView(CarData);

                    switch (OrderColumn.SelectedIndex)
                    {
                        case 0: //コード
                        default:
                            view.Sort = COLUM_ID;
                            break;
                        case 1: //商品名
                            view.Sort = COLUM_NAME;
                            break;
                        case 2:　//商品よみ
                            view.Sort = COLUM_KANA;
                            break;
                    }

                    CarData = view.ToTable();
                    SearchGrid.SelectedIndex = 0;

				break;
			default:
				break;
			}
        }

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            CloseDataSelected();
        }

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
        }

		/// <summary>
		/// 検索ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void OkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 12;
            this.OkButton.Content = "選択";
        }

        private void OkButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
        }

        private void CancelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 12;
            this.CancelButton.Content = "閉じる";
        }

        private void CancelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";
        }

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
            // グリッドの中で選択された行があるかどうかをチェックして終了
            CloseDataSelected();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        /// <summary>
        /// グリッドダブルグリッド時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            CloseDataSelected();

        }

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void CloseDataSelected()
        {
            if (this.SearchGrid.SelectedItems.Count == 0)
            {
                return;
            }

            this.SelectedCodeList = string.Empty;
            try
            {
                List<string> work = new List<string>();
                string delmtr = "";
                foreach (DataRowView row in this.SearchGrid.SelectedItems)
                {
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row[COLUM_ID].ToString();
                    delmtr = ",";
                }

                //選択したテキストへ値を挿入
                this.TwinTextBox.Text1 = CarData.Rows[SearchGrid.SelectedIndex][COLUM_ID].ToString();
            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

        }


        private void txtKana_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        private void OrderColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarData == null)
            {
                return;
            }

            DataView view = new DataView(CarData);

            switch (OrderColumn.SelectedIndex)
            {
                case 0: //コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1: //商品名
                    view.Sort = COLUM_NAME;
                    break;
                case 2:　//商品よみ
                    view.Sort = COLUM_KANA;
                    break;

            }

            CarData = view.ToTable();
            SearchGrid.SelectedIndex = 0;
        }

        private void Vehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridOutPut();
        }
       
        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void Maindoow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.Combo = this.OrderColumn.SelectedIndex;
            frmcfg.VehicleCombo = this.Vehicle.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                CloseDataSelected();
            }
        }


    }
}
