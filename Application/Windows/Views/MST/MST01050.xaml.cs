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
    /// 取引先機首残一括登録
	/// </summary>
	public partial class MST01050 : WindowMasterMainteBase
	{

        public UserConfig ucfg = null;

        public class ConfigMST01050 : FormConfigBase
        {
        }

        public ConfigMST01050 frmcfg = null;
        
		/// <summary>
        /// 一括取得用
		/// </summary>
        private const string 削除取引先データ取得 = "M01_DEL_ALL";

        /// <summary>
        /// 一括更新用
        /// </summary>
        private const string 削除取引先データ更新 = "M01_DEL_UPD";

        private DataTable _削除取引先データ;
        public DataTable 削除取引先データ
		{
            get { return this._削除取引先データ; }
            set { this._削除取引先データ = value; NotifyPropertyChanged(); }
		}


        /// <summary>
        /// 取引先機首残一括入力
		/// </summary>
		public MST01050() : base()
		{
			InitializeComponent();
			this.DataContext = this;
		}

        private void MST01050_Loaded(object sender, RoutedEventArgs e)
		{
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST01050)ucfg.GetConfigValue(typeof(ConfigMST01050));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST01050();
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            ScreenClear();
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
                    case 削除取引先データ取得:
                        this.削除取引先データ = tbl;
                        break;

                    case 削除取引先データ更新:
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
                var yesno = MessageBox.Show("選択データを復活しますか？", "復活確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    SendRequest(new CommunicationObject(MessageType.UpdateData, 削除取引先データ更新, new object[] { 削除取引先データ }));
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
            SendRequest(new CommunicationObject(MessageType.RequestData, 削除取引先データ取得));
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
            if (Convert.ToInt32(削除取引先データ.Compute("count(ResurrectionCheckbox)",
                                                         "ResurrectionCheckbox = true")) == 0)
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
        private void sp削除取引先_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
			if (e.EditAction == SpreadEditAction.Cancel)
			{
				return;
			}

            DataRow BeforeNum = 削除取引先データ.Rows[e.CellPosition.Row];
            if (BeforeNum[e.CellPosition.ColumnName, DataRowVersion.Original] != this.sp削除取引先データ.ActiveCell.Value)
            {
                this.sp削除取引先データ.Cells[e.CellPosition.Row, "取引先名１"].Foreground = new SolidColorBrush(Colors.Blue);
                this.sp削除取引先データ.Cells[e.CellPosition.Row, "取引先名２"].Foreground = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                this.sp削除取引先データ.Cells[e.CellPosition.Row, "取引先名１"].Foreground = new SolidColorBrush(Colors.Black);
                this.sp削除取引先データ.Cells[e.CellPosition.Row, "取引先名２"].Foreground = new SolidColorBrush(Colors.Black);
            }

            AppCommon.FixSpreadActiveCell(sp削除取引先データ);

        }
        #endregion

        #region Window_Closed
                //画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
            sp削除取引先データ.InputBindings.Clear();
            削除取引先データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);
        }
		#endregion

        private void sp削除取引先_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

    }
}
