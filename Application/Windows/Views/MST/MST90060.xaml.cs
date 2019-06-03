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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Reports.Preview;
using KyoeiSystem.Framework.Windows.ViewBase;

using System.IO;
using System.Windows.Resources;
using Microsoft.Win32;
using GrapeCity.Windows.SpreadGrid;
using GrapeCity.Windows.SpreadGrid.Editors;

namespace KyoeiSystem.Application.Windows.Views
{
	public class COLS
	{
		public string name { get; set; }
		public string systype { get; set; }
		public string avalue { get; set; }
	}

	public class TORIKOMI_SETTEI
	{
		public string systype { get; set; }
		public string table_name { get; set; }
		public int wariate { get; set; }
		public string kotei { get; set; }
		public string setumei { get; set; }
	}

	public class MST90060_TOK
	{
		public int 得意先KEY { get; set; }
		public int 得意先ID { get; set; }
		public string 略称名 { get; set; }
		public string 得意先名１ { get; set; }
		public string 得意先名２ { get; set; }
		public int 取引区分 { get; set; }
	}

	public class MST90060_DRV
	{
		public int 乗務員KEY { get; set; }
		public int 乗務員ID { get; set; }
		public string 乗務員名 { get; set; }
	}

	public class MST90060_CAR
	{
		public int 車輌KEY { get; set; }
		public int 車輌ID { get; set; }
		public string 車輌番号 { get; set; }
	}



	/// <summary>
	/// CSVデータ取込画面
	/// 20150714 wada 変更着手
	/// </summary>
	public partial class MST90060 : WindowReportBase
	{
		class ByteValidator : CellValidator
		{
			private int _maxBytes;
			public ByteValidator(int maxBytes, string message)
			{
				this._maxBytes = maxBytes;
				this.ErrorMessage = message;
			}
			public override bool IsValid(ICalcEvaluator evaluator, int rowIndex, int columnIndex, IActualValue actualValue)
			{
				object value = actualValue.GetValue();
				string text = value == null ? "" : value.ToString();
				int byteCount = Encoding.GetEncoding(932).GetByteCount(text); // 932 = Shift-JIS
				return byteCount <= this._maxBytes;
			}
		}

		#region 画面設定項目
		/// <summary>
		/// ユーザ設定項目
		/// </summary>
		UserConfig ucfg = null;
		#region "権限関係"
		CommonConfig ccfg = null;
		#endregion

		/// <summary>
		/// 画面固有設定項目のクラス定義
		/// ※ 必ず public で定義する。
		/// </summary>
		public class ConfigMST90060 : FormConfigBase
		{

			#region 取込保存
			// csv設定
			public List<TORIKOMI_SETTEI> 取込保存1 = null;
			public List<TORIKOMI_SETTEI> 取込保存2 = null;
			public List<TORIKOMI_SETTEI> 取込保存3 = null;
			public List<TORIKOMI_SETTEI> 取込保存4 = null;
			#endregion
		}

		/// ※ 必ず public で定義する。
		public ConfigMST90060 frmcfg = null;

		#endregion

		#region 定数
		private const string MST90020_TOK = "MST90020_TOK";
		private const string MST90020_CAR = "MST90020_CAR";
		private const string MST90020_DRV = "MST90020_DRV";
		private const string SEARCH_MST90022_00 = "SEARCH_MST90022_00"; //TRNT
		// 20150723 wada add
		private const string MST90020_TIK = "MST90020_TIK";     // 発着地ID
		private const string MST90020_HIN = "MST90020_HIN";     // 商品ID
		private const string MST90020_SYA = "MST90020_SYA";     // 車種ID
		private const string MST90020_TEK = "MST90020_TEK";     // 摘要ID

		private const string SEARCH_MST900601 = "SEARCH_MST900601";
		private const string SEARCH_MST90020 = "SEARCH_MST90020";
		private const string SEARCH_MST90060_00 = "SEARCH_MST90060_00";
		private const string SEARCH_MST90060_01 = "SEARCH_MST90060_01";
		private const string SEARCH_MST90060_02 = "SEARCH_MST90060_02";
		private const string SEARCH_MST90060_03 = "SEARCH_MST90060_03";

		#endregion

		#region Binding

		OpenFileDialog opendiag;
		DataTable Toktable;
		DataTable Drvtable;
		DataTable Cartable;

		// 20150723 wada add
		DataTable Tiktable;
		DataTable Hintable;
		DataTable Syatable;
		DataTable Tektable;
		DataTable CSVデータ;

		private List<TORIKOMI_SETTEI> _取込設定;
		public List<TORIKOMI_SETTEI> 取込設定
		{
			get { return _取込設定; }
			set { _取込設定 = value; NotifyPropertyChanged(); }
		}

		private List<COLS> _Table_column = null;
		public List<COLS> Table_column
		{
			get { return _Table_column; }
			set { _Table_column = value; NotifyPropertyChanged(); }
		}

		private DataTable _取込データ = null;
		public DataTable 取込データ
		{
			get { return this._取込データ; }
			set
			{
				this._取込データ = value;
				NotifyPropertyChanged();
			}
		}

		private DataTable _CSV設定データ = null;
		public DataTable CSV設定データ
		{
			get { return this._CSV設定データ; }
			set
			{
				this._CSV設定データ = value;
				NotifyPropertyChanged();
			}
		}

		private string _DialogPass = string.Empty;
		public string DialogPass
		{
			get { return this._DialogPass; }
			set { this._DialogPass = value; NotifyPropertyChanged(); }
		}

		private DataTable _DBData = null;
		public DataTable Database
		{
			get { return this._DBData; }
			set { this._DBData = value; NotifyPropertyChanged(); }
		}

		private Boolean _明細番号自動 = false;
		public Boolean 明細番号自動
		{
			get { return this._明細番号自動; }
			set { this._明細番号自動 = value; NotifyPropertyChanged(); }
		}

		// 20150723 wada add 各IDをカンマで区切った文字列を格納する用
		private string ToktableKEY = string.Empty;
		private string TiktableID = string.Empty;
		private string HintableID = string.Empty;
		private string SyatableID = string.Empty;
		private string TektableID = string.Empty;
		private string DateYMrange = string.Empty;  // 年月(yyyymm)

		// 20150724 wada add 乗務員KEY, 車輌KEYも追加
		private string DrvtableKEY = string.Empty;
		private string CartableKEY = string.Empty;

		// 20150723 wada add 主キーチェック用の変数を追加する。
		List<string> targetKey;

		// 20150730 wada add 文字列のフィールドを特定するための変数を追加する。
		List<int> stringColumn;

		#endregion

