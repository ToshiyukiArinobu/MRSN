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
    /// 取引先期首残一括登録
	/// </summary>
	public partial class MST01015 : WindowMasterMainteBase
	{


        public class M01_KIS_Member : INotifyPropertyChanged
        {
            public int _取引先ID { get; set; }
            public string _取引先名 { get; set; }
            public DateTime? _登録日時 { get; set; }
            public DateTime? _更新日時 { get; set; }
            public string _取引区分 { get; set; }
            public int? _Ｔ締日期首残 { get; set; }
            public int? _Ｔ月次期首残 { get; set; }
            public int? _Ｓ締日期首残 { get; set; }
            public int? _Ｓ月次期首残 { get; set; }


            public int 取引先ID { get { return _取引先ID; } set { _取引先ID = value; NotifyPropertyChanged(); } }
            public string 取引先名 { get { return _取引先名; } set { _取引先名 = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public string 取引区分 { get { return _取引区分; } set { _取引区分 = value; NotifyPropertyChanged(); } }
            public int? Ｔ締日期首残 { get { return _Ｔ締日期首残; } set { _Ｔ締日期首残 = value; NotifyPropertyChanged(); } }
            public int? Ｔ月次期首残 { get { return _Ｔ月次期首残; } set { _Ｔ月次期首残 = value; NotifyPropertyChanged(); } }
            public int? Ｓ締日期首残 { get { return _Ｓ締日期首残; } set { _Ｓ締日期首残 = value; NotifyPropertyChanged(); } }
            public int? Ｓ月次期首残 { get { return _Ｓ月次期首残; } set { _Ｓ月次期首残 = value; NotifyPropertyChanged(); } }

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

        public class ConfigMST01015 : FormConfigBase
        {
        }

        public ConfigMST01015 frmcfg = null;

        /// <summary>
        /// 一括取得用
		/// </summary>
        private const string 取引先期首残一括データ取得 = "M01_KIS_ALL";

        /// <summary>
        /// 一括更新用
        /// </summary>
        private const string 取引先期首残一括データ更新 = "M01_KIS_UPD";

        private DataTable _取引先一括期首残データ;
        public DataTable 取引先一括期首残データ
		{
            get { return this._取引先一括期首残データ; }
            set { this._取引先一括期首残データ = value; NotifyPropertyChanged(); }
		}

        #region "固定値"
        private const string 期首年月始 = "期首年月　";
        private const string 期首年月終 = "　～";
        #endregion

        #region "合計"
        private decimal _Ｔ締日期首残計 = 0;
        public decimal Ｔ締日期首残計
        {
            get { return _Ｔ締日期首残計; }
            set { _Ｔ締日期首残計 = value; NotifyPropertyChanged(); }
        }

        private decimal _Ｔ月次期首残計 = 0;
        public decimal Ｔ月次期首残計
        {
            get { return _Ｔ月次期首残計; }
            set { _Ｔ月次期首残計 = value; NotifyPropertyChanged(); }
        }

        private decimal _Ｓ締日期首残計 = 0;
        public decimal Ｓ締日期首残計
        {
            get { return _Ｓ締日期首残計; }
            set { _Ｓ締日期首残計 = value; NotifyPropertyChanged(); }
        }

        private decimal _Ｓ月次期首残計 = 0;
        public decimal Ｓ月次期首残計
        {
            get { return _Ｓ月次期首残計; }
            set { _Ｓ月次期首残計 = value; NotifyPropertyChanged(); }
        }
        #endregion

        // 権限用
        CommonConfig ccfg = null;

        private Boolean InitFlg = false;

        /// <summary>
        /// 取引先期首残一括入力
		/// </summary>
		public MST01015() : base()
		{
            InitializeComponent();
			this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST01015)ucfg.GetConfigValue(typeof(ConfigMST01015));

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg == null)
            {
                frmcfg = new ConfigMST01015();
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            InitFlg = true; 
            ScreenClear();

            ResetAllValidation();

        }

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
		}

        /// <summary>
        /// FileterSet
        /// フィルターの設定
        /// </summary>
        private void FileterSet()
        {
            //SpreadFilterDescription sfd = new SpreadFilterDescription();
            //sfd.ColumnName = "取引先ID";
            //IncludeListCondition ilc = new IncludeListCondition();
            //var MinVal = 取引先一括期首残データ.Compute("MIN(取引先ID)", "").ToString();
            //var MaxVal = 取引先一括期首残データ.Compute("MAX(取引先ID)", "").ToString();
            //ilc.Values.Add(MinVal);
            //ilc.Values.Add(MaxVal);
            //sfd.Conditions.Add(ilc);
            //sp取引先一括データ.FilterDescriptions.Add(sfd);

            //sp取引先一括データ.Columns["取引先ID"].FilterDropDownList = new FilterDropDownList() { ShowSort = true, ShowIconBar = true };
            //sp取引先一括データ.ColumnHeader.FilterButtonIndex = 0;

            sp取引先一括データ.CanUserFilterColumns = true;
            sp取引先一括データ.CanUserSortColumns = false;

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
                    case 取引先期首残一括データ取得:
                        if (tbl != null)
                        {
                            this.取引先一括期首残データ = tbl;
                            string ym = Convert.ToString(取引先一括期首残データ.Compute("max(期首年月)", null));
                            if (ym.Length == 6)
                            {
                                ym = ym.Substring(0, 4) + "/" + ym.Substring(4, 2);
                            }
                            else
                            {
                                ym = string.Empty;
                            }
                            this.lbl期首年月.LabelText = string.Format("{0}{1}{2}", 期首年月始, ym, 期首年月終);

                            // ロック設定
                            if (DataUpdateVisible == System.Windows.Visibility.Hidden)
                            {
                                権限Get.SpreadGridLock(this,true);
                            }

                            // フィルタ設定
                            FileterSet();

                            SumCalculation();
                        }
                        break;
                    case 取引先期首残一括データ更新:
                        MessageBox.Show("データを更新しました。");
                        /*
                        ScreenClear(); 
                        EditOn = false;
                        */
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
                    SendRequest(new CommunicationObject(MessageType.UpdateData, 取引先期首残一括データ更新, new object[] { 取引先一括期首残データ }));
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
            sp取引先一括データ.FilterDescriptions.Clear();

            SendRequest(new CommunicationObject(MessageType.RequestData, 取引先期首残一括データ取得));
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
            //#region "権限関係"
            //if (DataUpdateVisible == Visibility.Hidden) { e.Handled = true; return; }
            //#endregion
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
            if (Convert.ToInt32(取引先一括期首残データ.Compute("count(Ｔ締日期首残EditFlg) + count(Ｔ月次期首残EditFlg) + count(Ｓ締日期首残EditFlg) + count(Ｓ月次期首残EditFlg)",
                                                             "Ｔ締日期首残EditFlg = true or Ｔ月次期首残EditFlg = true or Ｓ締日期首残EditFlg = true or Ｓ月次期首残EditFlg = true")) == 0)
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

        #region "スプレッド関連"
        // スプレッドCell変更前値
        private void sp取引先_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
            //var row = sp取引先一括データ.ActiveRowIndex;
            //var col = sp取引先一括データ.ActiveColumnIndex;
            //this.sp取引先一括データ.ActiveCellPosition = new CellPosition(20, col);

            if (e.EditAction == SpreadEditAction.Cancel)
			{
                return;
			}

            DataRow BeforeNum = 取引先一括期首残データ.Rows[e.CellPosition.Row];
            if (Convert.ToDecimal(BeforeNum[e.CellPosition.ColumnName, DataRowVersion.Original]) != Convert.ToDecimal(this.sp取引先一括データ.ActiveCell.Value))
            {
                this.sp取引先一括データ.Cells[e.CellPosition.Row, 1].Foreground = new SolidColorBrush(Colors.Red);
                取引先一括期首残データ.Rows[e.CellPosition.Row][e.CellPosition.ColumnName + "EditFlg"] = true;
            }
            else
            {
                取引先一括期首残データ.Rows[e.CellPosition.Row][e.CellPosition.ColumnName + "EditFlg"] = false;
            }

            // 全ての値が元に戻った場合、文字色を元に戻す
            if (!Convert.ToBoolean(取引先一括期首残データ.Rows[e.CellPosition.Row]["Ｔ締日期首残EditFlg"])
             && !Convert.ToBoolean(取引先一括期首残データ.Rows[e.CellPosition.Row]["Ｔ月次期首残EditFlg"])
             && !Convert.ToBoolean(取引先一括期首残データ.Rows[e.CellPosition.Row]["Ｓ締日期首残EditFlg"])
             && !Convert.ToBoolean(取引先一括期首残データ.Rows[e.CellPosition.Row]["Ｓ月次期首残EditFlg"]))
            {
                this.sp取引先一括データ.Cells[e.CellPosition.Row, 1].Foreground = new SolidColorBrush(Colors.Black);                    
            }
            // 合計計算
            SumCalculation();
		}
        #endregion

        #region Window_Closed
        //画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
            sp取引先一括データ.InputBindings.Clear();
            取引先一括期首残データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);
        }
		#endregion

        private void sp取引先一括_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        #region 合計再計算
        //合計金額再計算
        public void SumCalculation()
        {
            Ｔ締日期首残計 = 0;
            Ｔ月次期首残計 = 0;
            Ｓ締日期首残計 = 0;
            Ｓ月次期首残計 = 0;

            //Ｔ締日期首残計 = (decimal)取引先一括期首残データ.Compute("sum(Ｔ締日期首残)", null);
            //Ｔ月次期首残計 = (decimal)取引先一括期首残データ.Compute("sum(Ｔ月次期首残)", null);
            //Ｓ締日期首残計 = (decimal)取引先一括期首残データ.Compute("sum(Ｓ締日期首残)", null);
            //Ｓ月次期首残計 = (decimal)取引先一括期首残データ.Compute("sum(Ｓ月次期首残)", null);
            foreach (var RowData in sp取引先一括データ.Rows)
            {
                if (RowData.IsVisible)
                {
                    decimal val = 0;
                    if (RowData.Cells[3].Value != null && decimal.TryParse(RowData.Cells[3].Value.ToString(), out val))
                    {
                    }else{
                        val = 0;
                    }
                    Ｔ締日期首残計 += val;

                    val = 0;
                    if (RowData.Cells[4].Value != null && decimal.TryParse(RowData.Cells[4].Value.ToString(), out val))
                    {
                    }
                    else
                    {
                        val = 0;
                    }
                    Ｔ月次期首残計 += val;

                    val = 0;
                    if (RowData.Cells[5].Value != null && decimal.TryParse(RowData.Cells[5].Value.ToString(), out val))
                    {
                    }
                    else
                    {
                        val = 0;
                    }
                    Ｓ締日期首残計 += val;

                    val = 0;
                    if (RowData.Cells[6].Value != null && decimal.TryParse(RowData.Cells[6].Value.ToString(), out val))
                    {
                    }
                    else
                    {
                        val = 0;
                    }
                    Ｓ月次期首残計 += val;
                }
            }

        }
        #endregion

        /// <summary>
        /// sp取引先_RowCollectionChanged
        /// 表示が変更された場合の疑似イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp取引先_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
        {
            if (InitFlg && sp取引先一括データ.Rows.Count() > 0)
            {
                SumCalculation();
            }
        }
    }
}
