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


    /// <summary>
    /// CSVデータ取込画面
    /// 20150714 wada 変更着手
    /// </summary>
    public partial class MST90090 : WindowReportBase
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
        public class ConfigMST90090 : FormConfigBase
        {

            #region 取込保存
            // csv設定
            public List<TORIKOMI_SETTEI> 取込保存1 = null;
            public List<TORIKOMI_SETTEI> 取込保存2 = null;
            public List<TORIKOMI_SETTEI> 取込保存3 = null;
            public List<TORIKOMI_SETTEI> 取込保存4 = null;
            public List<TORIKOMI_SETTEI> 取込保存5 = null;
            public List<TORIKOMI_SETTEI> 取込保存6 = null;
            public List<TORIKOMI_SETTEI> 取込保存7 = null;
            public List<TORIKOMI_SETTEI> 取込保存8 = null;
            public List<TORIKOMI_SETTEI> 取込保存9 = null;
            public List<TORIKOMI_SETTEI> 取込保存10 = null;
            public List<TORIKOMI_SETTEI> 取込保存11 = null;
            public List<TORIKOMI_SETTEI> 取込保存12 = null;
            public List<TORIKOMI_SETTEI> 取込保存13 = null;
            public List<TORIKOMI_SETTEI> 取込保存14 = null;
            public List<TORIKOMI_SETTEI> 取込保存15 = null;
            public List<TORIKOMI_SETTEI> 取込保存16 = null;
            public List<TORIKOMI_SETTEI> 取込保存17 = null;
            public List<TORIKOMI_SETTEI> 取込保存18 = null;
            public List<TORIKOMI_SETTEI> 取込保存19 = null;
            public List<TORIKOMI_SETTEI> 取込保存20 = null;
            public List<TORIKOMI_SETTEI> 取込保存21 = null;
            public List<TORIKOMI_SETTEI> 取込保存22 = null;
            public List<TORIKOMI_SETTEI> 取込保存23 = null;
            public List<TORIKOMI_SETTEI> 取込保存24 = null;
            public List<TORIKOMI_SETTEI> 取込保存25 = null;
            public List<TORIKOMI_SETTEI> 取込保存26 = null;
            public List<TORIKOMI_SETTEI> 取込保存27 = null;
            public List<TORIKOMI_SETTEI> 取込保存28 = null;
            public List<TORIKOMI_SETTEI> 取込保存29 = null;
            public List<TORIKOMI_SETTEI> 取込保存30 = null;
            public List<TORIKOMI_SETTEI> 取込保存31 = null;
            public List<TORIKOMI_SETTEI> 取込保存32 = null;
            public List<TORIKOMI_SETTEI> 取込保存33 = null;
            public List<TORIKOMI_SETTEI> 取込保存34 = null;
            public List<TORIKOMI_SETTEI> 取込保存35 = null;
            public List<TORIKOMI_SETTEI> 取込保存36 = null;
            public List<TORIKOMI_SETTEI> 取込保存37 = null;
            public List<TORIKOMI_SETTEI> 取込保存38 = null;
            public List<TORIKOMI_SETTEI> 取込保存39 = null;
            public List<TORIKOMI_SETTEI> 取込保存40 = null;
            public List<TORIKOMI_SETTEI> 取込保存41 = null;
            public List<TORIKOMI_SETTEI> 取込保存42 = null;
            public List<TORIKOMI_SETTEI> 取込保存43 = null;
            public List<TORIKOMI_SETTEI> 取込保存44 = null;
            public List<TORIKOMI_SETTEI> 取込保存45 = null;
            public List<TORIKOMI_SETTEI> 取込保存46 = null;
            public List<TORIKOMI_SETTEI> 取込保存47 = null;
            public List<TORIKOMI_SETTEI> 取込保存48 = null;
            public List<TORIKOMI_SETTEI> 取込保存49 = null;
            public List<TORIKOMI_SETTEI> 取込保存50 = null;

            #endregion
        }

        /// ※ 必ず public で定義する。
        public ConfigMST90090 frmcfg = null;

        #endregion

        #region 定数
        private const string MST90020_TOK = "MST90020_TOK";
        private const string MST90020_CAR = "MST90020_CAR";
        private const string MST90020_DRV = "MST90020_DRV";
        private const string SEARCH_MST90060_00 = "SEARCH_MST90022_00"; //TRNT
        // 20150723 wada add
        private const string MST90020_TIK = "MST90020_TIK";     // 発着地ID
        private const string MST90020_HIN = "MST90020_HIN";     // 商品ID
        private const string MST90020_SYA = "MST90020_SYA";     // 車種ID
        private const string MST90020_TEK = "MST90020_TEK";     // 摘要ID

        private const string SEARCH_MST900201 = "SEARCH_MST900201";
        private const string SEARCH_MST90020 = "SEARCH_MST90020";
        private const string SEARCH_MST90020_00 = "SEARCH_MST90020_00";
        private const string SEARCH_MST90020_01 = "SEARCH_MST90020_01";
        private const string SEARCH_MST90020_02 = "SEARCH_MST90020_02";
        private const string SEARCH_MST90020_03 = "SEARCH_MST90020_03";
        private const string SEARCH_MST90020_04 = "SEARCH_MST90020_04";
        private const string SEARCH_MST90020_05 = "SEARCH_MST90020_05";
        private const string SEARCH_MST90020_06 = "SEARCH_MST90020_06";
        private const string SEARCH_MST90020_07 = "SEARCH_MST90020_07";
        private const string SEARCH_MST90020_08 = "SEARCH_MST90020_08";
        private const string SEARCH_MST90020_09 = "SEARCH_MST90020_09"; //乗務員マスタ
        private const string SEARCH_MST90020_10 = "SEARCH_MST90020_10"; //適正診断データ
        private const string SEARCH_MST90020_11 = "SEARCH_MST90020_11"; //事故違反履歴データ
        private const string SEARCH_MST90020_12 = "SEARCH_MST90020_12"; //特別教育データ
        private const string SEARCH_MST90020_13 = "SEARCH_MST90020_13"; //乗務員データ
        private const string SEARCH_MST90020_14 = "SEARCH_MST90020_14"; //乗務員経費データ
        private const string SEARCH_MST90020_15 = "SEARCH_MST90020_15"; //乗務員画像データ
        private const string SEARCH_MST90020_16 = "SEARCH_MST90020_16"; //車輌マスタ
        private const string SEARCH_MST90020_17 = "SEARCH_MST90020_17";
        private const string SEARCH_MST90020_18 = "SEARCH_MST90020_18";
        private const string SEARCH_MST90020_19 = "SEARCH_MST90020_19";
        private const string SEARCH_MST90020_20 = "SEARCH_MST90020_20";
        private const string SEARCH_MST90020_21 = "SEARCH_MST90020_21";
        private const string SEARCH_MST90020_22 = "SEARCH_MST90020_22"; //車種マスタ
        private const string SEARCH_MST90020_23 = "SEARCH_MST90020_23";
        private const string SEARCH_MST90020_24 = "SEARCH_MST90020_24";
        private const string SEARCH_MST90020_25 = "SEARCH_MST90020_25"; //地区マスタ
        private const string SEARCH_MST90020_26 = "SEARCH_MST90020_26";
        private const string SEARCH_MST90020_27 = "SEARCH_MST90020_27"; //請求内訳マスタ
        private const string SEARCH_MST90020_28 = "SEARCH_MST90020_28";
        private const string SEARCH_MST90020_29 = "SEARCH_MST90020_29"; //規制マスタ
        private const string SEARCH_MST90020_30 = "SEARCH_MST90020_30"; //燃費目標マスタ
        private const string SEARCH_MST90020_31 = "SEARCH_MST90020_31"; //グリーン車種マスタ
        private const string SEARCH_MST90020_33 = "SEARCH_MST90020_33";
        private const string SEARCH_MST90020_34 = "SEARCH_MST90020_34";
        private const string SEARCH_MST90020_35 = "SEARCH_MST90020_35";
        private const string SEARCH_MST90020_36 = "SEARCH_MST90020_36";
        private const string SEARCH_MST90020_37 = "SEARCH_MST90020_37"; //取引区分マスタ
        private const string SEARCH_MST90020_38 = "SEARCH_MST90020_38"; //出勤区分マスタ
        private const string SEARCH_MST90020_39 = "SEARCH_MST90020_39";
        private const string SEARCH_MST90020_40 = "SEARCH_MST90020_40";
        private const string SEARCH_MST90020_41 = "SEARCH_MST90020_41";
        private const string SEARCH_MST90020_42 = "SEARCH_MST90020_42"; //運賃計算区分マスタ
        private const string SEARCH_MST90020_43 = "SEARCH_MST90020_43"; //運輸局マスタ
        private const string SEARCH_MST90020_44 = "SEARCH_MST90020_44";
        private const string SEARCH_MST90020_45 = "SEARCH_MST90020_45";
        private const string SEARCH_MST90020_46 = "SEARCH_MST90020_46";
        private const string SEARCH_MST90020_47 = "SEARCH_MST90020_47"; //グリッド表示マスタ
        private const string SEARCH_MST90020_48 = "SEARCH_MST90020_48"; //燃費単価マスタ
        private const string SEARCH_MST90020_49 = "SEARCH_MST90020_49";

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


        // 20150716 wada commentout 未使用っぽい
        //private int[] Count = new int[0];
        //private DataTable dt;

        private DataTable _DBData = null;
        public DataTable Database
        {
            get { return this._DBData; }
            set { this._DBData = value; NotifyPropertyChanged(); }
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

        List<string> IvKey;

        #endregion

        #region MST90090()
        /// <summary>
        /// 得意先売上合計表
        /// </summary>
        public MST90090()
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
            TabItem2.IsSelected = true;
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
            frmcfg = (ConfigMST90090)ucfg.GetConfigValue(typeof(ConfigMST90090));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST90090();
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

                    case SEARCH_MST90060_00:
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
                            case 4:
                                cnt = 0;
                                if (frmcfg.取込保存5 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存5.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存5[cnt].wariate;
                                            row.kotei = frmcfg.取込保存5[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 5:
                                cnt = 0;
                                if (frmcfg.取込保存6 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存6.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存6[cnt].wariate;
                                            row.kotei = frmcfg.取込保存6[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 6:
                                cnt = 0;
                                if (frmcfg.取込保存7 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存7.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存7[cnt].wariate;
                                            row.kotei = frmcfg.取込保存7[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 7:
                                cnt = 0;
                                if (frmcfg.取込保存8 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存8.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存8[cnt].wariate;
                                            row.kotei = frmcfg.取込保存8[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 8:
                                cnt = 0;
                                if (frmcfg.取込保存9 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存9.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存9[cnt].wariate;
                                            row.kotei = frmcfg.取込保存9[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 9:
                                cnt = 0;
                                if (frmcfg.取込保存10 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存10.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存10[cnt].wariate;
                                            row.kotei = frmcfg.取込保存10[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 10:
                                cnt = 0;
                                if (frmcfg.取込保存11 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存11.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存11[cnt].wariate;
                                            row.kotei = frmcfg.取込保存11[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 11:
                                cnt = 0;
                                if (frmcfg.取込保存12 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存12.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存12[cnt].wariate;
                                            row.kotei = frmcfg.取込保存12[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 12:
                                cnt = 0;
                                if (frmcfg.取込保存13 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存13.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存13[cnt].wariate;
                                            row.kotei = frmcfg.取込保存13[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 13:
                                cnt = 0;
                                if (frmcfg.取込保存14 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存14.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存14[cnt].wariate;
                                            row.kotei = frmcfg.取込保存14[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 14:
                                cnt = 0;
                                if (frmcfg.取込保存15 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存15.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存15[cnt].wariate;
                                            row.kotei = frmcfg.取込保存15[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 15:
                                cnt = 0;
                                if (frmcfg.取込保存16 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存16.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存16[cnt].wariate;
                                            row.kotei = frmcfg.取込保存16[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 16:
                                cnt = 0;
                                if (frmcfg.取込保存17 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存17.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存17[cnt].wariate;
                                            row.kotei = frmcfg.取込保存17[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 17:
                                cnt = 0;
                                if (frmcfg.取込保存18 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存18.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存18[cnt].wariate;
                                            row.kotei = frmcfg.取込保存18[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 18:
                                cnt = 0;
                                if (frmcfg.取込保存19 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存19.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存19[cnt].wariate;
                                            row.kotei = frmcfg.取込保存19[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 19:
                                cnt = 0;
                                if (frmcfg.取込保存20 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存20.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存20[cnt].wariate;
                                            row.kotei = frmcfg.取込保存20[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 20:
                                cnt = 0;
                                if (frmcfg.取込保存21 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存21.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存21[cnt].wariate;
                                            row.kotei = frmcfg.取込保存21[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 21:
                                cnt = 0;
                                if (frmcfg.取込保存22 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存22.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存22[cnt].wariate;
                                            row.kotei = frmcfg.取込保存22[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 22:
                                cnt = 0;
                                if (frmcfg.取込保存23 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存23.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存23[cnt].wariate;
                                            row.kotei = frmcfg.取込保存23[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 23:
                                cnt = 0;
                                if (frmcfg.取込保存24 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存24.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存24[cnt].wariate;
                                            row.kotei = frmcfg.取込保存24[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 24:
                                cnt = 0;
                                if (frmcfg.取込保存25 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存25.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存25[cnt].wariate;
                                            row.kotei = frmcfg.取込保存25[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 25:
                                cnt = 0;
                                if (frmcfg.取込保存26 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存26.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存26[cnt].wariate;
                                            row.kotei = frmcfg.取込保存26[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 26:
                                cnt = 0;
                                if (frmcfg.取込保存27 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存27.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存27[cnt].wariate;
                                            row.kotei = frmcfg.取込保存27[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 27:
                                cnt = 0;
                                if (frmcfg.取込保存28 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存28.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存28[cnt].wariate;
                                            row.kotei = frmcfg.取込保存28[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 28:
                                cnt = 0;
                                if (frmcfg.取込保存29 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存29.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存29[cnt].wariate;
                                            row.kotei = frmcfg.取込保存29[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 29:
                                cnt = 0;
                                if (frmcfg.取込保存30 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存30.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存30[cnt].wariate;
                                            row.kotei = frmcfg.取込保存30[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 30:
                                cnt = 0;
                                if (frmcfg.取込保存31 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存31.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存31[cnt].wariate;
                                            row.kotei = frmcfg.取込保存31[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 31:
                                cnt = 0;
                                if (frmcfg.取込保存32 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存32.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存32[cnt].wariate;
                                            row.kotei = frmcfg.取込保存32[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 32:
                                cnt = 0;
                                if (frmcfg.取込保存33 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存33.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存33[cnt].wariate;
                                            row.kotei = frmcfg.取込保存33[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 33:
                                cnt = 0;
                                if (frmcfg.取込保存34 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存34.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存34[cnt].wariate;
                                            row.kotei = frmcfg.取込保存34[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 34:
                                cnt = 0;
                                if (frmcfg.取込保存35 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存35.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存35[cnt].wariate;
                                            row.kotei = frmcfg.取込保存35[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 35:
                                cnt = 0;
                                if (frmcfg.取込保存36 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存36.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存36[cnt].wariate;
                                            row.kotei = frmcfg.取込保存36[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 36:
                                cnt = 0;
                                if (frmcfg.取込保存37 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存37.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存37[cnt].wariate;
                                            row.kotei = frmcfg.取込保存37[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 37:
                                cnt = 0;
                                if (frmcfg.取込保存38 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存38.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存38[cnt].wariate;
                                            row.kotei = frmcfg.取込保存38[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 38:
                                cnt = 0;
                                if (frmcfg.取込保存39 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存39.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存39[cnt].wariate;
                                            row.kotei = frmcfg.取込保存39[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 39:
                                cnt = 0;
                                if (frmcfg.取込保存40 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存40.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存40[cnt].wariate;
                                            row.kotei = frmcfg.取込保存40[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 40:
                                cnt = 0;
                                if (frmcfg.取込保存41 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存41.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存41[cnt].wariate;
                                            row.kotei = frmcfg.取込保存41[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 41:
                                cnt = 0;
                                if (frmcfg.取込保存42 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存42.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存42[cnt].wariate;
                                            row.kotei = frmcfg.取込保存42[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 42:
                                cnt = 0;
                                if (frmcfg.取込保存43 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存43.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存43[cnt].wariate;
                                            row.kotei = frmcfg.取込保存43[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 43:
                                cnt = 0;
                                if (frmcfg.取込保存44 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存44.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存44[cnt].wariate;
                                            row.kotei = frmcfg.取込保存44[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 44:
                                cnt = 0;
                                if (frmcfg.取込保存45 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存45.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存45[cnt].wariate;
                                            row.kotei = frmcfg.取込保存45[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 45:
                                cnt = 0;
                                if (frmcfg.取込保存46 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存46.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存46[cnt].wariate;
                                            row.kotei = frmcfg.取込保存46[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 46:
                                cnt = 0;
                                if (frmcfg.取込保存47 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存47.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存47[cnt].wariate;
                                            row.kotei = frmcfg.取込保存47[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 47:
                                cnt = 0;
                                if (frmcfg.取込保存48 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存48.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存48[cnt].wariate;
                                            row.kotei = frmcfg.取込保存48[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 48:
                                cnt = 0;
                                if (frmcfg.取込保存49 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存49.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存49[cnt].wariate;
                                            row.kotei = frmcfg.取込保存49[cnt].kotei;
                                        }
                                        cnt++;
                                    }
                                }
                                break;
                            case 49:
                                cnt = 0;
                                if (frmcfg.取込保存50 != null)
                                {
                                    foreach (var row in a)
                                    {
                                        if (frmcfg.取込保存50.Count > cnt)
                                        {
                                            row.wariate = frmcfg.取込保存50[cnt].wariate;
                                            row.kotei = frmcfg.取込保存50[cnt].kotei;
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

                        // 20150723 wada add 得意先KEYをカンマ区切りで格納する。
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

                        // 20150724 wada add 乗務員KEYをカンマ区切りで格納する。
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
                        // 20150724 wada add 車輌KEYをカンマ区切りで格納する。
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
                    case SEARCH_MST90020:
                    case SEARCH_MST90020_00:
                    case SEARCH_MST90020_01:
                    case SEARCH_MST90020_02:
                    case SEARCH_MST90020_03:
                    case SEARCH_MST90020_04:
                    case SEARCH_MST90020_05:
                    case SEARCH_MST90020_06:
                    case SEARCH_MST90020_07:
                    case SEARCH_MST90020_08:
                    case SEARCH_MST90020_09:
                    case SEARCH_MST90020_10:
                    case SEARCH_MST90020_11:
                    case SEARCH_MST90020_12:
                    case SEARCH_MST90020_13:
                    case SEARCH_MST90020_14:
                    case SEARCH_MST90020_15:
                    case SEARCH_MST90020_16:
                    case SEARCH_MST90020_17:
                    case SEARCH_MST90020_18:
                    case SEARCH_MST90020_19:
                    case SEARCH_MST90020_20:
                    case SEARCH_MST90020_21:
                    case SEARCH_MST90020_22:
                    case SEARCH_MST90020_23:
                    case SEARCH_MST90020_24:
                    case SEARCH_MST90020_25:
                    case SEARCH_MST90020_26:
                    case SEARCH_MST90020_27:
                    case SEARCH_MST90020_28:
                    case SEARCH_MST90020_29:
                    case SEARCH_MST90020_30:
                    case SEARCH_MST90020_31:
                    case SEARCH_MST90020_33:
                    case SEARCH_MST90020_34:
                    case SEARCH_MST90020_35:
                    case SEARCH_MST90020_36:
                    case SEARCH_MST90020_37:
                    case SEARCH_MST90020_38:
                    case SEARCH_MST90020_39:
                    case SEARCH_MST90020_40:
                    case SEARCH_MST90020_41:
                    case SEARCH_MST90020_42:
                    case SEARCH_MST90020_43:
                    case SEARCH_MST90020_44:
                    case SEARCH_MST90020_45:
                    case SEARCH_MST90020_46:
                    case SEARCH_MST90020_47:
                    case SEARCH_MST90020_48:
                    case SEARCH_MST90020_49:
                        Database = tbl;
                        F9.IsEnabled = true;
                        SetData();
                        break;

                    case SEARCH_MST900201:

                        // 20150716 wada add
                        // 取り込みデータのエラーをチェックして、登録されたデータがあるか確認する。
                        // それによりメッセージの内容を変更する。
                        string msg = "登録可能なデータはありませんでした。";
                        foreach (DataRow t in 取込データ.Rows)
                        {
                            if (!t.HasErrors)
                            {

                                msg = "登録が完了しました。";
                                this.Cursor = Cursors.Arrow;
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
                    #region 得意先マスタ

                    // 20150723～ wada modify CellValidatorの設定見直し
                    // 直接セットすると値を間違って入れているものがあったため、関数経由に変更する。
                    // 20150728変更 遠山様より数値9桁チェックのデータは7桁チェックに変更
                    // case1以降はコメントアウトするとソースが長くなるので変更前ソースは削除しています。
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999999, false);        // 得意先ID				
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日付             
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日付             
                    sp[3].CellValidator = SetValidatorToString(40);                        // 得意先名1            
                    sp[4].CellValidator = SetValidatorToString(40);                         // 得意先名2            
                    sp[5].CellValidator = SetValidatorToString(24);                         // 略称名               
                    sp[6].CellValidator = SetValidatorToNumberRange(0, 3);                  // 取引区分             
                    sp[7].CellValidator = SetValidatorToDateNumber(31);                     // T締日                
                    sp[8].CellValidator = SetValidatorToDateNumber(31);                     // S締日                
                    sp[9].CellValidator = SetValidatorToString(10);                         // かな読み             
                    sp[10].CellValidator = SetValidatorToString(8);                         // 郵便番号             
                    sp[11].CellValidator = SetValidatorToString(50);                        // 住所1                
                    sp[12].CellValidator = SetValidatorToString(50);                        // 住所2                
                    sp[13].CellValidator = SetValidatorToString(15);                        // 電話番号             
                    sp[14].CellValidator = SetValidatorToString(15);                        // FAX                  
                    sp[15].CellValidator = SetValidatorToDateNumber(31);                    // T集金日              
                    sp[16].CellValidator = SetValidatorToDateNumber(12);                    // Tサイト日            
                    sp[17].CellValidator = SetValidatorToNumberRange(0, 8);                 // T税区分ID            
                    sp[18].CellValidator = SetValidatorToNumberDigit(999999999, false);     // T締日期首残          
                    sp[19].CellValidator = SetValidatorToNumberDigit(999999999, false);     // T月次期首残          
                    sp[20].CellValidator = SetValidatorToNumberDigit(999999999, false);     // T路線計算年度        
                    sp[21].CellValidator = SetValidatorToNumberDigit(999999999, false);     // T路線計算率          
                    sp[22].CellValidator = SetValidatorToNumberDigit(999999999, false);     // T路線計算まるめ      
                    sp[23].CellValidator = SetValidatorToDateNumber(31);                    // S集金日              
                    sp[24].CellValidator = SetValidatorToDateNumber(12);                    // Sサイト日            
                    sp[25].CellValidator = SetValidatorToNumberRange(0, 8);                 // S税区分ID            
                    sp[26].CellValidator = SetValidatorToNumberDigit(999999999, false);     // S締日期首残          
                    sp[27].CellValidator = SetValidatorToNumberDigit(999999999, false);     // S月次期首残          
                    sp[28].CellValidator = SetValidatorToNumberDigit(999999999, false);     // S路線計算年度        
                    sp[29].CellValidator = SetValidatorToNumberDigit(999999999, false);     // S路線計算率          
                    sp[30].CellValidator = SetValidatorToNumberDigit(999999999, false);     // S路線計算まるめ      
                    sp[31].CellValidator = SetValidatorToNumberRange(0, 1);                 // ラベル区分           
                    sp[32].CellValidator = SetValidatorToNumberRange(0, 3);                 // 親子区分ID           
                    sp[33].CellValidator = SetValidatorToNumberDigit(9999999);              // 親ID                 
                    sp[34].CellValidator = SetValidatorToNumberRange(0, 1);                 // 請求内訳管理区分     
                    sp[35].CellValidator = SetValidatorToNumberDigit(9999999);              // 自社部門ID           
                    sp[36].CellValidator = SetValidatorToNumberRange(0, 7);                 // 請求運賃計算区分ID   
                    sp[37].CellValidator = SetValidatorToNumberRange(0, 7);                 // 支払運賃計算区分ID   
                    sp[38].CellValidator = SetValidatorToNumberRange(0, 3);                 // 請求書区分ID         
                    sp[39].CellValidator = SetValidatorToNumberDigit(9999999);              // 請求発行元ID         
                    sp[40].CellValidator = SetValidatorToString(20);                        // 法人ナンバー         
                    sp[41].CellValidator = SetValidatorToDate();                            // 削除日付             

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先ID");

                    break;
                    #endregion

                case 1:
                    #region 請求商品単価マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 得意先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit(99999);    // 発地ID
                    sp[2].CellValidator = SetValidatorToNumberDigit(99999);    // 着地ID
                    sp[3].CellValidator = SetValidatorToStringExists(HintableID, false);    // 商品ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
                        "999999999.99までの単価を入力してください。", false);               // 売上単価
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 3);                  // 計算区分
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先KEY");
                    targetKey.Add("発地ID");
                    targetKey.Add("着地ID");
                    targetKey.Add("商品ID");

                    break;
                    #endregion

                case 2:
                    #region 請求車種単価マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 得意先ID
                    sp[1].CellValidator = SetValidatorToStringExists(SyatableID, false);    // 車種ID
                    sp[2].CellValidator = SetValidatorToNumberDigit(99999);    // 発地ID
                    sp[3].CellValidator = SetValidatorToNumberDigit(99999);    // 着地ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
                        "999999999.99までの単価を入力してください。", false);               // 売上単価
                    sp[7].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先KEY");
                    targetKey.Add("車種ID");
                    targetKey.Add("発地ID");
                    targetKey.Add("着地ID");

                    break;
                    #endregion

                case 3:
                    #region 請求タリフマスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 得意先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit(99999, false);          // 距離
                    sp[2].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 重量
                    sp[3].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[4].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[5].CellValidator = SetValidatorToNumberDigit(9999999);               // 運賃
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先KEY");
                    targetKey.Add("距離");
                    targetKey.Add("重量");

                    break;
                    #endregion

                case 4:
                    #region 請求個建マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 得意先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit((decimal)999999.999,
                        "999999.999までの重量を入力してください。", false);                 // 重量
                    sp[2].CellValidator = SetValidatorToNumberDigit((decimal)99999999.9,
                        "99999999.9までの個数を入力してください。", false);                 // 個数
                    sp[3].CellValidator = SetValidatorToStringExists(TiktableID, false);    // 着地ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)9999999.99,
                        "9999999.99までの単価を入力してください。", false);                 // 個建単価
                    sp[7].CellValidator = SetValidatorToNumberDigit(999999999, false);      // 個建金額
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先KEY");
                    targetKey.Add("重量");
                    targetKey.Add("個数");
                    targetKey.Add("着地ID");

                    break;
                    #endregion

                case 5:
                    #region 支払商品単価マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 支払先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit(99999, false);    // 発地ID
                    sp[2].CellValidator = SetValidatorToNumberDigit(99999, false);    // 着地ID
                    sp[3].CellValidator = SetValidatorToStringExists(HintableID, false);    // 商品ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
                        "999999999.99までの単価を入力してください。");                      // 支払単価
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 3);                  // 計算区分
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("支払先KEY");
                    targetKey.Add("発地ID");
                    targetKey.Add("着地ID");
                    targetKey.Add("商品ID");

                    break;
                    #endregion

                case 6:
                    #region 支払車種単価マスタ

                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 支払先KEY
                    sp[1].CellValidator = SetValidatorToStringExists(SyatableID, false);    // 車種ID
                    sp[2].CellValidator = SetValidatorToNumberDigit(99999, false);    // 発地ID
                    sp[3].CellValidator = SetValidatorToNumberDigit(99999, false);    // 着地ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
                        "999999999.99までの単価を入力してください。", false);               // 支払単価
                    sp[7].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("支払先KEY");
                    targetKey.Add("車種ID");
                    targetKey.Add("発地ID");
                    targetKey.Add("着地ID");

                    break;
                    #endregion

                case 7:
                    #region 支払タリフマスタ

                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 支払先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit(99999, false);          // 距離
                    sp[2].CellValidator = SetValidatorToNumberDigit((decimal)999999.999,
                        "999999.999までの重量を入力してください。", false);                 // 重量
                    sp[3].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[4].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[5].CellValidator = SetValidatorToNumberDigit(999999999);             // 運賃
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("支払先KEY");
                    targetKey.Add("距離");
                    targetKey.Add("重量");

                    break;
                    #endregion

                case 8:
                    #region 支払個建マスタ

                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 支払先KEY
                    sp[1].CellValidator = SetValidatorToNumberDigit((decimal)999999.999,
                        "999999.999までの重量を入力してください。", false);                 // 重量
                    sp[2].CellValidator = SetValidatorToNumberDigit((decimal)99999999.9,
                        "99999999.9までの個数を入力してください。", false);                 // 個数
                    sp[3].CellValidator = SetValidatorToStringExists(TiktableID, false);    // 着地ID
                    sp[4].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[5].CellValidator = SetValidatorToDate();                             // 変更日時
                    sp[6].CellValidator = SetValidatorToNumberDigit((decimal)999999999.99,
                        "999999999.99までの単価を入力してください。", false);               // 個建単価
                    sp[7].CellValidator = SetValidatorToNumberDigit(999999999, false);      // 個建金額
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("支払先KEY");
                    targetKey.Add("重量");
                    targetKey.Add("個数");
                    targetKey.Add("着地ID");

                    break;
                    #endregion

                case 9:
                    #region 乗務員マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 乗務員ID					
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時                 
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時                 
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 乗務員名                 
                    sp[4].CellValidator = SetValidatorToNumberRange(0, 1, false);           // 自傭区分                 
                    sp[5].CellValidator = SetValidatorToNumberRange(0, 2, false);           // 就労区分                 
                    sp[6].CellValidator = SetValidatorToString(20);                         // かな読み                 
                    sp[7].CellValidator = SetValidatorToDate();                             // 生年月日                 
                    sp[8].CellValidator = SetValidatorToDate();                             // 入社日                   
                    sp[9].CellValidator = SetValidatorToNumberDigit(99999);          // 自社部門ID               
                    sp[10].CellValidator = SetValidatorToNumberDigit((decimal)100.00, "100.00までの値を入力して下さい。", false); // 歩合率                   
                    sp[11].CellValidator = SetValidatorToNumberDigit(9999999);              // デジタコCD               
                    sp[12].CellValidator = SetValidatorToNumberRange(0, 1, true, false);    // 性別区分                 
                    sp[13].CellValidator = SetValidatorToString(8);                         // 郵便番号                 
                    sp[14].CellValidator = SetValidatorToString(50);                        // 住所１                   
                    sp[15].CellValidator = SetValidatorToString(50);                        // 住所２                   
                    sp[16].CellValidator = SetValidatorToString(15);                        // 電話番号                 
                    sp[17].CellValidator = SetValidatorToString(15);                        // 携帯電話                 
                    sp[18].CellValidator = SetValidatorToString(20);                        // 業務種類                 
                    sp[19].CellValidator = SetValidatorToDate();                            // 選任年月日               
                    sp[20].CellValidator = SetValidatorToNumberRange(0, 4, true, false);    // 血液型                   
                    sp[21].CellValidator = SetValidatorToString(20);                        // 免許証番号               
                    sp[22].CellValidator = SetValidatorToDate();                            // 免許証取得年月日         
                    sp[23].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類1                
                    sp[24].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類2                
                    sp[25].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類3                
                    sp[26].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類4                
                    sp[27].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類5                
                    sp[28].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類6                
                    sp[29].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類7                
                    sp[30].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類8                
                    sp[31].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類9                
                    sp[32].CellValidator = SetValidatorToNumberDigit(9);                    // 免許種類10               
                    sp[33].CellValidator = SetValidatorToString(40);                        // 免許証条件               
                    sp[34].CellValidator = SetValidatorToNumberDigit(9);                    // 職種分類区分             
                    sp[35].CellValidator = SetValidatorToString(20);                        // 職種分類                 
                    sp[36].CellValidator = SetValidatorToNumberDigit(9999999);              // 作成番号                 
                    sp[37].CellValidator = SetValidatorToNumberDigit(9999);                 // 撮影年                   
                    sp[38].CellValidator = SetValidatorToDateNumber(12);                    // 撮影月                   
                    sp[39].CellValidator = SetValidatorToDate();                            // 免許有効年月日1          
                    sp[40].CellValidator = SetValidatorToString(20);                        // 免許有効番号1            
                    sp[41].CellValidator = SetValidatorToDate();                            // 免許有効年月日2          
                    sp[42].CellValidator = SetValidatorToString(20);                        // 免許有効番号2            
                    sp[43].CellValidator = SetValidatorToDate();                            // 免許有効年月日3          
                    sp[44].CellValidator = SetValidatorToString(20);                        // 免許有効番号3            
                    sp[45].CellValidator = SetValidatorToDate();                            // 免許有効年月日4          
                    sp[46].CellValidator = SetValidatorToString(20);                        // 免許有効番号4            
                    sp[47].CellValidator = SetValidatorToDate();                            // 履歴年月日1              
                    sp[48].CellValidator = SetValidatorToString(40);                        // 履歴1                    
                    sp[49].CellValidator = SetValidatorToDate();                            // 履歴年月日2              
                    sp[50].CellValidator = SetValidatorToString(40);                        // 履歴2                    
                    sp[51].CellValidator = SetValidatorToDate();                            // 履歴年月日3              
                    sp[52].CellValidator = SetValidatorToString(40);                        // 履歴3                    
                    sp[53].CellValidator = SetValidatorToDate();                            // 履歴年月日4              
                    sp[54].CellValidator = SetValidatorToString(40);                        // 履歴4                    
                    sp[55].CellValidator = SetValidatorToDate();                            // 履歴年月日5              
                    sp[56].CellValidator = SetValidatorToString(40);                        // 履歴5                    
                    sp[57].CellValidator = SetValidatorToDate();                            // 履歴年月日6              
                    sp[58].CellValidator = SetValidatorToString(40);                        // 履歴6                    
                    sp[59].CellValidator = SetValidatorToDate();                            // 履歴年月日7              
                    sp[60].CellValidator = SetValidatorToString(40);                        // 履歴7                    
                    sp[61].CellValidator = SetValidatorToNumberDigit(9);                    // 経験種類1                
                    sp[62].CellValidator = SetValidatorToNumberDigit(9999999);              // 経験定員1                
                    sp[63].CellValidator = SetValidatorToNumberRange(1, 99999999, false);   // 経験積載量1              
                    sp[64].CellValidator = SetValidatorToNumberRange(1, 9999);              // 経験年1                  
                    sp[65].CellValidator = SetValidatorToDateNumber(12);                    // 経験月1                  
                    sp[66].CellValidator = SetValidatorToString(40);                        // 経験事業所1              
                    sp[67].CellValidator = SetValidatorToNumberDigit(9);                    // 経験種類2                
                    sp[68].CellValidator = SetValidatorToNumberDigit(9999999);              // 経験定員2                
                    sp[69].CellValidator = SetValidatorToNumberRange(1, 99999999, false);   // 経験積載量2              
                    sp[70].CellValidator = SetValidatorToNumberRange(1, 9999);              // 経験年2                  
                    sp[71].CellValidator = SetValidatorToDateNumber(12);                    // 経験月2                  
                    sp[72].CellValidator = SetValidatorToString(40);                        // 経験事業所2              
                    sp[73].CellValidator = SetValidatorToNumberDigit(9);                    // 経験種類3                
                    sp[74].CellValidator = SetValidatorToNumberDigit(9999999);              // 経験定員3                
                    sp[75].CellValidator = SetValidatorToNumberRange(1, 99999999, false);   // 経験積載量3              
                    sp[76].CellValidator = SetValidatorToNumberRange(1, 9999);              // 経験年3                  
                    sp[77].CellValidator = SetValidatorToDateNumber(12);                    // 経験月3                  
                    sp[78].CellValidator = SetValidatorToString(40);                        // 経験事業所3              
                    sp[79].CellValidator = SetValidatorToDate();                            // 資格賞罰年月日1          
                    sp[80].CellValidator = SetValidatorToString(40);                        // 資格賞罰名1              
                    sp[81].CellValidator = SetValidatorToString(40);                        // 資格賞罰内容1            
                    sp[82].CellValidator = SetValidatorToDate();                            // 資格賞罰年月日2          
                    sp[83].CellValidator = SetValidatorToString(40);                        // 資格賞罰名2              
                    sp[84].CellValidator = SetValidatorToString(40);                        // 資格賞罰内容2            
                    sp[85].CellValidator = SetValidatorToDate();                            // 資格賞罰年月日3          
                    sp[86].CellValidator = SetValidatorToString(40);                        // 資格賞罰名3              
                    sp[87].CellValidator = SetValidatorToString(40);                        // 資格賞罰内容3            
                    sp[88].CellValidator = SetValidatorToDate();                            // 資格賞罰年月日4          
                    sp[89].CellValidator = SetValidatorToString(40);                        // 資格賞罰名4              
                    sp[90].CellValidator = SetValidatorToString(40);                        // 資格賞罰内容4            
                    sp[91].CellValidator = SetValidatorToDate();                            // 資格賞罰年月日5          
                    sp[92].CellValidator = SetValidatorToString(40);                        // 資格賞罰名5              
                    sp[93].CellValidator = SetValidatorToString(40);                        // 資格賞罰内容5            
                    sp[94].CellValidator = SetValidatorToNumberDigit(9);                    // 事業者コード             
                    sp[95].CellValidator = SetValidatorToDate();                            // 健康保険加入日           
                    sp[96].CellValidator = SetValidatorToString(20);                        // 健康保険番号             
                    sp[97].CellValidator = SetValidatorToDate();                            // 厚生年金加入日           
                    sp[98].CellValidator = SetValidatorToString(20);                        // 厚生年金番号             
                    sp[99].CellValidator = SetValidatorToDate();                            // 雇用保険加入日           
                    sp[100].CellValidator = SetValidatorToString(20);                       // 雇用保険番号             
                    sp[101].CellValidator = SetValidatorToDate();                           // 労災保険加入日           
                    sp[102].CellValidator = SetValidatorToString(20);                       // 労災保険番号             
                    sp[103].CellValidator = SetValidatorToDate();                           // 厚生年金基金加入日       
                    sp[104].CellValidator = SetValidatorToString(20);                       // 厚生年金基金番号         
                    sp[105].CellValidator = SetValidatorToNumberDigit(99);                  // 通勤時間                 
                    sp[106].CellValidator = SetValidatorToNumberDigit(99);                  // 通勤分                   
                    sp[107].CellValidator = SetValidatorToString(20);                       // 家族連絡                 
                    sp[108].CellValidator = SetValidatorToNumberRange(0, 7);                // 住居の種類               
                    sp[109].CellValidator = SetValidatorToString(20);                       // 通勤方法                 
                    sp[110].CellValidator = SetValidatorToString(20);                       // 家族氏名1                
                    sp[111].CellValidator = SetValidatorToDate();                           // 家族生年月日1            
                    sp[112].CellValidator = SetValidatorToString(4);                        // 家族続柄1                
                    sp[113].CellValidator = SetValidatorToNumberRange(0, 3);                // 家族血液型1              
                    sp[114].CellValidator = SetValidatorToString(20);                       // 家族その他1              
                    sp[115].CellValidator = SetValidatorToString(20);                       // 家族氏名2                
                    sp[116].CellValidator = SetValidatorToDate();                           // 家族生年月日2            
                    sp[117].CellValidator = SetValidatorToString(4);                        // 家族続柄2                
                    sp[118].CellValidator = SetValidatorToNumberRange(0, 3);                // 家族血液型2              
                    sp[119].CellValidator = SetValidatorToString(20);                       // 家族その他2              
                    sp[120].CellValidator = SetValidatorToString(20);                       // 家族氏名3                
                    sp[121].CellValidator = SetValidatorToDate();                           // 家族生年月日3            
                    sp[122].CellValidator = SetValidatorToString(4);                        // 家族続柄3                
                    sp[123].CellValidator = SetValidatorToNumberRange(0, 3);                // 家族血液型3              
                    sp[124].CellValidator = SetValidatorToString(20);                       // 家族その他3              
                    sp[125].CellValidator = SetValidatorToString(20);                       // 家族氏名4                
                    sp[126].CellValidator = SetValidatorToDate();                           // 家族生年月日4            
                    sp[127].CellValidator = SetValidatorToString(4);                        // 家族続柄4                
                    sp[128].CellValidator = SetValidatorToNumberRange(0, 3);                // 家族血液型4              
                    sp[129].CellValidator = SetValidatorToString(20);                       // 家族その他4              
                    sp[130].CellValidator = SetValidatorToString(20);                       // 家族氏名5                
                    sp[131].CellValidator = SetValidatorToDate();                           // 家族生年月日5            
                    sp[132].CellValidator = SetValidatorToString(4);                        // 家族続柄5                
                    sp[133].CellValidator = SetValidatorToNumberRange(0, 3);                // 家族血液型5              
                    sp[134].CellValidator = SetValidatorToString(20);                       // 家族その他5              
                    sp[135].CellValidator = SetValidatorToDate();                           // 退職年月日               
                    sp[136].CellValidator = SetValidatorToString(50);                       // 退職理由                 
                    sp[137].CellValidator = SetValidatorToString(80);                       // 特記事項1                
                    sp[138].CellValidator = SetValidatorToString(80);                       // 特記事項2                
                    sp[139].CellValidator = SetValidatorToString(80);                       // 特記事項3                
                    sp[140].CellValidator = SetValidatorToString(80);                       // 特記事項4                
                    sp[141].CellValidator = SetValidatorToString(80);                       // 特記事項5                
                    sp[142].CellValidator = SetValidatorToDate();                           // 健康診断年月日1          
                    sp[143].CellValidator = SetValidatorToDate();                           // 健康診断年月日2          
                    sp[144].CellValidator = SetValidatorToDate();                           // 健康診断年月日3          
                    sp[145].CellValidator = SetValidatorToDate();                           // 健康診断年月日4          
                    sp[146].CellValidator = SetValidatorToDate();                           // 健康診断年月日5          
                    sp[147].CellValidator = SetValidatorToString(240);                      // 健康状態                 
                    sp[148].CellValidator = SetValidatorToNumberRange(0, 1);                // 水揚連動区分             
                    sp[149].CellValidator = SetValidatorToNumberDigit(9999999);             // 自社ID                   
                    sp[150].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定給与                 
                    sp[151].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定賞与積立金           
                    sp[152].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定福利厚生費           
                    sp[153].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定法定福利費           
                    sp[154].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定退職引当金           
                    sp[155].CellValidator = SetValidatorToNumberDigit(9999999);             // 固定労働保険             
                    sp[156].CellValidator = SetValidatorToString(20);                       // 個人ナンバー             
                    sp[157].CellValidator = SetValidatorToDate();                           // 削除日付                 

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員ID");

                    break;
                    #endregion

                case 10:
                    #region 適正診断データ
                    sp[0].CellValidator = SetValidatorToStringExists(DrvtableKEY, false);   // 乗務員KEY	
                    sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 明細行       
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDate();                             // 実施年月日   
                    sp[5].CellValidator = SetValidatorToNumberRange(0, 4);                  // 対象種類     
                    sp[6].CellValidator = SetValidatorToString(30);                         // 実施機関名   
                    sp[7].CellValidator = SetValidatorToString(30);                         // 所見摘要     
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員KEY");
                    targetKey.Add("明細行");

                    break;
                    #endregion

                case 11:
                    #region 事故違反履歴データ
                    sp[0].CellValidator = SetValidatorToStringExists(DrvtableKEY, false);   // 乗務員KEY	
                    sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 明細行       
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDate();                             // 発生年月日   
                    sp[5].CellValidator = SetValidatorToString(60);                         // 概要処置     
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員KEY");
                    targetKey.Add("明細行");

                    break;
                    #endregion

                case 12:
                    #region 特別教育
                    sp[0].CellValidator = SetValidatorToStringExists(DrvtableKEY, false);   // 乗務員KEY	
                    sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 明細行       
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDate();                             // 実施年月日   
                    sp[5].CellValidator = SetValidatorToNumberRange(0, 2);                  // 教育種類     
                    sp[6].CellValidator = SetValidatorToString(30);                         // 教育内容     
                    sp[7].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員KEY");
                    targetKey.Add("明細行");

                    break;
                    #endregion

                case 13:
                    #region 乗務員データ
                    sp[0].CellValidator = SetValidatorToStringExists(DrvtableKEY, false);			// 乗務員KEY	
                    sp[1].CellValidator = SetValidatorToYearMonth(false);                           // 集計年月     
                    sp[2].CellValidator = SetValidatorToDate();                                     // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                                     // 更新日時     
                    sp[4].CellValidator = SetValidatorToNumberDigit(99999);                  // 自社部門ID   
                    sp[5].CellValidator = SetValidatorToStringExists(SyatableID, false);            // 車種ID       
                    sp[6].CellValidator = SetValidatorToStringExists(CartableKEY, false);           // 車輌KEY      
                    sp[7].CellValidator = SetValidatorToDateNumber(31, false);                      // 営業日数     
                    sp[8].CellValidator = SetValidatorToDateNumber(31, false);                      // 稼働日数     
                    sp[9].CellValidator = SetValidatorToNumberDigit(9999999, false);                // 走行ＫＭ     
                    sp[10].CellValidator = SetValidatorToNumberDigit(9999999, false);               // 実車ＫＭ     
                    sp[11].CellValidator = SetValidatorToNumberRange(0, 999999999, false, false);   // 輸送屯数     
                    sp[12].CellValidator = SetValidatorToNumberDigit(9999999, false);               // 運送収入     
                    sp[13].CellValidator = SetValidatorToNumberDigit(9999999, false);               // 燃料Ｌ       
                    sp[14].CellValidator = SetValidatorToNumberDigit(9999999, false);               // 一般管理費   
                    sp[15].CellValidator = SetValidatorToNumberDigit(9999999, false);               // 拘束時間     
                    sp[16].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 運転時間     
                    sp[17].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 高速時間     
                    sp[18].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 作業時間     
                    sp[19].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 待機時間     
                    sp[20].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 休憩時間     
                    sp[21].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 残業時間     
                    sp[22].CellValidator = SetValidatorToNumberRange(0, 9999999, false, false);     // 深夜時間     
                    sp[23].CellValidator = SetValidatorToDate();                                    // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員KEY");
                    targetKey.Add("集計年月");
                    targetKey.Add("車輌KEY");

                    break;
                    #endregion

                case 14:
                    #region 乗務員経費データ
                    sp[0].CellValidator = SetValidatorToStringExists(DrvtableKEY, false);   // 乗務員KEY	
                    sp[1].CellValidator = SetValidatorToYearMonth(false);                   // 集計年月     
                    sp[2].CellValidator = SetValidatorToNumberDigit(999, false);            // 経費項目ID   
                    sp[3].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[4].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[5].CellValidator = SetValidatorToString(20, false);                  // 経費項目名   
                    sp[6].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 固定変動区分 
                    sp[7].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 金額         
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("乗務員KEY");
                    targetKey.Add("集計年月");
                    targetKey.Add("経費項目ID");

                    break;
                    #endregion

                //20150724 wada commentout バイナリの取込はできない。
                case 15:
                    //【取込不可】
                    break;

                case 16:
                    #region 車輌マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          	// 車輌ID			
                    sp[1].CellValidator = SetValidatorToDate();                             	// 登録日時         
                    sp[2].CellValidator = SetValidatorToDate();                                 // 更新日時         
                    sp[3].CellValidator = SetValidatorToString(6, false);                       // 車輌番号         
                    sp[4].CellValidator = SetValidatorToNumberDigit(99999);                     // 自社部門ID       
                    sp[5].CellValidator = SetValidatorToStringExists(SyatableID);        // 車種ID           
                    sp[6].CellValidator = SetValidatorToStringExists(DrvtableKEY);       // 乗務員KEY        
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 9, true, false);         // 運輸局ID         
                    sp[8].CellValidator = SetValidatorToNumberRange(0, 1, true, false);         // 自傭区分         
                    sp[9].CellValidator = SetValidatorToNumberRange(0, 1, true, false);         // 廃車区分         
                    sp[10].CellValidator = SetValidatorToDate();                                // 廃車日           
                    sp[11].CellValidator = SetValidatorToDate();                                // 次回車検日       
                    sp[12].CellValidator = SetValidatorToString(28);                            // 車輌登録番号     
                    sp[13].CellValidator = SetValidatorToDate();                                // 登録日           
                    sp[14].CellValidator = SetValidatorToNumberDigit(9999, false);              // 初年度登録年     
                    sp[15].CellValidator = SetValidatorToDateNumber(12, false);                 // 初年度登録月     
                    sp[16].CellValidator = SetValidatorToString(10);                            // 自動車種別       
                    sp[17].CellValidator = SetValidatorToString(10);                            // 用途             
                    sp[18].CellValidator = SetValidatorToString(10);                            // 自家用事業用     
                    sp[19].CellValidator = SetValidatorToString(20);                            // 車体形状         
                    sp[20].CellValidator = SetValidatorToString(20);                            // 車名             
                    sp[21].CellValidator = SetValidatorToString(20);                            // 型式             
                    sp[22].CellValidator = SetValidatorToNumberDigit(99999, false);             // 乗車定員         
                    sp[23].CellValidator = SetValidatorToNumberDigit(9999999, false);           // 車輌重量         
                    sp[24].CellValidator = SetValidatorToNumberDigit(9999999, false);           // 車輌最大積載量   
                    sp[25].CellValidator = SetValidatorToNumberDigit(9999999, false);           // 車輌総重量       
                    sp[26].CellValidator = SetValidatorToNumberDigit(9999999, false);           // 車輌実積載量     
                    sp[27].CellValidator = SetValidatorToString(20);                            // 車台番号         
                    sp[28].CellValidator = SetValidatorToString(20);                            // 原動機型式       
                    sp[29].CellValidator = SetValidatorToNumberDigit(99999, false);             // 長さ             
                    sp[30].CellValidator = SetValidatorToNumberDigit(99999, false);             // 幅               
                    sp[31].CellValidator = SetValidatorToNumberDigit(99999, false);             // 高さ             
                    sp[32].CellValidator = SetValidatorToNumberDigit(9999999, false);           // 総排気量         
                    sp[33].CellValidator = SetValidatorToString(10);                            // 燃料種類         
                    sp[34].CellValidator = SetValidatorToNumberDigit(999999999, false);         // 現在メーター     
                    sp[35].CellValidator = SetValidatorToNumberDigit(999999999);         // デジタコCD       
                    sp[36].CellValidator = SetValidatorToString(40);                            // 所有者名         
                    sp[37].CellValidator = SetValidatorToString(60);                            // 所有者住所       
                    sp[38].CellValidator = SetValidatorToString(40);                            // 使用者名         
                    sp[39].CellValidator = SetValidatorToString(60);                            // 使用者住所       
                    sp[40].CellValidator = SetValidatorToString(40);                            // 使用本拠位置     
                    sp[41].CellValidator = SetValidatorToString(50);                            // 備考             
                    sp[42].CellValidator = SetValidatorToString(10);                            // 型式指定番号     
                    sp[43].CellValidator = SetValidatorToNumberDigit(99999, false);             // 前前軸重         
                    sp[44].CellValidator = SetValidatorToNumberDigit(99999, false);             // 前後軸重         
                    sp[45].CellValidator = SetValidatorToNumberDigit(99999, false);             // 後前軸重         
                    sp[46].CellValidator = SetValidatorToNumberDigit(99999, false);             // 後後軸重         
                    sp[47].CellValidator = SetValidatorToNumberRange(0, 9, false, false);       // 燃料費単価       
                    sp[48].CellValidator = SetValidatorToNumberRange(0, 9, false, false);       // 油脂費単価       
                    sp[49].CellValidator = SetValidatorToNumberRange(0, 9, false, false);       // タイヤ費単価     
                    sp[50].CellValidator = SetValidatorToNumberRange(0, 9, false, false);       // 修繕費単価       
                    sp[51].CellValidator = SetValidatorToNumberRange(0, 9, false, false);       // 車検費単価       
                    sp[52].CellValidator = SetValidatorToNumberDigit(9, false);                 // CO2区分          
                    sp[53].CellValidator = SetValidatorToNumberRange(0, 999999, false, false);  // 基準燃費         
                    sp[54].CellValidator = SetValidatorToNumberRange(0, 999999, false, false);  // 黒煙規制値       
                    sp[55].CellValidator = SetValidatorToNumberDigit(99999);                    // G車種ID          
                    sp[56].CellValidator = SetValidatorToNumberDigit(99999);                    // 規制区分ID       
                    sp[57].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定自動車税     
                    sp[58].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定重量税       
                    sp[59].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定取得税       
                    sp[60].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定自賠責保険   
                    sp[61].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定車輌保険     
                    sp[62].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定対人保険     
                    sp[63].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定対物保険     
                    sp[64].CellValidator = SetValidatorToNumberDigit(9999999);                  // 固定貨物保険     
                    sp[65].CellValidator = SetValidatorToDate();                                // 削除日付         

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌ID");
                    //targetKey.Add("乗務員KEY");

                    break;
                    #endregion

                case 17:
                    #region 車輌移動履歴データ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToDate(false);                        // 配置年月日   
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToString(20);                         // 営業所名     
                    sp[5].CellValidator = SetValidatorToString(20);                         // 転出先       
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("配置年月日");

                    break;
                    #endregion

                case 18:
                    #region 強陪保険データ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToDate(false);                        // 加入年月日   
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDate(false);                        // 期限         
                    sp[5].CellValidator = SetValidatorToString(20);                         // 契約先       
                    sp[6].CellValidator = SetValidatorToString(15);                         // 保険証番号   
                    sp[7].CellValidator = SetValidatorToDateNumber(12, false);              // 月数         
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("加入年月日");

                    break;
                    #endregion

                case 19:
                    #region 任意保険データ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToDate(false);                        // 加入年月日   
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDate(false);                        // 期限         
                    sp[5].CellValidator = SetValidatorToString(15);                         // 契約先       
                    sp[6].CellValidator = SetValidatorToString(20);                         // 保険証番号   
                    sp[7].CellValidator = SetValidatorToDateNumber(12, false);              // 月数         
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("加入年月日");

                    break;
                    #endregion

                case 20:
                    #region 車輌納税データ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToNumberDigit(9999, false);           // 年度         
                    sp[2].CellValidator = SetValidatorToYearMonth();                        // 自動車税年月 
                    sp[3].CellValidator = SetValidatorToNumberDigit(999999999);             // 自動車税     
                    sp[4].CellValidator = SetValidatorToYearMonth();                        // 重量税年月   
                    sp[5].CellValidator = SetValidatorToNumberDigit(999999999);             // 重量税       
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("年度");

                    break;
                    #endregion

                case 21:
                    #region 車輌点検データ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToYearMonth(false);                   // 年月         
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToDateNumber(31, false);              // 点検日       
                    sp[5].CellValidator = SetValidatorToString(4);                          // チェック     
                    sp[6].CellValidator = SetValidatorToNumberRange(0, 1);                  // エアコン区分 
                    sp[7].CellValidator = SetValidatorToString(40);                         // エアコン備考 
                    sp[8].CellValidator = SetValidatorToNumberRange(0, 1);                  // 異音区分     
                    sp[9].CellValidator = SetValidatorToString(40);                         // 異音備考     
                    sp[10].CellValidator = SetValidatorToNumberRange(0, 1);                 // 排気区分     
                    sp[11].CellValidator = SetValidatorToString(40);                        // 排気備考     
                    sp[12].CellValidator = SetValidatorToNumberRange(0, 1);                 // 燃費区分     
                    sp[13].CellValidator = SetValidatorToString(40);                        // 燃費備考     
                    sp[14].CellValidator = SetValidatorToNumberRange(0, 1);                 // その他区分   
                    sp[15].CellValidator = SetValidatorToString(40);                        // その他備考   
                    sp[16].CellValidator = SetValidatorToStringExists(DrvtableKEY);         // 乗務員KEY    
                    sp[17].CellValidator = SetValidatorToString(20);                        // 乗務員名     
                    sp[18].CellValidator = SetValidatorToString(255);                       // 整備指示     
                    sp[19].CellValidator = SetValidatorToDate();                            // 指示日付     
                    sp[20].CellValidator = SetValidatorToString(255);                       // 整備部品     
                    sp[21].CellValidator = SetValidatorToDate();                            // 部品日付     
                    sp[22].CellValidator = SetValidatorToString(255);                       // 整備結果     
                    sp[23].CellValidator = SetValidatorToDate();                            // 結果日付     
                    sp[24].CellValidator = SetValidatorToDate();                            // 削除日付     


                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("年月");
                    targetKey.Add("点検日");

                    break;
                    #endregion

                case 22:
                    #region 車種マスタ

                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 車種ID	
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時 
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時 
                    sp[3].CellValidator = SetValidatorToString(20);                         // 車種名   
                    sp[4].CellValidator = SetValidatorToNumberRange(0, 9999, false);        // 積載重量 
                    sp[5].CellValidator = SetValidatorToDate();                             // 削除日付 

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車種ID");

                    break;
                    #endregion

                case 23:
                    #region 経費マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(999, false);            // 経費項目ID	
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(20);                         // 経費項目名   
                    sp[4].CellValidator = SetValidatorToNumberRange(0, 1, true, false);     // 固定変動区分 
                    sp[5].CellValidator = SetValidatorToNumberRange(0, 1);                  // 編集区分     
                    sp[6].CellValidator = SetValidatorToNumberRange(0, 1);                  // グリーン区分 
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 6);                  // 経費区分     
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("経費項目ID");

                    break;
                    #endregion

                case 24:
                    #region 部品交換予定データ
                    sp[0].CellValidator = SetValidatorToNumberDigit(999, false);            // 経費項目ID	
                    sp[1].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY      
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToNumberDigit(9999999);               // 交換期間     
                    sp[5].CellValidator = SetValidatorToNumberDigit(9999999);               // 交換距離     
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("経費項目ID");
                    targetKey.Add("車輌KEY");

                    break;
                    #endregion

                case 25:
                    #region 地区マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 発着地ID			
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時         
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時         
                    sp[3].CellValidator = SetValidatorToString(30);                         // 発着地名         
                    sp[4].CellValidator = SetValidatorToString(10);                         // かな読み         
                    sp[5].CellValidator = SetValidatorToNumberDigit(9999999);               // タリフ距離       
                    sp[6].CellValidator = SetValidatorToString(8);                          // 郵便番号         
                    sp[7].CellValidator = SetValidatorToString(50);                         // 住所１           
                    sp[8].CellValidator = SetValidatorToString(50);                         // 住所２           
                    sp[9].CellValidator = SetValidatorToString(15);                         // 電話番号         
                    sp[10].CellValidator = SetValidatorToString(15);                        // ＦＡＸ番号       
                    sp[11].CellValidator = SetValidatorToNumberDigit(9999999);              // 配送エリアID     
                    sp[12].CellValidator = SetValidatorToDate();                            // 削除日付         

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("発着地ID");

                    break;
                    #endregion

                case 26:
                    #region 商品マスタ

                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 商品ID		
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(30);                         // 商品名       
                    sp[4].CellValidator = SetValidatorToString(10);                         // かな読み     
                    sp[5].CellValidator = SetValidatorToString(4);                          // 単位         
                    sp[6].CellValidator = SetValidatorToNumberRange(0, 999999999, false);   // 商品重量     
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 999999999, false);   // 商品才数     
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("商品ID");

                    break;
                    #endregion

                case 27:
                    #region 請求内訳マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(ToktableKEY, false);   // 得意先KEY	
                    sp[1].CellValidator = SetValidatorToNumberDigit(99999, false);          // 請求内訳ID   
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToString(30, false);                  // 請求内訳名   
                    sp[5].CellValidator = SetValidatorToString(10);                         // かな読み     
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("得意先KEY");
                    targetKey.Add("請求内訳ID");

                    break;
                    #endregion

                case 28:
                    #region 摘要マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 摘要ID		
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(10);                         // かな読み     
                    sp[4].CellValidator = SetValidatorToString(30, false);                  // 摘要名       
                    sp[5].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("摘要ID");

                    break;
                    #endregion

                case 29:
                    #region 規制マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 規制区分ID	
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(60);                         // 規制名       
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("規制区分ID");

                    break;
                    #endregion

                case 30:
                    #region 燃費目標マスタ
                    sp[0].CellValidator = SetValidatorToStringExists(CartableKEY, false);   // 車輌KEY		
                    sp[1].CellValidator = SetValidatorToYearMonth(false);                   // 年月         
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[3].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[4].CellValidator = SetValidatorToNumberRange(0, 99999, false);       // 目標燃費     
                    sp[5].CellValidator = SetValidatorToDate();                             // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("車輌KEY");
                    targetKey.Add("年月");

                    break;
                    #endregion

                case 31:
                    #region グリーン車種マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // G車種ID			
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時         
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時         
                    sp[3].CellValidator = SetValidatorToString(50, false);                  // G車種名          
                    sp[4].CellValidator = SetValidatorToString(16);                         // 略称名           
                    sp[5].CellValidator = SetValidatorToNumberRange(0, 9999999, false);     // CO2排出係数１    
                    sp[6].CellValidator = SetValidatorToNumberRange(0, 9999999, false);     // CO2排出係数２    
                    sp[7].CellValidator = SetValidatorToNumberRange(0, 1);                  // 事業用区分       
                    sp[8].CellValidator = SetValidatorToNumberRange(0, 1);                  // ディーゼル区分   
                    sp[9].CellValidator = SetValidatorToNumberRange(0, 1);                  // 小型普通区分     
                    sp[10].CellValidator = SetValidatorToNumberRange(0, 1);                 // 低公害区分       
                    sp[11].CellValidator = SetValidatorToDate();                            // 削除日付         

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("G車種ID");

                    break;
                    #endregion

                // 20150727 wada commentout 取込対象外のマスタ
                case 32:
                    #region 請求項目マスタ
                    break;
                    #endregion

                case 33:
                    #region 自社マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 自社ID		
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(40, false);                  // 自社名       
                    sp[4].CellValidator = SetValidatorToString(40, false);                  // 代表者名     
                    sp[5].CellValidator = SetValidatorToString(8, false);                   // 郵便番号     
                    sp[6].CellValidator = SetValidatorToString(40, false);                  // 住所１       
                    sp[7].CellValidator = SetValidatorToString(40, false);                  // 住所２       
                    sp[8].CellValidator = SetValidatorToString(15, false);                  // 電話番号     
                    sp[9].CellValidator = SetValidatorToString(15, false);                  // ＦＡＸ       
                    sp[10].CellValidator = SetValidatorToString(50);                        // 振込銀行１   
                    sp[11].CellValidator = SetValidatorToString(50);                        // 振込銀行２   
                    sp[12].CellValidator = SetValidatorToString(50);                        // 振込銀行３   
                    sp[13].CellValidator = SetValidatorToString(20);                        // 法人ナンバー 
                    sp[14].CellValidator = SetValidatorToDate();                            // 削除日付     

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("自社ID");

                    break;
                    #endregion

                case 34:
                    #region 自社部門マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 自社部門ID		
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時         
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時         
                    sp[3].CellValidator = SetValidatorToString(30, false);                  // 自社部門名       
                    sp[4].CellValidator = SetValidatorToString(10);                         // かな読み         
                    sp[5].CellValidator = SetValidatorToString(20);                         // 法人ナンバー     
                    sp[6].CellValidator = SetValidatorToDate();                             // 削除日付         

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("自社部門ID");

                    break;
                    #endregion

                case 35:
                    #region 担当者マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 担当者ID		
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時     
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時     
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 担当者名     
                    sp[4].CellValidator = SetValidatorToString(10);                         // かな読み     
                    sp[5].CellValidator = SetValidatorToString(10,false);                  // パスワード   
                    sp[6].CellValidator = SetValidatorToNumberDigit(9999999, false);        // グループ権限ID
                    sp[7].CellValidator = SetValidatorToString(20);                         // 個人ナンバー 
                    sp[8].CellValidator = SetValidatorToDate();                             // 削除日付     
                    sp[9].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 自社部門ID   
                    sp[10].CellValidator = SetValidatorToNumberDigit(9999999);              // 利用者コード
                    sp[11].CellValidator = SetValidatorToString(20);                        // PGM_ID
                    sp[12].CellValidator = SetValidatorToString(20);                        // 端末ID

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("担当者ID");

                    break;
                    #endregion

                case 36:
                    #region 消費税率マスタ
                    sp[0].CellValidator = SetValidatorToDate(false);                        // 適用開始日付
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToNumberDigit(9999999);         　　  // 消費税率
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日時

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("適用開始日付");

                    break;
                    #endregion

                case 37:
                    #region 歩合計算区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 歩合計算区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 歩合計算名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("歩合計算区分ID");

                    break;
                    #endregion

                case 38:
                    #region 取引区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 取引区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 取引区分名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("取引区分ID");

                    break;
                    #endregion

                case 39:
                    #region 出勤区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99, false);             // 出勤区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(4, false);                   // 出勤区分名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("出勤区分ID");

                    break;
                    #endregion

                case 40:
                    #region 税区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(9, false);              // 税区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 税区分名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("税区分ID");

                    break;
                    #endregion

                case 41:
                    #region 親子区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(9, false);              // 親子区分
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 親子区分名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("親子区分ID");

                    break;
                    #endregion

                case 42:
                    #region 請求書区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99, false);             // 請求書区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 請求書名
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("請求書区分ID");

                    break;
                    #endregion

                case 43:
                    #region 運賃計算区分マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(9, false);              // 運賃計算区分ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 運賃計算区分
                    sp[4].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("運賃計算区分ID");

                    break;
                    #endregion

                case 44:
                    #region 運輸局マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99, false);             // 運輸局ID
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時
                    sp[3].CellValidator = SetValidatorToString(20, false);                  // 運輸局名
                    sp[4].CellValidator = SetValidatorToString(20);                         // 法人ナンバー
                    sp[5].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("運輸局ID");

                    break;
                    #endregion

                case 45:
                    #region 管理マスタ
                    sp[0].CellValidator = SetValidatorToNumberDigit(99999, false);          // 管理ID					
                    sp[1].CellValidator = SetValidatorToDate();                             // 登録日時                 
                    sp[2].CellValidator = SetValidatorToDate();                             // 更新日時                 
                    sp[3].CellValidator = SetValidatorToNumberDigit(9999999);               // 得意先管理処理年月       
                    sp[4].CellValidator = SetValidatorToNumberDigit(9999999);               // 支払先管理処理年月       
                    sp[5].CellValidator = SetValidatorToNumberDigit(9999999);               // 車輌管理処理年月         
                    sp[6].CellValidator = SetValidatorToNumberDigit(9999999);               // 運転者管理処理年月       
                    sp[7].CellValidator = SetValidatorToNumberDigit(9999999);               // 更新年月                 
                    sp[8].CellValidator = SetValidatorToNumberDigit(9999999);               // 決算月                   
                    sp[9].CellValidator = SetValidatorToNumberDigit(9999999);               // 得意先自社締日           
                    sp[10].CellValidator = SetValidatorToNumberDigit(9999999);              // 支払先自社締日           
                    sp[11].CellValidator = SetValidatorToNumberDigit(9999999);              // 運転者自社締日           
                    sp[12].CellValidator = SetValidatorToNumberDigit(9999999);              // 車輌自社締日             
                    sp[13].CellValidator = SetValidatorToNumberDigit(9999999);              // 自社支払日               
                    sp[14].CellValidator = SetValidatorToNumberDigit(9999999);              // 自社サイト               
                    sp[15].CellValidator = SetValidatorToNumberDigit(9999999);              // 未定区分                 
                    sp[16].CellValidator = SetValidatorToNumberDigit(9999999);              // 部門管理区分             
                    sp[17].CellValidator = SetValidatorToString(10);                        // 割増料金名１             
                    sp[18].CellValidator = SetValidatorToString(10);                        // 割増料金名２             
                    sp[19].CellValidator = SetValidatorToNumberDigit(9999999);              // 得意先ID区分             
                    sp[20].CellValidator = SetValidatorToNumberDigit(9999999);              // 支払先ID区分             
                    sp[21].CellValidator = SetValidatorToNumberDigit(9999999);              // 乗務員ID区分             
                    sp[22].CellValidator = SetValidatorToNumberDigit(9999999);              // 車輌ID区分               
                    sp[23].CellValidator = SetValidatorToNumberDigit(9999999);              // 車種ID                   
                    sp[24].CellValidator = SetValidatorToNumberDigit(9999999);              // 発着地ID区分             
                    sp[25].CellValidator = SetValidatorToNumberDigit(9999999);              // 品名ID区分               
                    sp[26].CellValidator = SetValidatorToNumberDigit(9999999);              // 摘要ID区分               
                    sp[27].CellValidator = SetValidatorToYearMonth();                       // 期首年月                 
                    sp[28].CellValidator = SetValidatorToNumberDigit(99);                   // 売上消費税端数区分       
                    sp[29].CellValidator = SetValidatorToNumberDigit(99);                   // 支払消費税端数区分       
                    sp[30].CellValidator = SetValidatorToNumberDigit(9);                    // 金額計算端数区分         
                    sp[31].CellValidator = SetValidatorToNumberDigit(9);                    // 出力プリンター設定       
                    sp[32].CellValidator = SetValidatorToNumberDigit(9);                    // 自動学習区分             
                    sp[33].CellValidator = SetValidatorToNumberDigit(9);                    // 月次集計区分             
                    sp[34].CellValidator = SetValidatorToNumberDigit(9);                    // 距離転送区分             
                    sp[35].CellValidator = SetValidatorToNumberDigit(9);                    // 番号通知区分             
                    sp[36].CellValidator = SetValidatorToNumberDigit(9);                    // 通行料転送区分           
                    sp[37].CellValidator = SetValidatorToNumberDigit(9);                    // 路線計算区分             
                    sp[38].CellValidator = SetValidatorToNumberDigit(9999);                 // Ｇ期首月日               
                    sp[39].CellValidator = SetValidatorToNumberDigit(9999);                 // Ｇ期末月日               
                    sp[40].CellValidator = SetValidatorToNumberDigit(9);                    // 請求書区分               
                    sp[41].CellValidator = SetValidatorToDate();                            // 削除日付                 

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("管理ID");

                    break;
                    #endregion

                // 20150727 wada commentout 取込対象外のマスタ
                case 46:
                    #region 明細番号マスタ
                    //【取込不可】
                    break;
                    #endregion

                // 20150727 wada commentout 取込対象外のマスタ
                case 47:
                    #region グリッド表示マスタ
                    //【取込不可】
                    break;
                    #endregion

                case 48:
                    #region 燃料単価マスタ

                    sp[0].CellValidator = SetValidatorToDate(false);                        // 適用開始年月日
                    sp[1].CellValidator = SetValidatorToNumberDigit(9999999, false);        // 支払先KEY
                    sp[2].CellValidator = SetValidatorToDate();                             // 登録日時
                    sp[3].CellValidator = SetValidatorToDate();                             // 変更日時
                    sp[4].CellValidator = SetValidatorToNumberRange(0, 9999999, false);     // 燃料単価
                    sp[5].CellValidator = SetValidatorToDate();                             // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("適用開始年月日");
                    targetKey.Add("支払先KEY");

                    break;
                    #endregion

                case 49:
                    #region 軽油取引税マスタ
                    sp[0].CellValidator = SetValidatorToDate(false);                    // 適用開始年月日
                    sp[1].CellValidator = SetValidatorToDate();                         // 登録日時
                    sp[2].CellValidator = SetValidatorToDate();                         // 変更日時
                    sp[3].CellValidator = SetValidatorToNumberRange(0, 9999999, false); // 燃料単価
                    sp[4].CellValidator = SetValidatorToDate();                         // 削除日付

                    // 主キーチェックフィールドをセットする。
                    targetKey.Add("適用開始年月日");

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
                        if (sp[col].CellValidator.ComparisonOperator == ComparisonOperator.LessThanOrEqualTo)
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

        ///// <summary>
        ///// 20150723 wada add 文字列の桁数を指定したValidator
        ///// </summary>
        ///// <param name="value">最大文字数</param>
        ///// <param name="nullable">NULLを許容するかどうか</param>
        ///// <returns>Spread用のセットしたCellValidator</returns>
        //private CellValidator SetValidatorToString2(int value, int a, bool nullable = true)
        //{
        //    value += 1;
        //    int cnt = 0;
        //    //LassThanOrEqualTo→LessThan
        //    CellValidator cv = new CellValidator();
        //    foreach (var rows in 取込データ.Rows)
        //    {
        //        string stest = CsvData[cnt, a].Value.ToString();
        //        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        //        int test = sjisEnc.GetByteCount(stest);
        //        if (value <= test)
        //        {
        //            CsvData[cnt, a].Validator = CellValidator.CreateFormulaValidator(" '" + value + "' <= '" + test + "' ", value.ToString() + "文字以下で入力してください。");
        //        }
        //        cv.IgnoreBlank = nullable;
        //        cnt++;
        //    }
        //    return cv;
        //}

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
                    tablenm = "M01_TOK";
                    break;
                case 1:
                    tablenm = "M02_TTAN1";
                    break;
                case 2:
                    tablenm = "M02_TTAN2";
                    break;
                case 3:
                    tablenm = "M02_TTAN3";
                    break;
                case 4:
                    tablenm = "M02_TTAN4";
                    break;
                case 5:
                    tablenm = "M03_YTAN1";
                    break;
                case 6:
                    tablenm = "M03_YTAN2";
                    break;
                case 7:
                    tablenm = "M03_YTAN3";
                    break;
                case 8:
                    tablenm = "M03_YTAN4";
                    break;
                case 9:
                    tablenm = "M04_DRV";
                    break;
                case 10:
                    tablenm = "M04_DDT1";
                    break;
                case 11:
                    tablenm = "M04_DDT2";
                    break;
                case 12:
                    tablenm = "M04_DDT3";
                    break;
                case 13:
                    tablenm = "M04_DRD";
                    break;
                case 14:
                    tablenm = "M04_DRSB";
                    break;
                //case 15:
                //    tablenm = "T04_NYUK";
                //    break;
                case 16:
                    tablenm = "M05_CAR";
                    break;
                case 17:
                    tablenm = "M05_CDT1";
                    break;
                case 18:
                    tablenm = "M05_CDT2";
                    break;
                case 19:
                    tablenm = "M05_CDT3";
                    break;
                case 20:
                    tablenm = "M05_CDT4";
                    break;
                case 21:
                    tablenm = "M05_TEN";
                    break;
                case 22:
                    tablenm = "M06_SYA";
                    break;
                case 23:
                    tablenm = "M07_KEI";
                    break;
                case 24:
                    tablenm = "M07_KOU";
                    break;
                case 25:
                    tablenm = "M08_TIK";
                    break;
                case 26:
                    tablenm = "M09_HIN";
                    break;
                case 27:
                    tablenm = "M10_UHK";
                    break;
                case 28:
                    tablenm = "M11_TEK";
                    break;
                case 29:
                    tablenm = "M12_KIS";
                    break;
                case 30:
                    tablenm = "M13_MOK";
                    break;
                case 31:
                    tablenm = "M14_GSYA";
                    break;
                //case 32:
                //    tablenm = "T04_NYUK";
                //    break;
                case 33:
                    tablenm = "M70_JIS";
                    break;
                case 34:
                    tablenm = "M71_BUM";
                    break;
                case 35:
                    tablenm = "M72_TNT";
                    break;
                case 36:
                    tablenm = "M73_ZEI";
                    break;
                case 37:
                    tablenm = "M76_DBU";
                    break;
                case 38:
                    tablenm = "M77_TRH";
                    break;
                case 39:
                    tablenm = "M78_SYK";
                    break;
                case 40:
                    tablenm = "M79_ZKB";
                    break;
                case 41:
                    tablenm = "M81_OYK";
                    break;
                case 42:
                    tablenm = "M82_SEI";
                    break;
                case 43:
                    tablenm = "M83_UKE";
                    break;
                case 44:
                    tablenm = "M84_RIK";
                    break;
                case 45:
                    tablenm = "M87_CNTL";
                    break;
                //case 46:
                //    tablenm = "M82_SEI";
                //    break;
                //case 47:
                //    tablenm = "M82_SEI";
                //    break;
                case 48:
                    tablenm = "M91_OTAN";
                    break;
                case 49:
                    tablenm = "M92_KZEI";
                    break;
            }

            CommunicationObject com
                = new CommunicationObject(MessageType.RequestData, SEARCH_MST90060_00, tablenm);
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
                        var CSV00KeyList = new List<int>();
                        int CSV00Key;

                        // 20150716 wada add 適切なExceptionに修正する。
                        //if (取込データ.Columns.Count != 43 || !取込データ.Columns.Contains("得意先ID"))
                        //{
                        //    throw new CSVException();
                        //}

                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["得意先ID"].ToString(), out CSV00Key))
                            {
                                CSV00KeyList.Add(CSV00Key);
                            }
                        }
                        int[] CSV00LKey = CSV00KeyList.ToArray();

                        // 20150716 wada commentout
                        //if (取込データ.Columns.Count != 44)
                        //{
                        //    throw new ArithmeticException();
                        //}

                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_00, new object[] { CSV00LKey }));
                        break;
                    case 01:

                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_01, new object[] { }));
                        break;

                    case 02:
                        //if (取込データ.Columns.Count != 8)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_02, new object[] { }));
                        break;

                    case 03:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_03, new object[] { }));
                        break;

                    case 04:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_04, new object[] { }));
                        break;

                    case 05:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_05, new object[] { }));
                        break;

                    case 06:
                        //if (取込データ.Columns.Count != 8)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_06, new object[] { }));
                        break;

                    case 07:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_07, new object[] { }));
                        break;

                    case 08:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_08, new object[] { }));
                        break;

                    case 09:
                        var CSV09KeyList = new List<int>();
                        int CSV09Key;

                        //if (取込データ.Columns.Count != 159 || !取込データ.Columns.Contains("乗務員ID"))
                        //{
                        //    throw new CSVException();
                        //}

                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["乗務員ID"].ToString(), out CSV09Key))
                            {
                                CSV09KeyList.Add(CSV09Key);
                            }
                        }
                        int[] CSV09LKey = CSV09KeyList.ToArray();

                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_09, new object[] { CSV09LKey }));
                        break;

                    case 10:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_10, new object[] { }));
                        break;

                    case 11:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_11, new object[] { }));
                        break;

                    case 12:
                        //if (取込データ.Columns.Count != 8)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_12, new object[] { }));
                        break;

                    case 13:
                        //if (取込データ.Columns.Count != 24)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_13, new object[] { }));
                        break;

                    case 14:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_14, new object[] { }));
                        break;

                    case 15:
                        //if (取込データ.Columns.Count != 3)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_15, new object[] { }));
                        break;

                    case 16:

                        var CSV16KeyList = new List<int>();
                        int CSV16Key;

                        // 20150730 wada modify フィールド数を修正
                        //if (取込データ.Columns.Count != 67 || !取込データ.Columns.Contains("車輌ID"))
                        //if (取込データ.Columns.Count != 66 || !取込データ.Columns.Contains("車輌ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["車輌ID"].ToString(), out CSV16Key))
                            {
                                CSV16KeyList.Add(CSV16Key);
                            }
                        }
                        int[] CSV16LKey = CSV16KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_16, new object[] { CSV16LKey }));
                        break;

                    case 17:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_17, new object[] { }));
                        break;

                    case 18:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_18, new object[] { }));
                        break;

                    case 19:
                        //if (取込データ.Columns.Count != 9)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_19, new object[] { }));
                        break;

                    case 20:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_20, new object[] { }));
                        break;

                    case 21:
                        //if (取込データ.Columns.Count != 25)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_21, new object[] { }));
                        break;

                    case 22:

                        var CSV22KeyList = new List<int>();
                        int CSV22Key;
                        //if (取込データ.Columns.Count != 6 || !取込データ.Columns.Contains("車種ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["車種ID"].ToString(), out CSV22Key))
                            {
                                CSV22KeyList.Add(CSV22Key);
                            }
                        }
                        int[] CSV22LKey = CSV22KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_22, new object[] { CSV22LKey }));
                        break;

                    case 23:

                        var CSV23KeyList = new List<int>();
                        int CSV23Key;
                        //if (取込データ.Columns.Count != 9 || !取込データ.Columns.Contains("経費項目ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["経費項目ID"].ToString(), out CSV23Key))
                            {
                                CSV23KeyList.Add(CSV23Key);
                            }
                        }
                        int[] CSV23LKey = CSV23KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_23, new object[] { CSV23LKey }));
                        break;

                    case 24:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_24, new object[] { }));
                        break;

                    case 25:
                        var KeyList1 = new List<int>();
                        int Key1;
                        //if (取込データ.Columns.Count != 13 || !取込データ.Columns.Contains("発着地ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["発着地ID"].ToString(), out Key1))
                            {
                                KeyList1.Add(Key1);
                            }
                        }
                        int[] LKey1 = KeyList1.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_25, new object[] { LKey1 }));
                        break;

                    case 26:

                        var CSV26KeyList = new List<int>();
                        int CSV26Key;
                        //if (取込データ.Columns.Count != 9 || !取込データ.Columns.Contains("商品ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["商品ID"].ToString(), out CSV26Key))
                            {
                                CSV26KeyList.Add(CSV26Key);
                            }
                        }
                        int[] CSV26LKey = CSV26KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_26, new object[] { CSV26LKey }));
                        break;

                    case 27:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_27, new object[] { }));
                        break;

                    case 28:

                        var CSV28KeyList = new List<int>();
                        int CSV28Key;
                        //if (取込データ.Columns.Count != 6 || !取込データ.Columns.Contains("摘要ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["摘要ID"].ToString(), out CSV28Key))
                            {
                                CSV28KeyList.Add(CSV28Key);
                            }
                        }
                        int[] CSV28LKey = CSV28KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_28, new object[] { CSV28LKey }));
                        break;

                    case 29:

                        var CSV29KeyList = new List<int>();
                        int CSV29Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("規制区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["規制区分ID"].ToString(), out CSV29Key))
                            {
                                CSV29KeyList.Add(CSV29Key);
                            }
                        }
                        int[] CSV29LKey = CSV29KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_29, new object[] { CSV29LKey }));
                        break;

                    case 30:
                        //if (取込データ.Columns.Count != 6)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_30, new object[] { }));
                        break;

                    case 31:

                        var CSV31KeyList = new List<int>();
                        int CSV31Key;
                        //if (取込データ.Columns.Count != 12 || !取込データ.Columns.Contains("G車種ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["G車種ID"].ToString(), out CSV31Key))
                            {
                                CSV31KeyList.Add(CSV31Key);
                            }
                        }
                        int[] CSV31LKey = CSV31KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_31, new object[] { CSV31LKey }));
                        break;

                    case 33:

                        var CSV33KeyList = new List<int>();
                        int CSV33Key;

                        // 20150730 wada modify フィールド数を修正
                        //if (取込データ.Columns.Count != 16 || !取込データ.Columns.Contains("自社ID"))
                        //if (取込データ.Columns.Count != 15 || !取込データ.Columns.Contains("自社ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["自社ID"].ToString(), out CSV33Key))
                            {
                                CSV33KeyList.Add(CSV33Key);
                            }
                        }
                        int[] CSV33LKey = CSV33KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_33, new object[] { CSV33LKey }));
                        break;

                    case 34:

                        var CSV34KeyList = new List<int>();
                        int CSV34Key;
                        //if (取込データ.Columns.Count != 7 || !取込データ.Columns.Contains("自社部門ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["自社部門ID"].ToString(), out CSV34Key))
                            {
                                CSV34KeyList.Add(CSV34Key);
                            }
                        }
                        int[] CSV34LKey = CSV34KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_34, new object[] { CSV34LKey }));
                        break;

                    case 35:

                        var CSV35KeyList = new List<int>();
                        int CSV35Key;

                        // 20150730 wada modify フィールド数を修正
                        //if (取込データ.Columns.Count != 14 || !取込データ.Columns.Contains("担当者ID"))
                        //if (取込データ.Columns.Count != 13 || !取込データ.Columns.Contains("担当者ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["担当者ID"].ToString(), out CSV35Key))
                            {
                                CSV35KeyList.Add(CSV35Key);
                            }
                        }
                        int[] CSV35LKey = CSV35KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_35, new object[] { CSV35LKey }));
                        break;

                    case 36:

                        var CSV36KeyList = new List<DateTime>();
                        DateTime CSV36Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("適用開始日付"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (DateTime.TryParse(Rows["適用開始日付"].ToString(), out CSV36Key))
                            {
                                CSV36KeyList.Add(CSV36Key);
                            }
                        }
                        DateTime[] CSV36LKey = CSV36KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020, new object[] { CSV36LKey }));
                        break;

                    case 37:

                        var CSV37KeyList = new List<int>();
                        int CSV37Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("歩合計算区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["歩合計算区分ID"].ToString(), out CSV37Key))
                            {
                                CSV37KeyList.Add(CSV37Key);
                            }
                        }
                        int[] CSV37LKey = CSV37KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_37, new object[] { CSV37LKey }));
                        break;

                    case 38:

                        var CSV38KeyList = new List<int>();
                        int CSV38Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("取引区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["取引区分ID"].ToString(), out CSV38Key))
                            {
                                CSV38KeyList.Add(CSV38Key);
                            }
                        }
                        int[] CSV38LKey = CSV38KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_38, new object[] { CSV38LKey }));
                        break;


                    case 39:

                        var CSV39KeyList = new List<int>();
                        int CSV39Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("出勤区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["出勤区分ID"].ToString(), out CSV39Key))
                            {
                                CSV39KeyList.Add(CSV39Key);
                            }
                        }
                        int[] CSV39LKey = CSV39KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_39, new object[] { CSV39LKey }));
                        break;

                    case 40:

                        var CSV40KeyList = new List<int>();
                        int CSV40Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("税区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["税区分ID"].ToString(), out CSV40Key))
                            {
                                CSV40KeyList.Add(CSV40Key);
                            }
                        }
                        int[] CSV40LKey = CSV40KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_40, new object[] { CSV40LKey }));
                        break;

                    case 41:

                        var CSV41KeyList = new List<int>();
                        int CSV41Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("親子区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["親子区分ID"].ToString(), out CSV41Key))
                            {
                                CSV41KeyList.Add(CSV41Key);
                            }
                        }
                        int[] CSV41LKey = CSV41KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_41, new object[] { CSV41LKey }));
                        break;

                    case 42:

                        var CSV42KeyList = new List<int>();
                        int CSV42Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("請求書区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["請求書区分ID"].ToString(), out CSV42Key))
                            {
                                CSV42KeyList.Add(CSV42Key);
                            }
                        }
                        int[] CSV42LKey = CSV42KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_42, new object[] { CSV42LKey }));
                        break;

                    case 43:

                        var CSV43KeyList = new List<int>();
                        int CSV43Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("運賃計算区分ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["運賃計算区分ID"].ToString(), out CSV43Key))
                            {
                                CSV43KeyList.Add(CSV43Key);
                            }
                        }
                        int[] CSV43LKey = CSV43KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_43, new object[] { CSV43LKey }));
                        break;

                    case 44:

                        var CSV44KeyList = new List<int>();
                        int CSV44Key;
                        //if (取込データ.Columns.Count != 6 || !取込データ.Columns.Contains("運輸局ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["運輸局ID"].ToString(), out CSV44Key))
                            {
                                CSV44KeyList.Add(CSV44Key);
                            }
                        }
                        int[] CSV44LKey = CSV44KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_44, new object[] { CSV44LKey }));
                        break;

                    case 45:

                        var CSV45KeyList = new List<int>();
                        int CSV45Key;
                        //if (取込データ.Columns.Count != 42 || !取込データ.Columns.Contains("管理ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["管理ID"].ToString(), out CSV45Key))
                            {
                                CSV45KeyList.Add(CSV45Key);
                            }
                        }
                        int[] CSV45LKey = CSV45KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_45, new object[] { CSV45LKey }));
                        break;

                    case 46:

                        var CSV46KeyList = new List<int>();
                        int CSV46Key;
                        //if (取込データ.Columns.Count != 6 || !取込データ.Columns.Contains("明細番号ID"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (int.TryParse(Rows["明細番号ID"].ToString(), out CSV46Key))
                            {
                                CSV46KeyList.Add(CSV46Key);
                            }
                        }
                        int[] CSV46LKey = CSV46KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_46, new object[] { CSV46LKey }));
                        break;

                    case 47:
                        //if (取込データ.Columns.Count != 7)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_47, new object[] { }));
                        break;

                    case 48:
                        //if (取込データ.Columns.Count != 6)
                        //{
                        //    throw new CSVException();
                        //}
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_48, new object[] { }));
                        break;

                    case 49:

                        var CSV49KeyList = new List<DateTime>();
                        DateTime CSV49Key;
                        //if (取込データ.Columns.Count != 5 || !取込データ.Columns.Contains("適用開始年月日"))
                        //{
                        //    throw new CSVException();
                        //}
                        foreach (DataRow Rows in 取込データ.Rows)
                        {
                            if (DateTime.TryParse(Rows["適用開始年月日"].ToString(), out CSV49Key))
                            {
                                CSV49KeyList.Add(CSV49Key);
                            }
                        }
                        DateTime[] CSV49LKey = CSV49KeyList.ToArray();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST90020_49, new object[] { CSV49LKey }));
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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST900201, new object[] { ds, (int)TableName.SelectedValue, 0 }));
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
			取込設定 = null;
			CsvData.ItemsSource = null;


            this.Cursor = Cursors.Arrow;
            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigMST90090(); }
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
            SetData();
            // 20150728 wada commentout
            //SetData();
        }
        #endregion


        private void CellBeginEdit(object sender, SpreadCellBeginEditEventArgs e)
        {
            // 20150729 wada add 取込データがない場合は抜ける。
            if (取込データ == null)
            {
                return;
            }

            int col = CsvData.ActiveColumnIndex;
            int row = CsvData.ActiveRowIndex;
            CsvData[row,col].Validator = null;
        }

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
                        string msg = "主キーが重複しています。";
                        CsvData.Rows[取込データ.Rows.IndexOf(dr)].ValidationErrors.Add(new SpreadValidationError(msg, null));
                        break;
                    }
                    subKey.Clear();
                }
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
                case 4:
                    frmcfg.取込保存5 = 取込設定;
                    break;
                case 5:
                    frmcfg.取込保存6 = 取込設定;
                    break;
                case 6:
                    frmcfg.取込保存7 = 取込設定;
                    break;
                case 7:
                    frmcfg.取込保存8 = 取込設定;
                    break;
                case 8:
                    frmcfg.取込保存9 = 取込設定;
                    break;
                case 9:
                    frmcfg.取込保存10 = 取込設定;
                    break;
                case 10:
                    frmcfg.取込保存11 = 取込設定;
                    break;
                case 11:
                    frmcfg.取込保存12 = 取込設定;
                    break;
                case 12:
                    frmcfg.取込保存13 = 取込設定;
                    break;
                case 13:
                    frmcfg.取込保存14 = 取込設定;
                    break;
                case 14:
                    frmcfg.取込保存15 = 取込設定;
                    break;
                case 15:
                    frmcfg.取込保存16 = 取込設定;
                    break;
                case 16:
                    frmcfg.取込保存17 = 取込設定;
                    break;
                case 17:
                    frmcfg.取込保存18 = 取込設定;
                    break;
                case 18:
                    frmcfg.取込保存19 = 取込設定;
                    break;
                case 19:
                    frmcfg.取込保存20 = 取込設定;
                    break;
                case 20:
                    frmcfg.取込保存21 = 取込設定;
                    break;
                case 21:
                    frmcfg.取込保存22 = 取込設定;
                    break;
                case 22:
                    frmcfg.取込保存23 = 取込設定;
                    break;
                case 23:
                    frmcfg.取込保存24 = 取込設定;
                    break;
                case 24:
                    frmcfg.取込保存25 = 取込設定;
                    break;
                case 25:
                    frmcfg.取込保存26 = 取込設定;
                    break;
                case 26:
                    frmcfg.取込保存27 = 取込設定;
                    break;
                case 27:
                    frmcfg.取込保存28 = 取込設定;
                    break;
                case 28:
                    frmcfg.取込保存29 = 取込設定;
                    break;
                case 29:
                    frmcfg.取込保存30 = 取込設定;
                    break;
                case 30:
                    frmcfg.取込保存31 = 取込設定;
                    break;
                case 31:
                    frmcfg.取込保存32 = 取込設定;
                    break;
                case 32:
                    frmcfg.取込保存33 = 取込設定;
                    break;
                case 33:
                    frmcfg.取込保存34 = 取込設定;
                    break;
                case 34:
                    frmcfg.取込保存35 = 取込設定;
                    break;
                case 35:
                    frmcfg.取込保存36 = 取込設定;
                    break;
                case 36:
                    frmcfg.取込保存37 = 取込設定;
                    break;
                case 37:
                    frmcfg.取込保存38 = 取込設定;
                    break;
                case 38:
                    frmcfg.取込保存39 = 取込設定;
                    break;
                case 39:
                    frmcfg.取込保存40 = 取込設定;
                    break;
                case 40:
                    frmcfg.取込保存41 = 取込設定;
                    break;
                case 41:
                    frmcfg.取込保存42 = 取込設定;
                    break;
                case 42:
                    frmcfg.取込保存43 = 取込設定;
                    break;
                case 43:
                    frmcfg.取込保存44 = 取込設定;
                    break;
                case 44:
                    frmcfg.取込保存45 = 取込設定;
                    break;
                case 45:
                    frmcfg.取込保存46 = 取込設定;
                    break;
                case 46:
                    frmcfg.取込保存47 = 取込設定;
                    break;
                case 47:
                    frmcfg.取込保存48 = 取込設定;
                    break;
                case 48:
                    frmcfg.取込保存49 = 取込設定;
                    break;
                case 49:
                    frmcfg.取込保存50 = 取込設定;
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
            if (取込設定 != null && 取込設定.Count > 0 && tabControl.SelectedIndex != 0)
            {
                Yomikomi();
            }
        }







    }
}