		#region MST90090()
		/// <summary>
		/// 得意先売上合計表
		/// </summary>
		public MST90060()
		{
			InitializeComponent();
			this.DataContext = this;

			// 20150716 wada commentout
			////CSVファイル取り込みダイアログ
			opendiag = new OpenFileDialog();
			opendiag.Filter = "CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";
		}
		#endregion

		#region LOADイベント
		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			ScreenClear();
			F9.IsEnabled = false;
			AppCommon.SetutpComboboxList(this.TableName, false);

			base.SendRequest(new CommunicationObject(MessageType.UpdateData, MST90020_TOK, new object[] { }));
			base.SendRequest(new CommunicationObject(MessageType.UpdateData, MST90020_DRV, new object[] { }));
			base.SendRequest(new CommunicationObject(MessageType.UpdateData, MST90020_CAR, new object[] { }));

			// 20150723 wada add 発着地、商品、車種、摘要の登録済IDを取得する。
			base.SendRequest(new CommunicationObject(MessageType.RequestData, MST90020_TIK, new object[] { }));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, MST90020_HIN, new object[] { }));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, MST90020_SYA, new object[] { }));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, MST90020_TEK, new object[] { }));

			// 20150723 wada add 年月の範囲をセットしておく。
			for (int i = 1901; i <= 2100; i++)
			{
				for (int j = 1; j <= 12; j++)
				{
					DateYMrange += (DateYMrange.Length == 0) ? i.ToString() + string.Format("{0:00}", j) : "," + i.ToString() + string.Format("{0:00}", j);
				}
			}

			#region 設定項目取得
			ucfg = AppCommon.GetConfig(this);
			ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

			#region "権限関係"
			// 登録ボタン設定
			if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
			{
				// RibbonWindowViewBaseのプロパティに設定
				DataUpdateVisible = System.Windows.Visibility.Hidden;
			}
			#endregion
			frmcfg = (ConfigMST90060)ucfg.GetConfigValue(typeof(ConfigMST90060));
			if (frmcfg == null)
			{
				frmcfg = new ConfigMST90060();
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

		#region 画面初期化
		/// <summary>
		/// 画面初期化処理
		/// </summary>
		private void ScreenClear()
		{
			this.Cursor = Cursors.Arrow;

			CsvData.Reset();
			CsvData.BeginInit();
			CsvData.EndInit();

			// 20150716 wada add 
			CsvData.ColumnCount = 400;
			CsvData.RowCount = 200;

			取込データ = null;
			取込設定 = new List<TORIKOMI_SETTEI>();

			// 20150730 wada add
			targetKey = new List<string>();
			stringColumn = new List<int>();

			F9.IsEnabled = false;
			F5.IsEnabled = false;
			ChangeKeyItemChangeable(true);
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

				case SEARCH_MST90022_00:
					Table_column = (List<COLS>)AppCommon.ConvertFromDataTable(typeof(List<COLS>), tbl);

					var a = new List<TORIKOMI_SETTEI>();
					foreach (DataRow row in tbl.Rows)
					{
						a.Add(new TORIKOMI_SETTEI
						{
							systype = row["systype"].ToString(),
							kotei = "",
							table_name = row["name"].ToString(),
							wariate = 0,
							setumei = row["avalue"].ToString(),

						});
					}

					int cnt = 0;
					switch (TableName.SelectedIndex)
					{
					case 0:
						cnt = 0;
						if (frmcfg.取込保存1 != null)
						{
							foreach (var row in a)
							{
								if (frmcfg.取込保存1.Count > cnt)
								{
									row.wariate = frmcfg.取込保存1[cnt].wariate;
									row.kotei = frmcfg.取込保存1[cnt].kotei;
								}
								cnt++;
							}
						}
						break;
					case 1:
						cnt = 0;
						if (frmcfg.取込保存2 != null)
						{
							foreach (var row in a)
							{
								if (frmcfg.取込保存2.Count > cnt)
								{
									row.wariate = frmcfg.取込保存2[cnt].wariate;
									row.kotei = frmcfg.取込保存2[cnt].kotei;
								}
								cnt++;
							}
						}
						break;
					case 2:
						cnt = 0;
						if (frmcfg.取込保存3 != null)
						{
							foreach (var row in a)
							{
								if (frmcfg.取込保存3.Count > cnt)
								{
									row.wariate = frmcfg.取込保存3[cnt].wariate;
									row.kotei = frmcfg.取込保存3[cnt].kotei;
								}
								cnt++;
							}
						}
						break;
					case 3:
						cnt = 0;
						if (frmcfg.取込保存4 != null)
						{
							foreach (var row in a)
							{
								if (frmcfg.取込保存4.Count > cnt)
								{
									row.wariate = frmcfg.取込保存4[cnt].wariate;
									row.kotei = frmcfg.取込保存4[cnt].kotei;
								}
								cnt++;
							}
						}
						break;

					}



					取込設定 = a;

					break;

				case MST90020_TOK:
					Toktable = tbl;

					// 20150723 wada add 得意先IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					ToktableKEY = string.Empty;
					foreach (DataRow dr in Toktable.Rows)
					{
						string tempRec = dr["得意先ID"].ToString();
						ToktableKEY += (ToktableKEY.Length == 0) ? tempRec : "," + tempRec;
					}

					break;
				case MST90020_DRV:
					Drvtable = tbl;

					// 20150724 wada add 乗務員IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					DrvtableKEY = string.Empty;
					foreach (DataRow dr in Drvtable.Rows)
					{
						string tempRec = dr["乗務員ID"].ToString();
						DrvtableKEY += (DrvtableKEY.Length == 0) ? tempRec : "," + tempRec;
					}

					break;
				case MST90020_CAR:

					Cartable = tbl;
					// 20150724 wada add 車輌IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					CartableKEY = string.Empty;
					foreach (DataRow dr in Cartable.Rows)
					{
						string tempRec = dr["車輌ID"].ToString();
						CartableKEY += (CartableKEY.Length == 0) ? tempRec : "," + tempRec;
					}

					break;

				// 20150723 wada add
				case MST90020_TIK:
					Tiktable = tbl;

					// 発着地IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					TiktableID = string.Empty;
					foreach (DataRow dr in Tiktable.Rows)
					{
						string tempRec = dr["発着地ID"].ToString();
						TiktableID += (TiktableID.Length == 0) ? tempRec : "," + tempRec;
					}
					break;
				case MST90020_HIN:
					Hintable = tbl;

					// 商品IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					HintableID = string.Empty;
					foreach (DataRow dr in Hintable.Rows)
					{
						string tempRec = dr["商品ID"].ToString();
						HintableID += (HintableID.Length == 0) ? tempRec : "," + tempRec;
					}
					break;
				case MST90020_SYA:
					Syatable = tbl;

					// 車種IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					SyatableID = string.Empty;
					foreach (DataRow dr in Syatable.Rows)
					{
						string tempRec = dr["車種ID"].ToString();
						SyatableID += (SyatableID.Length == 0) ? tempRec : "," + tempRec;
					}
					break;
				case MST90020_TEK:
					Tektable = tbl;

					// 摘要IDをカンマ区切りで格納する。
					// あとでSpreadのValidationチェックで使用する。
					TektableID = string.Empty;
					foreach (DataRow dr in Tektable.Rows)
					{
						string tempRec = dr["摘要ID"].ToString();
						TektableID += (TektableID.Length == 0) ? tempRec : "," + tempRec;
					}
					break;

				//検索結果取得時
				// 20150716 wada modify case文の同一処理をまとめる。
				case SEARCH_MST90060_00://T01_TRNファイル
				case SEARCH_MST90060_01://T02_UTRNファイル
				case SEARCH_MST90060_02://T03_KTRNファイル
				case SEARCH_MST90060_03://T04_NYUKファイル
					Database = tbl;
					F9.IsEnabled = true;
					SetData();
					break;

				case SEARCH_MST900601:

					// 20150716 wada add
					// 取り込みデータのエラーをチェックして、登録されたデータがあるか確認する。
					// それによりメッセージの内容を変更する。
					string msg = "登録可能なデータはありませんでした。";
					foreach (DataRow t in 取込データ.Rows)
					{
						if (!t.HasErrors)
						{
							this.Cursor = Cursors.Arrow;
							msg = "登録が完了しました。";
							break;
						}
					}

					// 20150716 wada modify 取り込みデータをクリアする。
					取込データ = null;

					// 20150716 wada modify
					MessageBox.Show(msg);

					F9.IsEnabled = false;
					ScreenClear();
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region SetData()

		void SetData()
		{
			F5.IsEnabled = true;

			// 20150723 wada comment
			// キーの妥当性チェックに関しては、
			// 得意先KEY、発着地ID、商品ID、車種ID、摘要IDの５つを対象とする。

			// 20150723 wada add CsvData.Columnsを一時的に割り当てる変数を追加する。
			var sp = this.CsvData.Columns;

			// 20150723 wada comment このswitch文ではCellValidatorのみを設定する。
			// 20150730 wada modify index→valueに変更
			//switch (TableName.SelectedIndex)
			switch ((int)TableName.SelectedValue)
			{
			case 0:
				#region T01_TRNファイル
				if (明細番号自動)
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999);        //明細番号 		        INT		
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999);        //明細行   		        INT
				}
				else
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細番号 		        INT		
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細行   		        INT
				}
				sp[2].CellValidator = SetValidatorToDate();                             //登録日時  		    DATETIME	    ✔
				sp[3].CellValidator = SetValidatorToDate();                             //更新日時  		    DATETIME	    ✔
				sp[4].CellValidator = SetValidatorToNumberRange(0, 1);                  //明細区分  		    INT
				sp[5].CellValidator = SetValidatorToNumberRange(0, 4);                  //入力区分  		    INT
				sp[6].CellValidator = SetValidatorToDate();                             //請求日付  		    DATE
				sp[7].CellValidator = SetValidatorToDate();                             //支払日付  		    DATE		    ✔
				sp[8].CellValidator = SetValidatorToDate();                             //配送日付  		    DATE
				sp[9].CellValidator = SetValidatorToNumberDigit((decimal)99999.99,
					"9999.99までの時間を入力して下さい。");                             //配送時間  		    DECIMAL(6,2)	✔
				sp[10].CellValidator = SetValidatorToStringExists(ToktableKEY);         //得意先KEY    	        INT		        ✔
				sp[11].CellValidator = SetValidatorToNumberDigit(9999999);              //請求内訳ID    	    INT		        ✔
				sp[12].CellValidator = SetValidatorToStringExists(CartableKEY);         //車輌KEY 		        INT		        ✔
				sp[13].CellValidator = SetValidatorToNumberDigit(9999999);              //車種ID  		        INT		        ✔
				sp[14].CellValidator = SetValidatorToStringExists(ToktableKEY);         //支払先KEY    	        INT		        ✔
				sp[15].CellValidator = SetValidatorToStringExists(DrvtableKEY);         //乗務員KEY    	        INT		        ✔
				sp[16].CellValidator = SetValidatorToNumberDigit(9999999);              //自社部門ID    	    INT
				sp[17].CellValidator = SetValidatorToString(6);                         //車輌番号  		    VARCHAR(6)	    ✔
				sp[18].CellValidator = SetValidatorToString(20);                        //支払先名２次    	    VARCHAR(20)	    ✔
				sp[19].CellValidator = SetValidatorToString(20);                        //実運送乗務員    	    VARCHAR(20)	    ✔
				sp[20].CellValidator = SetValidatorToString(15);                        //乗務員連絡先    	    VARCHAR(15)	    ✔
				sp[21].CellValidator = SetValidatorToNumberRange(0, 7);                 //請求運賃計算区分ID    INT
				sp[22].CellValidator = SetValidatorToNumberRange(0, 7);                 //支払運賃計算区分ID    INT
				sp[23].CellValidator = SetValidatorToNumberDigit((decimal)99999999.9,
					"99999999.9までの数量を入力して下さい。", false);                   //数量    		        DECIMAL(9,1)
				sp[24].CellValidator = SetValidatorToString(4);                         //単位    		        VARCHAR(4)	    ✔
				sp[25].CellValidator = SetValidatorToNumberDigit((decimal)999999.999,
					"999999.999までの数量を入力して下さい。", false);                   //重量    		        DECIMAL(9,3)
				sp[26].CellValidator = SetValidatorToNumberDigit(999999999, false);     //走行ＫＭ  		    INT
				sp[27].CellValidator = SetValidatorToNumberDigit(999999999, false);     //実車ＫＭ  		    INT
				sp[28].CellValidator = SetValidatorToNumberDigit((decimal)99999.99,
					"999999.99までの時間を入力して下さい。", false);                    //待機時間  		    DECIMAL(8,2)
				sp[29].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
					"9999.99までの単価を入力して下さい。", false);                      //売上単価  		    DECIMAL(11,2)
				sp[30].CellValidator = SetValidatorToNumberDigit(999999999, false);     //売上金額  		    INT
				sp[31].CellValidator = SetValidatorToNumberDigit(999999999, false);     //通行料   		        INT
				sp[32].CellValidator = SetValidatorToNumberDigit(999999999, false);     //請求割増１ 	        INT
				sp[33].CellValidator = SetValidatorToNumberDigit(999999999, false);     //請求割増２ 	        INT
				sp[34].CellValidator = SetValidatorToNumberDigit(999999999, false);     //請求消費税 	        INT
				sp[35].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
					"9999.99までの単価を入力して下さい。", false);                      //支払単価  		    DECIMAL(11,2)
				sp[36].CellValidator = SetValidatorToNumberDigit(999999999, false);     //支払金額  		    INT
				sp[37].CellValidator = SetValidatorToNumberDigit(999999999, false);     //支払通行料 	        INT
				sp[38].CellValidator = SetValidatorToNumberDigit(999999999, false);     //支払割増１		    INT
				sp[39].CellValidator = SetValidatorToNumberDigit(999999999, false);     //支払割増２		    INT
				sp[40].CellValidator = SetValidatorToNumberDigit(999999999, false);     //支払消費税		    INT
				sp[41].CellValidator = SetValidatorToNumberDigit(999999999, false);     //水揚金額		        INT
				sp[42].CellValidator = SetValidatorToNumberRange(0, 1, false);           //社内区分		        INT
				sp[43].CellValidator = SetValidatorToNumberRange(0, 1);                 //請求税区分		    INT		        ✔
				sp[44].CellValidator = SetValidatorToNumberRange(0, 1, false);           //支払税区分		    INT
				sp[45].CellValidator = SetValidatorToNumberRange(0, 1, false);           //売上未定区分	        INT
				sp[46].CellValidator = SetValidatorToNumberRange(0, 1, false);           //支払未定区分	        INT
				sp[47].CellValidator = SetValidatorToNumberDigit(999999999);            //商品ID		        INT		        ✔
				sp[48].CellValidator = SetValidatorToString(30);                        //商品名		        VARCHAR(30)	    ✔
				sp[49].CellValidator = SetValidatorToNumberDigit(9999999);              //発地ID		        INT		        ✔
				sp[50].CellValidator = SetValidatorToString(30);                        //発地名		        VARCHAR(30)	    ✔
				sp[51].CellValidator = SetValidatorToNumberDigit(9999999);              //着地ID		        INT		        ✔
				sp[52].CellValidator = SetValidatorToString(30);                        //着地名		        VARCHAR(30)	    ✔
				sp[53].CellValidator = SetValidatorToNumberDigit(9999999);              //請求摘要ID		    INT		        ✔
				sp[54].CellValidator = SetValidatorToString(30);                        //請求摘要		        VARCHAR(30)	    ✔
				sp[55].CellValidator = SetValidatorToNumberDigit(9999999);              //社内備考ID		    INT		        ✔
				sp[56].CellValidator = SetValidatorToString(30);                        //社内備考		        VARCHAR(30)	    ✔
				sp[57].CellValidator = SetValidatorToNumberDigit(9999999);              //入力者ID		        INT
				sp[58].CellValidator = SetValidatorToNumberRange(0, 1, false);           //確認名称区分	        INT

				//明細番号と明細行のどちらも重複値だった場合は重複エラーとして返す
				targetKey.Add("明細番号");
				targetKey.Add("明細行");
				break;
				#endregion

			case 1:
				#region T02_UTRNファイル
				if (明細番号自動)
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999);        //明細行	            int	
				}
				else
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細行	            int	
				}
				sp[2].CellValidator = SetValidatorToDate();                             //登録日時	            datetime	    ✔
				sp[3].CellValidator = SetValidatorToDate();                             //更新日時	            datetime	    ✔
				sp[4].CellValidator = SetValidatorToNumberRange(0, 1);                  //明細区分	            int
				sp[5].CellValidator = SetValidatorToNumberRange(0, 4);                  //入力区分	            int
				sp[6].CellValidator = SetValidatorToDate();                             //実運行日開始	        date	        ✔
				sp[7].CellValidator = SetValidatorToDate();                             //実運行日終了	        date	        ✔
				sp[8].CellValidator = SetValidatorToStringExists(CartableKEY);          //車輌KEY	            int	            ✔
				sp[9].CellValidator = SetValidatorToStringExists(DrvtableKEY);          //乗務員KEY	            int	            ✔
				sp[10].CellValidator = SetValidatorToNumberDigit(9999999);              //車種ID	            int	            ✔
				sp[11].CellValidator = SetValidatorToString(7);                         //車輌番号	            varchar(6)	    ✔
				sp[12].CellValidator = SetValidatorToNumberDigit(9999999);              //自社部門ID	        int	            ✔
				sp[13].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                            //出庫時間	            decimal(6, 2)	✔
				sp[14].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                             //帰庫時間	            decimal(6, 2)	✔
				sp[15].CellValidator = SetValidatorToNumberDigit(9999999);              //出勤区分ID	        int
				sp[16].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                             //拘束時間	            decimal(6, 2)	✔
				sp[17].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                             //運転時間	            decimal(6, 2)	✔
				sp[18].CellValidator = SetValidatorToNumberDigit((decimal)99.99,
					"99.99までの時間を入力して下さい。");                               //高速時間	            decimal(4, 2)	✔
				sp[19].CellValidator = SetValidatorToNumberDigit((decimal)99.99,
					"99.99までの時間を入力して下さい。");                               //作業時間	            decimal(4, 2)	✔
				sp[20].CellValidator = SetValidatorToNumberDigit((decimal)99.99,
					"99.99までの時間を入力して下さい。");                               //待機時間	            decimal(4, 2)	✔
				sp[21].CellValidator = SetValidatorToNumberDigit((decimal)99.99,
					"99.99までの時間を入力して下さい。");                               //休憩時間	            decimal(4, 2)	✔
				sp[22].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                             //残業時間	            decimal(6, 2)	✔
				sp[23].CellValidator = SetValidatorToNumberDigit((decimal)9999.99,
					"9999.99までの時間を入力して下さい。");                             //深夜時間	            decimal(6, 2)	✔
				sp[24].CellValidator = SetValidatorToNumberDigit(999999999);            //走行ＫＭ	            int	
				sp[25].CellValidator = SetValidatorToNumberDigit(999999999);            //実車ＫＭ	            int	
				sp[26].CellValidator = SetValidatorToNumberDigit((decimal)9999999.9,
					"9999.99までの屯数を入力して下さい。", false);                      //輸送屯数	            decimal(8, 1)	
				sp[27].CellValidator = SetValidatorToNumberDigit(999999999);            //出庫ＫＭ	            int	
				sp[28].CellValidator = SetValidatorToNumberDigit(999999999);            //帰庫ＫＭ	            int	
				sp[29].CellValidator = SetValidatorToString(100);                       //備考	                varchar(100)	✔
				sp[30].CellValidator = SetValidatorToDate();                            //勤務開始日	        date	
				sp[31].CellValidator = SetValidatorToDate();                            //勤務終了日	        date	
				sp[32].CellValidator = SetValidatorToDate();                            //労務日	            date	
				sp[33].CellValidator = SetValidatorToNumberDigit(9999999);              //入力者ID	            int	            ✔

				//明細番号と明細行のどちらも重複値だった場合は重複エラーとして返す
				targetKey.Add("明細番号");
				targetKey.Add("明細行");
				break;
				#endregion

			case 2:
				#region T03_KTRNファイル
				if (明細番号自動)
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999);        //明細行	            int	
				}
				else
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細行	            int	
				}
				sp[2].CellValidator = SetValidatorToDate();                             //登録日時	            datetime	    ✔
				sp[3].CellValidator = SetValidatorToDate();                             //更新日時	            datetime	    ✔
				sp[4].CellValidator = SetValidatorToNumberRange(0, 1);                  //明細区分	            int	
				sp[5].CellValidator = SetValidatorToNumberRange(0, 4);                  //入力区分	            int	
				sp[6].CellValidator = SetValidatorToDate();                             //経費発生日	        date	        ✔
				sp[7].CellValidator = SetValidatorToNumberDigit(9999999);               //車輌ID	            int	            ✔
				sp[8].CellValidator = SetValidatorToString(6);                          //車輌番号	            varchar(6)	    ✔
				sp[9].CellValidator = SetValidatorToNumberDigit(999999999);             //メーター	            int	            ✔
				sp[10].CellValidator = SetValidatorToStringExists(DrvtableKEY);         //乗務員KEY	            int	            ✔
				sp[11].CellValidator = SetValidatorToStringExists(ToktableKEY);             //支払先KEY	            int	            ✔
				sp[12].CellValidator = SetValidatorToNumberDigit(9999999);              //自社部門ID	        int	            ✔
				sp[13].CellValidator = SetValidatorToNumberDigit(9999999);              //経費項目ID	        int	            ✔
				sp[14].CellValidator = SetValidatorToString(20);                        //経費補助名称	        varchar(20)	    ✔
				sp[15].CellValidator = SetValidatorToNumberDigit((decimal)9999999.99,
					"9999999.99までの単価を入力して下さい。", false);                   //単価	                decimal(9, 2)	
				sp[16].CellValidator = SetValidatorToNumberDigit((decimal)9999999.99,
					"9999999.99までの単価を入力して下さい。");                          //内軽油税分	        decimal(9, 2)	✔
				sp[17].CellValidator = SetValidatorToNumberDigit((decimal)99999999.9,
					"9999.99までの数量を入力して下さい。");                             //数量	                decimal(9, 1)	✔
				sp[18].CellValidator = SetValidatorToNumberDigit(999999999);            //金額	                int	            ✔
				sp[19].CellValidator = SetValidatorToNumberRange(0, 1);                 //収支区分	            int	            ✔
				sp[20].CellValidator = SetValidatorToNumberDigit(9999999);              //摘要ID	            int	            ✔
				sp[21].CellValidator = SetValidatorToString(40);                        //摘要名	            varchar(40)	    ✔
				sp[22].CellValidator = SetValidatorToNumberDigit(9999999);              //入力者ID	            int	            ✔

				//明細番号と明細行のどちらも重複値だった場合は重複エラーとして返す
				targetKey.Add("明細番号");
				targetKey.Add("明細行");
				break;
				#endregion

			case 3:
				#region T04_NYUKファイル
				if (明細番号自動)
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999);        //明細行	            int	
				}
				else
				{
					sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細番号	            int	
					sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        //明細行	            int	
				}
				sp[2].CellValidator = SetValidatorToDate();                             //登録日時	            datetime	    ✔
				sp[3].CellValidator = SetValidatorToDate();                             //更新日時	            datetime	    ✔
				sp[4].CellValidator = SetValidatorToNumberRange(2, 3);                  //明細区分	            int	
				sp[5].CellValidator = SetValidatorToDate();                             //入出金日付	        date	        ✔
				sp[6].CellValidator = SetValidatorToStringExists(ToktableKEY, false);    //取引先KEY	            int	
				sp[7].CellValidator = SetValidatorToNumberRange(1, 9, false);            //入出金区分	        int	
				sp[8].CellValidator = SetValidatorToNumberDigit(999999999, false);       //入出金金額	        int	
				sp[9].CellValidator = SetValidatorToNumberDigit(9999999);               //摘要ID	            int	            ✔
				sp[10].CellValidator = SetValidatorToString(30);                        //摘要名	            varchar(30)	    ✔
				sp[11].CellValidator = SetValidatorToDate();                            //手形日付	            date	        ✔
				sp[12].CellValidator = SetValidatorToNumberDigit(9999999);              //入力者ID	            int	            ✔

				//明細番号と明細行のどちらも重複値だった場合は重複エラーとして返す
				targetKey.Add("明細番号");
				targetKey.Add("明細行");

				break;
				#endregion

			}
		#endregion

			#region DataErrorCheck

			// チェックするタイプが文字列の場合、文字列チェックではバイトで判定できないため、
			// TextCellTypeのMaxLengthを使用する。
			try
			{
				for (int col = 0; col < this.CsvData.ColumnCount; col++)
				{
					if (sp[col].CellValidator.CriteriaType == CriteriaType.TextLength)
					{
						if (sp[col].CellValidator.ComparisonOperator == ComparisonOperator.LessThan)
						{
							for (int row = 0; row < this.CsvData.RowCount; row++)
							{
								TextCellType tc = new TextCellType();
								tc.MaxLengthUnit = GrapeCity.Windows.SpreadGrid.Editors.LengthUnit.Byte;
								tc.MaxLength = (int)sp[col].CellValidator.Value1;
								this.CsvData[row, col].CellType = tc;

							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				// 例外に入ってきた場合は、用意しているフィールド＞CSVのフィールドとなるので無視する。
			}

			// CSVデータのチェックを全て行う。
			CheckCSVDataAll();
		}

		private CellValidator SetValidatorToNumberDigit(long p1, bool p2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 20150723 wada add 日付のValidator
		/// 範囲は9999/12/31までを対象とする。
		/// </summary>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToDate(bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateDateValidator(ComparisonOperator.Between, new DateTime(1, 1, 1), new DateTime(9999, 12, 31), false, "日付を入力してください。");
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 日付のValidator
		/// 月または日付を数値で格納するフィールドを対象とする。
		/// </summary>
		/// <param name="number">12のとき月、31のとき数値のメッセージを表示</param>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToDateNumber(int number, bool nullable = true)
		{
			string msg = string.Empty;
			switch (number)
			{
			case 12:
				msg = "12以下の月を数値で入力してください。";
				break;
			case 31:
				msg = "31以下の日付を数値で入力してください。";
				break;
			default:
				msg = number.ToString() + "以下の数値を入力してください。";
				break;
			}
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateNumberValidator(ComparisonOperator.LessThanOrEqualTo, number, null, true, msg);
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 日付のValidator
		/// 年月(yyyymm)を数値で格納するフィールドを対象とする。
		/// </summary>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToYearMonth(bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateListValidator(DateYMrange, "年月(yyyymm)を入力してください。例：2016年1月＝201601");
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 数値の範囲を指定したValidator
		/// </summary>
		/// <param name="fromValue">数値の範囲（開始）</param>
		/// <param name="toValue">数値の範囲（終了）</param>
		/// <param name="isIntegerValue">整数かどうか</param>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToNumberRange(int fromValue, int toValue, bool isIntegerValue = true, bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateNumberValidator(ComparisonOperator.Between, fromValue, toValue, isIntegerValue, fromValue.ToString() + "～" + toValue.ToString() + "の数値を入力してください。");
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 数値の桁数を指定したValidator
		/// </summary>
		/// <param name="value">MAX値</param>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToNumberDigit(int value, bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateNumberValidator(ComparisonOperator.LessThanOrEqualTo, value, null, true, value.ToString().Length + "桁までの整数を入力してください。");
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 数値の桁数を指定したValidator(decimal用）
		/// </summary>
		/// <param name="value">MAX値</param>
		/// <param name="msg">エラー時メッセージ</param>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToNumberDigit(decimal value, string msg, bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateNumberValidator(ComparisonOperator.LessThanOrEqualTo, value, null, false, msg);
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 文字列の桁数を指定したValidator
		/// </summary>
		/// <param name="value">最大文字数</param>
		/// <param name="nullable">NULLを許容するかどうか</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private ByteValidator SetValidatorToString(int value, bool nullable = true)
		{
			//value += 1;
			////LassThanOrEqualTo→LessThan
			//CellValidator cv = new CellValidator();
			//cv = CellValidator.CreateTextLengthValidator(ComparisonOperator.LessThan, value, null, value.ToString() + "文字以下で入力してください。");
			//cv.IgnoreBlank = nullable;
			//return cv;


			ByteValidator cv = new ByteValidator(value, value.ToString() + "文字以下で入力してください。");
			return cv;
		}

		/// <summary>
		/// 20150723 wada add 文字列で存在するかどうかのValidator
		/// フィールドが外部キー等で存在しないものを登録されると困るときに使用する。
		/// </summary>
		/// <param name="value">対象文字列（カンマ区切り）</param>
		/// <returns>Spread用のセットしたCellValidator</returns>
		private CellValidator SetValidatorToStringExists(string value, bool nullable = true)
		{
			CellValidator cv = new CellValidator();
			cv = CellValidator.CreateListValidator(value, "この値はマスタに存在しません。正しい値を入力してください。");
			cv.IgnoreBlank = nullable;
			return cv;
		}

		/// <summary>
		/// 20150715 wada add
		/// エラー行かどうかをDataTableに持たせる。
		/// </summary>
		private void SetRowError()
		{
			foreach (var s in CsvData.Rows)
			{
				取込データ.Rows[s.Index].RowError = string.Empty;
				foreach (var c in s.Cells)
				{
					if (!c.IsValid)
					{
						取込データ.Rows[s.Index].RowError = "c";
						break;
					}
				}
				取込データ.Rows[s.Index].RowError += s.IsValid == false ? "r" : string.Empty;
			}
		}

			#endregion

		#region CSVファイル出力
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

		#region リボン

		/// <summary>
		/// CSVデータ出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF5Key(object sender, KeyEventArgs e)
		{
			DataTable CsvTable = 取込データ.Copy();
			int i = 0;
			while (CsvTable.Rows.Count > i)
			{
				int Flg = Convert.ToInt32(CsvTable.Rows[i]["Flg"].ToString());
				if (Flg == 0)
				{
					CsvTable.Rows[i].Delete();
				}
				else
				{
					i += 1;
				}
			}
			CsvTable.Columns.RemoveAt(CsvTable.Columns.Count - 1);
			OutPutCSV(CsvTable);
		}

		/// <summary>
		/// CSVデータ取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF6Key(object sender, KeyEventArgs e)
		{

			//20160202 Toyama add COMBO_LISTに登録されている【取込不可】のデータはCSV参照を行わせないようにする
			//取込可能かチェック
			string FileName = this.TableName.Text;
			if (FileName.Contains("【取込不可】") == true)
			{

				var yesno = MessageBox.Show(FileName.Replace("【取込不可】", string.Empty) + "は取込出来ません", "取込不可", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.OK)
				{
					return;
				}
			}



			string tablenm = "";
			switch (TableName.SelectedIndex)
			{
			case 0:
				tablenm = "T01_TRN";
				break;
			case 1:
				tablenm = "T02_UTRN";
				break;
			case 2:
				tablenm = "T03_KTRN";
				break;
			case 3:
				tablenm = "T04_NYUK";
				break;

			}

			CommunicationObject com
				= new CommunicationObject(MessageType.RequestData, SEARCH_MST90022_00, tablenm);
			base.SendRequest(com);

			//return;


			if (opendiag.ShowDialog().Value)
			{
				using (Stream fstream = opendiag.OpenFile())
				{
					// CSVファイルを開く
					CsvData.OpenCsv(fstream, new CsvOpenSettings() { IncludeColumnHeader = true, ColumnHeaderRowCount = 1 });

					int RowCount = CsvData.Rows.Count();
					int ColCount = CsvData.Columns.Count();

					取込データ = CSVData.ReadCsv(opendiag.FileName, ",");
					ChangeKeyItemChangeable(false);

					// 20150715 wada add
					// Spreadの列・行数を合わせる。
					CsvData.ColumnCount = 取込データ.Columns.Count;
					CsvData.RowCount = 取込データ.Rows.Count;
				}
				CSVデータ = 取込データ;

				Table_column = new List<COLS>();

				//空白追加
				Table_column.Add(new COLS { });
				//コンボリスト追加
				foreach (DataColumn col in CSVデータ.Columns)
				{
					Table_column.Add(new COLS
					{
						name = col.ColumnName,
						systype = CSVデータ.Rows.Count > 0 ? (CSVデータ.Rows[0][col.ColumnName]).ToString() : "",
					});
				}


				// コンボボックス型セル（マルチカラム）
				ComboBoxCellType c3 = new ComboBoxCellType();
				c3.ItemsSource = Table_column;
				c3.ContentPath = "name";
				c3.UseMultipleColumn = true;
				c3.AutoGenerateColumns = false;
				c3.ValueType = ComboBoxValueType.SelectedIndex;
				c3.Columns.Add(new ListTemplateColumn()
				{
					Header = "項目名",
					SubItemTemplate = CsvData2.FindResource("MyListColumnTemplate") as DataTemplate
				});
				c3.Columns.Add(new ListTextColumn() { Header = "1レコード目", MemberPath = "systype" });
				c3.DropDownWidth = 300;
				CsvData2.Columns[2].CellType = c3;

				//CsvData2.ItemsSource = 取込設定;


				//DialogPass = ofd.FileName;
				ChangeKeyItemChangeable(false);

				TabItem2.IsSelected = true;
			}
		}


		#region チェック

		/// <summary>
		/// チェック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Check()
		{

			string Name = this.TableName.Text;


			//MessageBoxResult result = MessageBox.Show(Name + "のデータをチェックします。"
			//			  , "ファイル確認"
			//			  , MessageBoxButton.YesNo
			//			  , MessageBoxImage.Question);
			//if (result == MessageBoxResult.Yes)
			//{
			try
			{
				// 20150727 wada commentout 不要っぽい。
				//int iKey;
				//DateTime dKey;

				// 20150730 wada modify index→valueに変更
				//switch (TableName.SelectedIndex)
				switch ((int)TableName.SelectedValue)
				{

				// 20150723～ wada modify Exceptionの設定見直し
				// case1以降はコメントアウトするとソースが長くなるので変更前ソースは削除しています。
				case 0:
					base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90060_00, new object[] { }));
					break;

				case 01:
					base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90060_01, new object[] { }));
					break;

				case 02:
					base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90060_02, new object[] { }));
					break;

				case 03:
					base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90060_03, new object[] { }));
					break;

				}
			}

			// 20150716 wada add
			catch (CSVException ex)
			{
				MessageBox.Show("参照したファイルは" + TableName.Combo_Text + "と一致しません", "参照エラー", MessageBoxButton.OK, MessageBoxImage.Error);
				ScreenClear();
			}

			catch (Exception ex)
			{
				// 20150716 wada modify
				//int ErrorCode = ex.HResult;
				//if (ErrorCode == -2147024809)
				//{
				//    MessageBox.Show("参照したファイルは" + TableName.Combo_Text + "と一致しません", "参照エラー", MessageBoxButton.OK, MessageBoxImage.Error);
				//    ScreenClear();
				//}
				//else
				//{
				MessageBox.Show(ex.Message);
				//}
			}
			//}

		}

		#endregion

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF9Key(object sender, KeyEventArgs e)
		{
			// 20150730 wada modify
			//SpreadValidationError[] errlist = CsvData.ValidateAll();
			CheckCSVDataAll();

			DataSet ds = new DataSet();

			var yesno = MessageBox.Show("データを登録しますか？\n\r(＊赤い行はエラーの為登録出来ません＊)", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
			if (yesno == MessageBoxResult.Yes)
			{
				//DataSet型としてLinqに送る
				ds.Tables.Add(取込データ);
				取込データ.TableName = "CSV取り込み";
				this.Cursor = Cursors.Wait;
				// 20150730 wada modify index→value値に変更
				base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST900601, new object[] { ds, (int)TableName.SelectedValue, 0 }));
			}
			else
			{
				return;
			}
		}

		/// <summary>
		/// 取り消し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF10Key(object sender, KeyEventArgs e)
		{
			ScreenClear();
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF11Key(object sender, KeyEventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Mindoow_Closed
		//画面が閉じられた時、データを保持する
		private void MainWindow_Closed(object sender, EventArgs e)
		{
			CsvData.ItemsSource = null;
			取込設定 = null;

			this.Cursor = Cursors.Arrow;

			if (ucfg != null)
			{
				if (frmcfg == null) { frmcfg = new ConfigMST90060(); }
				frmcfg.Top = this.Top;
				frmcfg.Left = this.Left;
				frmcfg.Width = this.Width;
				frmcfg.Height = this.Height;

				ucfg.SetConfigValue(frmcfg);
			}
		}
		#endregion

		#region セル値変更
		private void CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
			// 20150729 wada add 取込データがない場合は抜ける。
			if (取込データ == null)
			{
				return;
			}

			int col = CsvData.ActiveColumnIndex;
			int row = CsvData.ActiveRowIndex;
			取込データ.Rows[row][col] = CsvData.ActiveCell.Text;

			// 20150728 wada commentout
			//SetData();
		}
		#endregion

		/// <summary>
		/// 20150729 wada add
		/// セルを抜けたときのイベント
		/// 必須項目かどうかをここでチェックする。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CsvData_CellLeave(object sender, SpreadCellLeaveEventArgs e)
		{
			if (this.CsvData.ActiveCell == null)
			{
				return;
			}

			// 値が空欄の場合で必須かどうかをチェックする。
			if (this.CsvData.ActiveCell.Value == null)
			{
				if (this.CsvData.ActiveCell.InheritedValidator != null)
				{
					// 空欄を許可しないかどうかを判断する。
					if (!this.CsvData.ActiveCell.InheritedValidator.IgnoreBlank)
					{
						int col = e.Column;
						int row = e.Row;
						this.CsvData.Cells[row, col].ValidationErrors.Add(new SpreadValidationError("必須入力項目です。", null));
						e.Cancel = true;
					}
				}
			}
		}

		/// <summary>
		/// 20150729 wada add
		/// 必須項目をチェックする。
		/// </summary>
		private void CheckRequiredField()
		{
			// 地味だが１つ１つ空欄を許可しないかどうかを判断する。
			for (int col = 0; col < this.CsvData.Columns.Count; col++)
			{
				for (int row = 0; row < this.CsvData.Rows.Count; row++)
				{
					if (this.CsvData.Cells[row, col].Value == null)
					{
						if (this.CsvData.Cells[row, col].InheritedValidator != null)
						{
							if (!this.CsvData.Cells[row, col].InheritedValidator.IgnoreBlank)
							{
								this.CsvData.Cells[row, col].ValidationErrors.Add(new SpreadValidationError("必須入力項目です。", null));
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 取り込んだCSVデータのチェックを行う。
		/// </summary>
		private void CheckCSVDataAll()
		{

			// 20150723 wada add ここでValidationチェックとキー違反チェック、エラー行のセットを行う。
			// セットしたValidationをチェックする。
			this.CsvData.ValidateAll();

			// 20150729 wada add 必須項目をチェックする。
			CheckRequiredField();
			// 20150723 wada add
			// 主キー違反をしていないかチェックする。
			foreach (DataRow dr in 取込データ.Rows)
			{
				#region コメントアウト 2016/05/18
				/*
                List<string> mineKey = new List<string>();
                List<string> subKey = new List<string>();

                // CSV取込データの中で、対象のフィールド名を取り出し、キーの値をセットする。
                foreach (string tk in targetKey)
                {
                    if (!string.IsNullOrEmpty(tk))
                    {
                        mineKey.Add(dr[tk].ToString());
                    }
                }
                foreach (DataRow dr2 in Database.Rows)
                {
                    // 登録済データの中で、対象のフィールド名を取り出し、キーの値をセットする。
                    foreach (string tk in targetKey)
                    {
                        if (!string.IsNullOrEmpty(tk))
                        {
                            if (dr2[tk] is decimal)
                            {
                                subKey.Add((Convert.ToDecimal(dr2[tk])).ToString("#.#####"));
                            }
                            else if (dr2[tk] is DateTime)
                            {
                                subKey.Add((Convert.ToDateTime(dr2[tk])).ToString("yyyy/M/d"));
                            }
                            else
                            {
                                subKey.Add(dr2[tk].ToString());
                            }
                        }
                    }

                    // キー同士が同じかどうかチェックする。
                    if (Enumerable.SequenceEqual(mineKey.OrderBy(t => t), subKey.OrderBy(t => t)))
                    {
                        string msg = "明細番号と明細行が重複しています。";
                        CsvData.Rows[取込データ.Rows.IndexOf(dr)].ValidationErrors.Add(new SpreadValidationError(msg, null));
                        break;
                    }
                    subKey.Clear();
                }
                */
				#endregion

				//▼速度改善 2016/05/18 Arinobu
				if (Database.Columns.Contains("明細番号") && Database.Columns.Contains("明細行"))
				{

					DataRow[] drList = Database.Select("明細番号 = " + (dr["明細番号"].ToString() == "" ? "0" : dr["明細番号"].ToString()) + " AND 明細行 = " + (dr["明細行"].ToString() == "" ? "0" : dr["明細行"].ToString()));

					if (drList.Length > 0)
					{
						//エラー処理
						string msg = "明細番号と明細行が重複しています。";
						CsvData.Rows[取込データ.Rows.IndexOf(dr)].ValidationErrors.Add(new SpreadValidationError(msg, null));
					}
				}
				//▲速度改善 2016/05/18 Arinobu
			}

			// エラー行かどうかをセットする。
			SetRowError();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Yomikomi();
		}


		private void Yomikomi()
		{
			TabItem1.IsSelected = true;
			//datatable列作成
			DataTable 売上明細データ = new DataTable();
			foreach (var row in 取込設定)
			{
				売上明細データ.Columns.Add(row.table_name, typeof(string));
			}
			DataTable 売上明細データ2 = new DataTable();
			foreach (var row in 取込設定)
			{
				売上明細データ2.Columns.Add(row.table_name, typeof(string));
			}


			foreach (DataRow row in CSVデータ.Rows)
			{
				var nrow = 売上明細データ2.NewRow();

				foreach (var row2 in 取込設定)
				{
					if (row2.wariate != 0 && row.ItemArray.Count() > (row2.wariate - 1))
					{
						nrow[row2.table_name] = row[row2.wariate - 1];
					}
					if ((row2.kotei != "" && row2.kotei != null) && row.ItemArray.Count() > (row2.wariate - 1))
					{
						nrow[row2.table_name] = row2.kotei;
					}
				}
				売上明細データ2.Rows.Add(nrow);
			}

			GcSpreadGrid abc = new GcSpreadGrid();
			abc.ItemsSource = 売上明細データ2.DefaultView;
			CsvData.Rows.Clear();
			CsvData.Columns.Clear();
			CsvData.RowCount = abc.RowCount;
			CsvData.ColumnCount = abc.ColumnCount;
			for (int j = 0; j < abc.Columns.Count; j++)
			{
				CsvData.Columns[j].Header = 売上明細データ2.Columns[j].ToString();
			}
			for (int i = 0; i < abc.Rows.Count; i++)
			{
				for (int x = 0; x < abc.Columns.Count; x++)
				{
					CsvData[i, x].Value = abc[i, x].Value.ToString();
				}
			}
			取込データ = 売上明細データ2;
			売上明細データ = 売上明細データ2;

			for (int i = 0; i < abc.Rows.Count; i++)
			{
				for (int x = 0; x < abc.Columns.Count; x++)
				{
					取込データ.Rows[i][x] = CsvData[i, x].Value;
				}
			}

			switch (TableName.SelectedIndex)
			{
			case 0:
				frmcfg.取込保存1 = 取込設定;
				break;
			case 1:
				frmcfg.取込保存2 = 取込設定;
				break;
			case 2:
				frmcfg.取込保存3 = 取込設定;
				break;
			case 3:
				frmcfg.取込保存4 = 取込設定;
				break;

			}
			Check();
		}

		private void S_READ_Button_Click(object sender, RoutedEventArgs e)
		{

			if (opendiag.ShowDialog().Value)
			{
				using (Stream fstream = opendiag.OpenFile())
				{
					List<TORIKOMI_SETTEI> CP取込設定 = new List<TORIKOMI_SETTEI>();
					foreach (TORIKOMI_SETTEI row in 取込設定)
					{
						CP取込設定.Add(new TORIKOMI_SETTEI
						{
							kotei = row.kotei,
							setumei = row.setumei,
							systype = row.systype,
							table_name = row.table_name,
							wariate = row.wariate,
						});
					}

					CSV設定データ = CSVData.ReadCsv(opendiag.FileName, ",");
					if (CSV設定データ.Rows.Count == 取込設定.Count && CSV設定データ.Columns.Count == 5 && CSV設定データ.Columns[0].ColumnName == "systype" && CSV設定データ.Columns[1].ColumnName == "table_name"
						&& CSV設定データ.Columns[2].ColumnName == "wariate" && CSV設定データ.Columns[3].ColumnName == "kotei" && CSV設定データ.Columns[4].ColumnName == "setumei")
					{
						int cnt = 0;
						foreach (DataRow row in CSV設定データ.Rows)
						{
							CP取込設定[cnt].kotei = row["kotei"].ToString();
							CP取込設定[cnt].wariate = AppCommon.IntParse(row["wariate"].ToString());
							cnt++;
						}
						取込設定 = CP取込設定;
					}
				}
			}
		}

		private void S_SAVE_Button_Click(object sender, RoutedEventArgs e)
		{

			DataTable CSVデータ = new DataTable();
			//リストをデータテーブルへ
			AppCommon.ConvertToDataTable(取込設定, CSVデータ);
			OutPutCSV(CSVデータ);
		}

		private void TabItem1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var a = tabControl.SelectedIndex;
			if (取込設定 != null && 取込設定.Count > 0 && tabControl.SelectedIndex != 0)
			{
				Yomikomi();
			}
		}




	}
}