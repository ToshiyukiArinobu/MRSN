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
using KyoeiSystem.Framework.Windows.Controls;
using System.Reflection;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 請求書デザイン画面
	/// </summary>
	public partial class MST90010 : WindowReportBase
	{
		private const string GET_CNTL = "M87_CNTL";
		private const string GET_RPT = "M99_RPT_GET";
		private const string PUT_RPT = "M99_RPT_UPD";

		#region 画面設定項目
		/// <summary>
		/// ユーザ設定項目
		/// </summary>
		public UserConfig ucfg = null;

		//<summary>
		//画面固有設定項目のクラス定義
		//※ 必ず public で定義する。
		//</summary>
		public class ConfigMST90010 : FormConfigBase
		{
			public FormConfigBase rptDesignForm = null;
		}

		/// ※ 必ず public で定義する。
		public ConfigMST90010 frmcfg = null;

		#endregion

		#region プレビュー表示用請求書データ
		public class W_TKS01010_02_Member
		{
			public string 端末ID { get; set; }
			public int 連番 { get; set; }
			public int H_得意先ID { get; set; }
			public string H_取引先名1 { get; set; }
			public string H_取引先名2 { get; set; }
			public int? H_T締日 { get; set; }
			public string H_郵便番号 { get; set; }
			public string H_住所1 { get; set; }
			public string H_住所2 { get; set; }
			public string H_電話番号 { get; set; }
			public string H_FAX { get; set; }
			public int? H_自社ID { get; set; }
			public string H_自社名 { get; set; }
			public string H_代表者名 { get; set; }
			public string H_自社郵便番号 { get; set; }
			public string H_自社住所1 { get; set; }
			public string H_自社住所2 { get; set; }
			public string H_自社電話番号 { get; set; }
			public string H_自社FAX { get; set; }
            // 追加 ST
            public string H_控え { get; set; }
            public byte[] H_ロゴ画像 { get; set; }
            // 追加 ST
            public int? H_当月請求合計 { get; set; }
			public int? H_当月売上額 { get; set; }
			public int? H_当月通行料 { get; set; }
			public int? H_当月課税金額 { get; set; }
			public int? H_当月非課税金額 { get; set; }
			public int? H_当月消費税 { get; set; }
			public int? H_前月繰越額 { get; set; }
			public int? H_当月入金額 { get; set; }
			public int? H_当月入金調整額 { get; set; }
			public int? H_差引繰越額 { get; set; }
			public string H_振込銀行1 { get; set; }
			public string H_振込銀行2 { get; set; }
			public string H_振込銀行3 { get; set; }
			public DateTime? D_請求日付 { get; set; }
			public decimal? D_配送時間 { get; set; }
			public string D_得意先略称名 { get; set; }
			public int? D_請求内訳ID { get; set; }
			public string D_請求内訳名 { get; set; }
			public int? D_車輌ID { get; set; }
			public string D_車種名 { get; set; }
			public string D_支払先略称名 { get; set; }
			public string D_乗務員名 { get; set; }
			public string D_車輌番号 { get; set; }
			public string D_支払先名2次 { get; set; }
			public string D_実運送乗務員 { get; set; }
			public string D_乗務員連絡先 { get; set; }
			public decimal? D_数量 { get; set; }
			public string D_単位 { get; set; }
			public decimal? D_重量 { get; set; }
			public int? D_走行KM { get; set; }
			public int? D_実車KM { get; set; }
			public decimal? D_売上単価 { get; set; }
			public int? D_売上金額 { get; set; }
			public int? D_通行料 { get; set; }
			public int? D_請求割増1 { get; set; }
			public int? D_請求割増2 { get; set; }
			public int? D_請求消費税 { get; set; }
			public int? D_売上金額計1 { get; set; }
			public int? D_売上金額計2 { get; set; }
			public int? D_売上金額計3 { get; set; }
			public int? D_社内区分 { get; set; }
			public int? D_請求税区分 { get; set; }
			public int? D_商品ID { get; set; }
			public string D_商品名 { get; set; }
			public int? D_発地ID { get; set; }
			public string D_発地名 { get; set; }
			public int? D_着地ID { get; set; }
			public string D_着地名 { get; set; }
			public int? D_請求摘要ID { get; set; }
			public string D_請求摘要 { get; set; }
			public int? D_社内備考ID { get; set; }
			public string D_社内備考 { get; set; }
			public int 請求税区分 { get; set; }
			public int Ｔ税区分ID { get; set; }
			public int 請求書区分 { get; set; }

		}
		#endregion

		string WarimasiName1 = string.Empty;
		string WarimasiName2 = string.Empty;

		DataTable reporttbl = null;

		#region
		/// <summary>
		/// 帳票デザイン機能
		/// </summary>
		public MST90010()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion

		#region LOADイベント
		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MST90010_Loaded(object sender, RoutedEventArgs e)
		{

			#region 設定項目取得
			ucfg = AppCommon.GetConfig(this);
			frmcfg = (ConfigMST90010)ucfg.GetConfigValue(typeof(ConfigMST90010));
			if (frmcfg == null)
			{
				frmcfg = new ConfigMST90010();
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
			}
			#endregion

			base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CNTL, 1, 0));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_RPT));

		}
		#endregion

		#region エラー受信
		/// <summary>
		/// データアクセスエラー受信イベント
		/// </summary>
		/// <param name="message"></param>
		public override void OnReveivedError(CommunicationObject message)
		{
			// 基底クラスのエラー受信イベントを呼び出します。
			base.OnReveivedError(message);
		}
		#endregion

		#region データ受信メソッド
		/// <summary>
		/// 取得データの正常受信時のイベント
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
				case GET_CNTL:
					this.WarimasiName1 = AppCommon.GetWarimasiName1(tbl);
					this.WarimasiName2 = AppCommon.GetWarimasiName2(tbl);
					break;
				case GET_RPT:
					GetReportFile(tbl);
					break;
				case PUT_RPT:
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region リボン

		/// <summary>
		/// F8 リボン　印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF8Key(object sender, KeyEventArgs e)
		{
		}

		//リボン
		public override void OnF11Key(object sender, KeyEventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Mindoow_Closed
		//画面が閉じられた時、データを保持する
		private void MainWindow_Closed(object sender, EventArgs e)
		{
			frmcfg.Top = this.Top;
			frmcfg.Left = this.Left;
			frmcfg.Height = this.Height;
			frmcfg.Width = this.Width;
			ucfg.SetConfigValue(frmcfg);
		}
		#endregion

		private void GetReportFile(DataTable tbl)
		{
			foreach (DataRow row in tbl.Rows)
			{
				string filepath = string.Format(@"{0}{1}.rpt", AppConst.BillReportFileBase, row["帳票ID"]);
				byte[] dat = (byte[])row["レポート定義データ"];
				FileStream filest = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				filest.Write(dat, 0, dat.Length);
				filest.Close();
			}
		}

		private void PutReportFile(string filepath, bool initialized)
		{
			FileInfo fi = new FileInfo(filepath);
			FileStream filest = fi.OpenRead();
			byte[] dat = new byte[filest.Length];
			filest.Read(dat, 0, dat.Length);
			filest.Close();
			base.SendRequest(new CommunicationObject(MessageType.RequestData, PUT_RPT, fi.Name.Replace(fi.Extension, ""), filepath, initialized ? null : dat));
		}

		#region デザイナー画面
		/// <summary>
		/// プレビュー画面表示
		/// </summary>
		/// <param name="tbl"></param>
		private void DispPreviw(string rptfile, ReportDesignParameter rptprm, DataTable tbl)
		{
			try
			{
				while (true)
				{
					//印刷処理
					KyoeiSystem.Framework.Reports.Preview.ReportPreviewWPF view = new KyoeiSystem.Framework.Reports.Preview.ReportPreviewWPF();
					if (frmcfg.rptDesignForm == null)
					{
						frmcfg.rptDesignForm = new FormConfigBase() { Top = this.Top, Left = this.Left, Width = this.Width, Height = this.Height, };
					}
					view.Top = frmcfg.rptDesignForm.Top;
					view.Left = frmcfg.rptDesignForm.Left;
					view.Width = frmcfg.rptDesignForm.Width;
					view.Height = frmcfg.rptDesignForm.Height;
					//第1引数　帳票タイトル
					//第2引数　帳票ファイルPass
					//第3以上　帳票の開始点(0で良い)
					view.MakeReport("請求書デザイン", rptfile, rptprm.marginLeft, rptprm.marginTop, rptprm.PageRow1);
					view.SetReportData(tbl);
					view.SetupParmeters(
						new List<Framework.Reports.Preview.ReportParameter>()
						{
							new Framework.Reports.Preview.ReportParameter(){ PNAME="割増名1", VALUE=(this.WarimasiName1)},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="割増名2", VALUE=(this.WarimasiName2)},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="出力日付", VALUE=("9999/99/99")},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="請求対象期間From", VALUE=("9999/99/99")},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="請求対象期間To", VALUE=("9999/99/99")},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="締日", VALUE=("99")},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="行数１", VALUE=(rptprm.PageRow1)},
							new Framework.Reports.Preview.ReportParameter(){ PNAME="行数２", VALUE=(rptprm.PageRow2)},
						}
						);
					bool? ret = view.ShowDesigner();
					frmcfg.rptDesignForm.Top = view.Top;
					frmcfg.rptDesignForm.Left = view.Left;
					frmcfg.rptDesignForm.Width = view.Width;
					frmcfg.rptDesignForm.Height = view.Height;
					if (ret == null)
						continue;
					if (view.IsReload)
						continue;
					if (ret == false)
						break;
					if (view.IsSaved)
					{
						// IsInitialRecovered はインストール時の状態に戻すかどうかのスイッチ
						//   true の場合 ：インストール時の定義に戻す（M99_RPTから削除する）
						//   false の場合：変更内容を保存する（M99_RPTに登録する）
						PutReportFile(rptfile, view.IsInitialRecovered);
					}
					break;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		private void btnSeikyusho1_Click(object sender, RoutedEventArgs e)
		{
			var rptprm = AppCommon.GetBillreportConfig("A");
			DispPreviw(AppConst.rptBill_A, rptprm, MakeDummyData(rptprm));
		}

		private void btnSeikyusho2_Click(object sender, RoutedEventArgs e)
		{
			var rptprm = AppCommon.GetBillreportConfig("B");
			DispPreviw(AppConst.rptBill_B, rptprm, MakeDummyData(rptprm));
		}

		private void btnSeikyusho3_Click(object sender, RoutedEventArgs e)
		{
			var rptprm = AppCommon.GetBillreportConfig("T");
			DispPreviw(AppConst.rptBill_T, rptprm, MakeDummyData(rptprm));
		}

		private void btnSeikyusho4_Click(object sender, RoutedEventArgs e)
		{
			var rptprm = AppCommon.GetBillreportConfig("K");
			DispPreviw(AppConst.rptBill_K, rptprm, MakeDummyData(rptprm));
		}

		private void btnSeikyusho5_Click(object sender, RoutedEventArgs e)
		{
			var rptprm = AppCommon.GetBillreportConfig("C");
			DispPreviw(AppConst.rptBill_C, rptprm, MakeDummyData(rptprm));
		}

		private DataTable MakeDummyData(ReportDesignParameter rptprm)
		{
			string X80 = new string('X', 80);
			string Ｎ40 = new string('Ｎ', 40);
			W_TKS01010_02_Member dat = new W_TKS01010_02_Member()
			{
				端末ID = X80,
				連番 = 99999999,
				H_得意先ID = 99999999,
				H_取引先名1 = Ｎ40,
				H_取引先名2 = Ｎ40,
				H_T締日 = 99999999,
				H_郵便番号 = X80,
				H_住所1 = Ｎ40,
				H_住所2 = Ｎ40,
				H_電話番号 = X80,
				H_FAX = X80,
				H_自社ID = 99999999,
				H_自社名 = Ｎ40,
				H_代表者名 = Ｎ40,
				H_自社郵便番号 = X80,
				H_自社住所1 = Ｎ40,
				H_自社住所2 = Ｎ40,
				H_自社電話番号 = X80,
				H_自社FAX = X80,
                // 追加
                H_ロゴ画像 = null,
				H_当月請求合計 = 999999999,
				H_当月売上額 = 999999999,
				H_当月通行料 = 999999999,
				H_当月課税金額 = 999999999,
				H_当月非課税金額 = 999999999,
				H_当月消費税 = 999999999,
				H_前月繰越額 = 999999999,
				H_当月入金額 = 999999999,
				H_当月入金調整額 = 999999999,
				H_差引繰越額 = 999999999,
				H_振込銀行1 = Ｎ40,
				H_振込銀行2 = Ｎ40,
				H_振込銀行3 = Ｎ40,
				D_請求日付 = DateTime.Today,
				D_配送時間 = 999.99m,
				D_得意先略称名 = Ｎ40,
				D_請求内訳ID = 9999999,
				D_請求内訳名 = Ｎ40,
				D_車輌ID = 99999999,
				D_車種名 = Ｎ40,
				D_支払先略称名 = Ｎ40,
				D_乗務員名 = Ｎ40,
				D_車輌番号 = X80,
				D_支払先名2次 = Ｎ40,
				D_実運送乗務員 = Ｎ40,
				D_乗務員連絡先 = X80,
				D_数量 = 999999.99m,
				D_単位 = X80,
				D_重量 = 999999.999m,
				D_走行KM = 99999999,
				D_実車KM = 99999999,
				D_売上単価 = 999999.99m,
				D_売上金額 = 99999999,
				D_通行料 = 99999999,
				D_請求割増1 = 99999999,
				D_請求割増2 = 99999999,
				D_請求消費税 = 99999999,
				D_売上金額計1 = 99999999,
				D_売上金額計2 = 99999999,
				D_売上金額計3 = 99999999,
				D_社内区分 = 999,
				D_請求税区分 = 999,
				D_商品ID = 99999999,
				D_商品名 = Ｎ40,
				D_発地ID = 99999999,
				D_発地名 = Ｎ40,
				D_着地ID = 99999999,
				D_着地名 = Ｎ40,
				D_請求摘要ID = 99999999,
				D_請求摘要 = Ｎ40,
				D_社内備考ID = 99999999,
				D_社内備考 = Ｎ40,
				請求税区分 = 9999,
				Ｔ税区分ID = 1,
				請求書区分 = 9999,

			};
			List<W_TKS01010_02_Member> list = new List<W_TKS01010_02_Member>();
			for (int i = 0; i < (rptprm.PageRow1+rptprm.PageRow2); i++)
			{
				list.Add(dat);
			}
			DataTable tbl = new DataTable();
			AppCommon.ConvertToDataTable(list, tbl);

			return tbl;
		}


	}
}
