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
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 取引先マスタ問合せ
	/// </summary>
	public partial class MST01010_1 : WindowMasterMainteBase
	{
		private const string GetMasterList = "M01_TOK_LIST";
		private const string GetMasterList_CSV = "M01_TOK_LIST_CSV";
		private string rptFilePath = @"Files\MST\MST01010.rpt";

		public UserConfig ucfg = null;
		public class ConfigMST01010_1 : FormConfigBase
		{
		}
		/// ※ 必ず public で定義する。
		public ConfigMST01010_1 frmcfg = null;

		private string _取引先FROM = string.Empty;
		public string 得意先範囲指定From
		{
			get { return this._取引先FROM; }
			set { this._取引先FROM = value; NotifyPropertyChanged(); }
		}

		private string _取引先TO = string.Empty;
		public string 得意先範囲指定To
		{
			get { return this._取引先TO; }
			set { this._取引先TO = value; NotifyPropertyChanged(); }
		}

		private string _表示方法 = "0";
		public string 表示方法
		{
			get { return this._表示方法; }
			set { this._表示方法 = value; NotifyPropertyChanged(); }
		}

		private string _表示区分 = "0";
		public string 表示区分
		{
			get { return this._表示区分; }
			set { this._表示区分 = value; NotifyPropertyChanged(); }
		}

		private string _取引区分 = "0";
		public string 取引区分
		{
			get { return this._取引区分; }
			set { this._取引区分 = value; NotifyPropertyChanged(); }
		}

        private string _取引先指定 = string.Empty;
        public string 取引先指定
        {
            get { return this._取引先指定; }
            set { this._取引先指定 = value; NotifyPropertyChanged(); }
        }

        private string _取引停止区分 = "0";
        public string 取引停止区分
        {
            get { return this._取引停止区分; }
            set { this._取引停止区分 = value; NotifyPropertyChanged(); }
        }

		private DataTable _mSTData;
		public DataTable MSTData
		{
			get { return this._mSTData; }
			set
			{
				this._mSTData = value;
				NotifyPropertyChanged();
			}
		}


		/// <summary>
		/// 取引先マスタ一覧
		/// </summary>
		public MST01010_1()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		/// <summary>
		/// Loadイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			#region 設定項目取得
			ucfg = AppCommon.GetConfig(this);
			frmcfg = (ConfigMST01010_1)ucfg.GetConfigValue(typeof(ConfigMST01010_1));
			if (frmcfg == null)
			{
				frmcfg = new ConfigMST01010_1();
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
				this.Width = frmcfg.Width;
				this.Height = frmcfg.Height;
			}
			#endregion

		}


		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

            //　追加
            if (tbl != null)           
            {
                tbl.Columns.Remove("取引停止区分");
                tbl.Columns["停止区分"].ColumnName = "取引停止区分";
            }

			switch (message.GetMessageName())
			{
			case GetMasterList:
				this.MSTData = tbl;
				PrintOut();
				break;
			//CSV出力
			case GetMasterList_CSV:
				OutPutCSV(tbl);
				break;
			}
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}

		public override void OnF1Key(object sender, KeyEventArgs e)
		{
			try
			{
				var ctl = FocusManager.GetFocusedElement(this);
				if (ctl is TextBox)
				{
					var uctext = ViewBaseCommon.FindVisualParent<UcTextBox>(ctl as UIElement);
					if (uctext == null)
					{
						return;
					}
					if (string.IsNullOrWhiteSpace(uctext.DataAccessName))
					{
						return;
					}
                    SCH01010 srch = new SCH01010();
                    switch (uctext.DataAccessName)
                    {
                        case "M01_TOK_PIC":
                            srch.MultiSelect = true;
                            break;
                        default:
                            srch.MultiSelect = false;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
					srch.表示区分 = 1;
					var ret = srch.ShowDialog(this);
					if (ret == true)
					{
						uctext.Text = srch.SelectedCodeList;
						FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
					}
				}
			}
			catch (Exception ex)
			{
				appLog.Error("検索画面起動エラー", ex);
				ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}
		}

		/// <summary>
		/// CSVファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF5Key(object sender, KeyEventArgs e)
		{
			int? tokFrom = null;
			int? tokTo = null;

			if (base.CheckAllValidation() != true)
			{
				this.ErrorMessage = "入力値にエラーがあります。";
				return;
			}

			if (string.IsNullOrWhiteSpace(this.得意先範囲指定From) != true)
			{
				tokFrom = AppCommon.IntParse(this.得意先範囲指定From);
			}
			if (string.IsNullOrWhiteSpace(this.得意先範囲指定To) != true)
			{
				tokTo = AppCommon.IntParse(this.得意先範囲指定To);
			}

			if (tokFrom != null && tokTo != null)
			{
				if (tokTo < tokFrom)
				{
					this.ErrorMessage = "取引先コードの範囲が正しくありません。";
					return;
				}
			}


			int[] i取引先List = new int[0];
			if (!string.IsNullOrEmpty(取引先指定))
			{
				string[] 取引先List = 取引先指定.Split(',');
				i取引先List = new int[取引先List.Length];

				for (int i = 0; i < 取引先List.Length; i++)
				{
					string str = 取引先List[i];
					int code;
					if (!int.TryParse(str, out code))
					{
						this.ErrorMessage = "取引先指定の形式が不正です。";
						return;
					}
					i取引先List[i] = code;
				}
			}
			//Todo: 条件渡し
			//base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMasterList_CSV, this.得意先範囲指定From, this.得意先範囲指定To, this.表示方法, this.表示区分, this.取引区分, i取引先List));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMasterList_CSV, this.得意先範囲指定From, this.得意先範囲指定To, this.表示方法, this.表示区分, this.取引区分, i取引先List, this.取引停止区分));
        }

		public override void OnF8Key(object sender, KeyEventArgs e)
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;

			int? tokFrom = null;
			int? tokTo = null;

			if (base.CheckAllValidation() != true)
			{
				this.ErrorMessage = "入力値にエラーがあります。";
				return;
			}

			if (string.IsNullOrWhiteSpace(this.得意先範囲指定From) != true)
			{
				tokFrom = AppCommon.IntParse(this.得意先範囲指定From);
			}
			if (string.IsNullOrWhiteSpace(this.得意先範囲指定To) != true)
			{
				tokTo = AppCommon.IntParse(this.得意先範囲指定To);
			}

			if (tokFrom != null && tokTo != null)
			{
				if (tokTo < tokFrom)
				{
					this.ErrorMessage = "取引先コードの範囲が正しくありません。";
					return;
				}
			}


            int[] i取引先List = new int[0];
            if (!string.IsNullOrEmpty(取引先指定))
            {
                string[] 取引先List = 取引先指定.Split(',');
                i取引先List = new int[取引先List.Length];

                for (int i = 0; i < 取引先List.Length; i++)
                {
                    string str = 取引先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "取引先指定の形式が不正です。";
                        return;
                    }
                    i取引先List[i] = code;
                }
            }
            //base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMasterList, this.得意先範囲指定From, this.得意先範囲指定To, this.表示方法, this.表示区分, this.取引区分, i取引先List));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMasterList, this.得意先範囲指定From, this.得意先範囲指定To, this.表示方法, this.表示区分, this.取引区分, i取引先List,this.取引停止区分));
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


		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
			if (ucfg != null)
			{
				if (frmcfg == null) { frmcfg = new ConfigMST01010_1(); }
				frmcfg.Top = this.Top;
				frmcfg.Left = this.Left;
				frmcfg.Width = this.Width;
				frmcfg.Height = this.Height;

				ucfg.SetConfigValue(frmcfg);
			}
		}
		#endregion

		void PrintOut()
		{
			if (this.MSTData == null)
			{
				this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
				return;
			}
			if (this.MSTData.Rows.Count == 0)
			{
				this.ErrorMessage = "印刷データがありません。";
				return;
			}
			try
			{
				KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();

				this.MSTData.TableName = "取引先ﾏｽﾀ一覧";
				view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
				view.MakeReport(this.MSTData.TableName, this.rptFilePath, 0, 0, 0);
				view.SetReportData(this.MSTData);

				view.PrinterName = frmcfg == null ? string.Empty : frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;
			}
			catch (Exception ex)
			{
				this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
				appLog.Error("取引先ﾏｽﾀ一覧の印刷時に例外が発生しました。", ex);
			}
		}

		#region CSVファイル処理
		/// <summary>
		/// CSVファイル出力
		/// </summary>
		/// <param name="tbl"></param>
		private void OutPutCSV(DataTable tbl)
		{
			System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
			//はじめに表示されるフォルダを指定する
			sfd.InitialDirectory = @"C:\";
			//[ファイルの種類]に表示される選択肢を指定する
			sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
			//「CSVファイル」が選択されているようにする
			sfd.FilterIndex = 1;
			//タイトルを設定する
			sfd.Title = "保存先のファイルを選択してください";
			//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			sfd.RestoreDirectory = true;
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				//CSVファイル出力
				CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
				MessageBox.Show("CSVファイルの出力が完了しました。");
			}
		}
		#endregion
	
	}
}
