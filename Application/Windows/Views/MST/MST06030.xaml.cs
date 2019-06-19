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
using System.ComponentModel;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using GrapeCity.Windows.SpreadGrid;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 車輌固定費一括登録
	/// </summary>
	public partial class MST06030 : WindowMasterMainteBase
	{


        public class M05_CAR_Member : INotifyPropertyChanged
        {
            public int _車輌ID { get; set; }
            public int _車輌KEY { get; set; }
            public DateTime? _登録日時 { get; set; }
            public DateTime? _更新日時 { get; set; }
            public string _車輌番号 { get; set; }
            public int? _自社部門ID { get; set; }
            public int? _車種ID { get; set; }
            public int? _乗務員ID { get; set; }
            public int _乗務員KEY { get; set; }
            public int _運輸局ID { get; set; }
            public int _自傭区分 { get; set; }
            public int _廃車区分 { get; set; }
            public string _廃車区分表示 { get; set; }
            public DateTime? _廃車日 { get; set; }
            public DateTime? _次回車検日 { get; set; }
            public string _車輌登録番号 { get; set; }
            public DateTime? _登録日 { get; set; }
            public int _初年度登録年 { get; set; }
            public int _初年度登録月 { get; set; }
            public string _自動車種別 { get; set; }
            public string _用途 { get; set; }
            public string _自家用事業用 { get; set; }
            public string _車体形状 { get; set; }
            public string _車名 { get; set; }
            public string _型式 { get; set; }
            public int _乗車定員 { get; set; }
            public int _車輌重量 { get; set; }
            public int _車輌最大積載量 { get; set; }
            public int _車輌総重量 { get; set; }
            public int _車輌実積載量 { get; set; }
            public string _車台番号 { get; set; }
            public string _原動機型式 { get; set; }
            public int _長さ { get; set; }
            public int _幅 { get; set; }
            public int _高さ { get; set; }
            public int _総排気量 { get; set; }
            public string _燃料種類 { get; set; }
            public int _現在メーター { get; set; }
            public int _デジタコCD { get; set; }
            public string _所有者名 { get; set; }
            public string _所有者住所 { get; set; }
            public string _使用者名 { get; set; }
            public string _使用者住所 { get; set; }
            public string _使用本拠位置 { get; set; }
            public string _備考 { get; set; }
            public string _型式指定番号 { get; set; }
            public int _前前軸重 { get; set; }
            public int _前後軸重 { get; set; }
            public int _後前軸重 { get; set; }
            public int _後後軸重 { get; set; }
            public int _CO2区分 { get; set; }
            public decimal _基準燃費 { get; set; }
            public decimal _黒煙規制値 { get; set; }
            public int? _G車種ID { get; set; }
            public int? _規制区分ID { get; set; }
            public decimal _燃料費単価 { get; set; }
            public decimal _油脂費単価 { get; set; }
            public decimal _修繕費単価 { get; set; }
            public decimal _タイヤ費単価 { get; set; }
            public decimal _車検費単価 { get; set; }
            public int? _固定自動車税 { get; set; }
            public int? _固定重量税 { get; set; }
            public int? _固定取得税 { get; set; }
            public int? _固定自賠責保険 { get; set; }
            public int? _固定車輌保険 { get; set; }
            public int? _固定対人保険 { get; set; }
            public int? _固定対物保険 { get; set; }
            public int? _固定貨物保険 { get; set; }
            //追加分
            public DateTime? _削除日付 { get; set; }
            public string _乗務員名 { get; set; }
            public string _運輸局名 { get; set; }
            public string _自社部門名 { get; set; }
            public string _車種名 { get; set; }




            public int 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public int 車輌KEY { get { return _車輌KEY; } set { _車輌KEY = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
            public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value; NotifyPropertyChanged(); } }
            public int? 車種ID { get { return _車種ID; } set { _車種ID = value; NotifyPropertyChanged(); } }
            public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
            public int 乗務員KEY { get { return _乗務員KEY; } set { _乗務員KEY = value; NotifyPropertyChanged(); } }
            public int 運輸局ID { get { return _運輸局ID; } set { _運輸局ID = value; NotifyPropertyChanged(); } }
            public int 自傭区分 { get { return _自傭区分; } set { _自傭区分 = value; NotifyPropertyChanged(); } }
            public int 廃車区分 { get { return _廃車区分; } set { _廃車区分 = value; NotifyPropertyChanged(); } }
            public string 廃車区分表示 { get { return _廃車区分表示; } set { _廃車区分表示 = value; NotifyPropertyChanged(); } }
            public DateTime? 廃車日 { get { return _廃車日; } set { _廃車日 = value; NotifyPropertyChanged(); } }
            public DateTime? 次回車検日 { get { return _次回車検日; } set { _次回車検日 = value; NotifyPropertyChanged(); } }
            public string 車輌登録番号 { get { return _車輌登録番号; } set { _車輌登録番号 = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日 { get { return _登録日; } set { _登録日 = value; NotifyPropertyChanged(); } }
            public int 初年度登録年 { get { return _初年度登録年; } set { _初年度登録年 = value; NotifyPropertyChanged(); } }
            public int 初年度登録月 { get { return _初年度登録月; } set { _初年度登録月 = value; NotifyPropertyChanged(); } }
            public string 自動車種別 { get { return _自動車種別; } set { _自動車種別 = value; NotifyPropertyChanged(); } }
            public string 用途 { get { return _用途; } set { _用途 = value; NotifyPropertyChanged(); } }
            public string 自家用事業用 { get { return _自家用事業用; } set { _自家用事業用 = value; NotifyPropertyChanged(); } }
            public string 車体形状 { get { return _車体形状; } set { _車体形状 = value; NotifyPropertyChanged(); } }
            public string 車名 { get { return _車名; } set { _車名 = value; NotifyPropertyChanged(); } }
            public string 型式 { get { return _型式; } set { _型式 = value; NotifyPropertyChanged(); } }
            public int 乗車定員 { get { return _乗車定員; } set { _乗車定員 = value; NotifyPropertyChanged(); } }
            public int 車輌重量 { get { return _車輌重量; } set { _車輌重量 = value; NotifyPropertyChanged(); } }
            public int 車輌最大積載量 { get { return _車輌最大積載量; } set { _車輌最大積載量 = value; NotifyPropertyChanged(); } }
            public int 車輌総重量 { get { return _車輌総重量; } set { _車輌総重量 = value; NotifyPropertyChanged(); } }
            public int 車輌実積載量 { get { return _車輌実積載量; } set { _車輌実積載量 = value; NotifyPropertyChanged(); } }
            public string 車台番号 { get { return _車台番号; } set { _車台番号 = value; NotifyPropertyChanged(); } }
            public string 原動機型式 { get { return _原動機型式; } set { _原動機型式 = value; NotifyPropertyChanged(); } }
            public int 長さ { get { return _長さ; } set { _長さ = value; NotifyPropertyChanged(); } }
            public int 幅 { get { return _幅; } set { _幅 = value; NotifyPropertyChanged(); } }
            public int 高さ { get { return _高さ; } set { _高さ = value; NotifyPropertyChanged(); } }
            public int 総排気量 { get { return _総排気量; } set { _総排気量 = value; NotifyPropertyChanged(); } }
            public string 燃料種類 { get { return _燃料種類; } set { _燃料種類 = value; NotifyPropertyChanged(); } }
            public int 現在メーター { get { return _現在メーター; } set { _現在メーター = value; NotifyPropertyChanged(); } }
            public int デジタコCD { get { return _デジタコCD; } set { _デジタコCD = value; NotifyPropertyChanged(); } }
            public string 所有者名 { get { return _所有者名; } set { _所有者名 = value; NotifyPropertyChanged(); } }
            public string 所有者住所 { get { return _所有者住所; } set { _所有者住所 = value; NotifyPropertyChanged(); } }
            public string 使用者名 { get { return _使用者名; } set { _使用者名 = value; NotifyPropertyChanged(); } }
            public string 使用者住所 { get { return _使用者住所; } set { _使用者住所 = value; NotifyPropertyChanged(); } }
            public string 使用本拠位置 { get { return _使用本拠位置; } set { _使用本拠位置 = value; NotifyPropertyChanged(); } }
            public string 備考 { get { return _備考; } set { _備考 = value; NotifyPropertyChanged(); } }
            public string 型式指定番号 { get { return _型式指定番号; } set { _型式指定番号 = value; NotifyPropertyChanged(); } }
            public int 前前軸重 { get { return _前前軸重; } set { _前前軸重 = value; NotifyPropertyChanged(); } }
            public int 前後軸重 { get { return _前後軸重; } set { _前後軸重 = value; NotifyPropertyChanged(); } }
            public int 後前軸重 { get { return _後前軸重; } set { _後前軸重 = value; NotifyPropertyChanged(); } }
            public int 後後軸重 { get { return _後後軸重; } set { _後後軸重 = value; NotifyPropertyChanged(); } }
            public int CO2区分 { get { return _CO2区分; } set { _CO2区分 = value; NotifyPropertyChanged(); } }
            public decimal 基準燃費 { get { return _基準燃費; } set { _基準燃費 = value; NotifyPropertyChanged(); } }
            public decimal 黒煙規制値 { get { return _黒煙規制値; } set { _黒煙規制値 = value; NotifyPropertyChanged(); } }
            public int? G車種ID { get { return _G車種ID; } set { _G車種ID = value; NotifyPropertyChanged(); } }
            public int? 規制区分ID { get { return _規制区分ID; } set { _規制区分ID = value; NotifyPropertyChanged(); } }
            public decimal 燃料費単価 { get { return _燃料費単価; } set { _燃料費単価 = value; NotifyPropertyChanged(); } }
            public decimal 油脂費単価 { get { return _油脂費単価; } set { _油脂費単価 = value; NotifyPropertyChanged(); } }
            public decimal 修繕費単価 { get { return _修繕費単価; } set { _修繕費単価 = value; NotifyPropertyChanged(); } }
            public decimal タイヤ費単価 { get { return _タイヤ費単価; } set { _タイヤ費単価 = value; NotifyPropertyChanged(); } }
            public decimal 車検費単価 { get { return _車検費単価; } set { _車検費単価 = value; NotifyPropertyChanged(); } }
            public int? 固定自動車税 { get { return _固定自動車税; } set { _固定自動車税 = value; NotifyPropertyChanged(); } }
            public int? 固定重量税 { get { return _固定重量税; } set { _固定重量税 = value; NotifyPropertyChanged(); } }
            public int? 固定取得税 { get { return _固定取得税; } set { _固定取得税 = value; NotifyPropertyChanged(); } }
            public int? 固定自賠責保険 { get { return _固定自賠責保険; } set { _固定自賠責保険 = value; NotifyPropertyChanged(); } }
            public int? 固定車輌保険 { get { return _固定車輌保険; } set { _固定車輌保険 = value; NotifyPropertyChanged(); } }
            public int? 固定対人保険 { get { return _固定対人保険; } set { _固定対人保険 = value; NotifyPropertyChanged(); } }
            public int? 固定対物保険 { get { return _固定対物保険; } set { _固定対物保険 = value; NotifyPropertyChanged(); } }
            public int? 固定貨物保険 { get { return _固定貨物保険; } set { _固定貨物保険 = value; NotifyPropertyChanged(); } }
            //追加分
            public DateTime? 削除日付 { get { return _削除日付; } set { _削除日付 = value; NotifyPropertyChanged(); } }
            public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
            public string 運輸局名 { get { return _運輸局名; } set { _運輸局名 = value; NotifyPropertyChanged(); } }
            public string 自社部門名 { get { return _自社部門名; } set { _自社部門名 = value; NotifyPropertyChanged(); } }
            public string 車種名 { get { return _車種名; } set { _車種名 = value; NotifyPropertyChanged(); } }


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


		public UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion
        public class ConfigMST06030 : FormConfigBase
        {
        }

        public ConfigMST06030 frmcfg = null;


		/// <summary>
        /// 一括取得用
		/// </summary>
        private const string 一括車輌固定データ取得 = "M05_CAR_KOTEI_ALL";

        /// <summary>
        /// 一括更新用
        /// </summary>
        private const string 一括車輌固定データ更新 = "M05_CAR_KOTEI_UPD";

        /// <summary>
        /// 一括更新終了時用
        /// </summary>
        private const string 一括車輌固定データ更新終了時 = "M05_CAR_KOTEI_UPD_EXIT";

		private DataTable _一括車輌固定費データ;
		public DataTable 一括車輌固定費データ
		{
			get { return this._一括車輌固定費データ; }
			set { this._一括車輌固定費データ = value; NotifyPropertyChanged(); }
		}
		
		/// <summary>
		/// 乗務員マスタ一括入力
		/// </summary>
		public MST06030() : base()
		{
			InitializeComponent();
			this.DataContext = this;
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//sp車輌一括データ.InputBindings.Add(new KeyBinding(sp車輌一括データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST06030)ucfg.GetConfigValue(typeof(ConfigMST06030));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST06030();
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            ScreenClear();

            ResetAllValidation();

		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
		}

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message"></param>
		public override void OnReceivedResponseData(CommunicationObject message)
		{
			try
			{
				this.ErrorMessage = string.Empty;

				var data = message.GetResultData();
				DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    case 一括車輌固定データ取得:
                        this.一括車輌固定費データ = tbl;
                        break;
                    case 一括車輌固定データ更新:
                        MessageBox.Show("データを更新しました。");
                        this.Close();
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


		private void Update()
		{
			try
			{
                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    SendRequest(new CommunicationObject(MessageType.UpdateData, 一括車輌固定データ更新, new object[] { 一括車輌固定費データ }));
                }
                else
                {
                    return;
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        /// <summary>
        /// 画面初期化
        /// </summary>
		private void ScreenClear()
		{
            SendRequest(new CommunicationObject(MessageType.RequestData, 一括車輌固定データ取得));
		}

		/// <summary>
		/// F1 マスタ検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF1Key(object sender, KeyEventArgs e)
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
			ScreenClear();
		}

		/// <summary>
		/// F11　リボン終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF11Key(object sender, KeyEventArgs e)
		{
			//this.Close();
            Boolean EndFlg = true;
            foreach( DataRow RowData in 一括車輌固定費データ.Rows){
                if( RowData.RowState == DataRowState.Modified ){
                    EndFlg = false;
                    break;
                }
            }

            if (EndFlg)
            {
                this.Close();
            }
            else
            {
                var yesno = MessageBox.Show("更新せずに終了してもよろしいですか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.No)
                {
                    return;
                }
                this.Close();
            }
        }

		private void 一括入力_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void sp一括_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
			if (e.EditAction == SpreadEditAction.Cancel)
			{
				return;
			}
			string nm = e.CellPosition.ColumnName;

            DataRow BeforeNum = 一括車輌固定費データ.Rows[e.CellPosition.Row];
            if (Convert.ToDecimal(BeforeNum[nm, DataRowVersion.Original]) != Convert.ToDecimal(this.sp車輌一括データ.ActiveCell.Value))
            {
                this.sp車輌一括データ.Cells[e.CellPosition.Row, 1].Foreground = new SolidColorBrush(Colors.Red);
            }

            // 全ての値が元に戻った場合、文字色を元に戻す
            int DispFlg = 0;

            for (int Cnt = 0 ; Cnt < BeforeNum.ItemArray.Length; Cnt++)
            {
                string ColName = "d" + BeforeNum.Table.Columns[Cnt].ColumnName;
                switch(BeforeNum.Table.Columns[Cnt].ColumnName){
                    case "固定自動車税":
                    case "固定重量税":
                    case "固定取得税":
                    case "固定自賠責保険":
                    case "固定車輌保険":
                    case "固定対人保険":
                    case "固定対物保険":
                    case "固定貨物保険":
                        if (Convert.ToDecimal(BeforeNum[Cnt, DataRowVersion.Original]) == Convert.ToDecimal(this.sp車輌一括データ.Cells[e.CellPosition.Row, ColName].Value))
                        {
                            DispFlg += 1;
                        }
                        break;
                    default:
                        break;
                }

            }

            if( DispFlg == 8 ){
                this.sp車輌一括データ.Cells[e.CellPosition.Row, 1].Foreground = new SolidColorBrush(Colors.Black);
            }
        }


		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
            SendRequest(new CommunicationObject(MessageType.UpdateData, 一括車輌固定データ更新終了時, new object[] { 一括車輌固定費データ }));
			sp車輌一括データ.InputBindings.Clear();
			一括車輌固定費データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

		}
		#endregion

        private void sp車輌一括入力_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.V)
            {
                e.Handled = true;
            }
        }
	}
}
