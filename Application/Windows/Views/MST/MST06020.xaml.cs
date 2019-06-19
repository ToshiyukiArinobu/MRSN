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
    /// 車輌マスタ問合せ
	/// </summary>
	public partial class MST06020 : WindowMasterMainteBase
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
        public class ConfigMST06020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST06020 frmcfg = null;

        #endregion

        #region 定数
        //検索データ取得用
        private const string TargetTableNm = "M05_CAR_ICHIRAN";
        //CSV出力用
        private const string SEARCH_MST06010_CSV = "SEARCH_MST06010_CSV";
        //プレビュー出力用
        private const string SEARCH_MST06010 = "SEARCH_MST06010";
        //台帳出力用
        private const string SEARCH_DLY17010 = "SEARCH_DLY17010";
        //プレビュー用
        private const string rptFullPathName_PIC = @"Files\MST\MST06010.rpt";
        //台帳用
        private const string rptFullPathName_PIC2 = @"Files\MST\MST06020.rpt";
        #endregion

        #region バインド用プロパティ
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

        private string _車輌指定 = string.Empty;
        public string 車輌指定
		{
            get { return this._車輌指定; }
            set { this._車輌指定 = value; NotifyPropertyChanged(); }
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
        #endregion

        #region MST06020
        /// <summary>
		/// 車輌マスタ問合せ
		/// </summary>
        public MST06020()
		{
			InitializeComponent();
			this.DataContext = this;
		}
        #endregion

        #region LOADイベント
        /// <summary>
		/// Loadイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST06020)ucfg.GetConfigValue(typeof(ConfigMST06020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST06020();
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

        //検索画面追加
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });
            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { null, typeof(SCH10010) });
		}
        #endregion

        #region 一覧データ取得の実行メソッド
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
        #endregion

        #region 一覧データ取得メソッド
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

                if (!int.TryParse(車輌IDFROM, out syaryouFrom))
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


                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { syaryouFrom, syaryouTo, bumonFrom, bumonTo, 廃車区分 }));

            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region データ受信メソッド
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                //検索用
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

                //プレビュー用
                case SEARCH_MST06010:
                    DispPreviw(tbl);
                    break;

                //CSV出力用
                case SEARCH_MST06010_CSV:
                    OutPutCSV(tbl);
                    break;

                case SEARCH_DLY17010:
                    DispPreviw(tbl);
                    break;


                default:
                    break;

            }
            //this.MSTData = null;
        }
        #endregion

        #region エラー受信メソッド
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region プレビュー画面表示
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("車輌マスタ一覧表", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
				view.SetReportData(tbl);
				view.PrinterName = frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;

                // 印刷した場合
                if (view.IsPrinted)
                {
                    //印刷した場合はtrueを返す
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// F1 リボン　検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                        return;
                    }
                    SCH06010 srch = new SCH06010();
                    switch (uctext.DataAccessName)
                    {
                        case "M09_HIN":
                            srch.MultiSelect = false;
                            break;
                        default:
                            srch.MultiSelect = true;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
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
        /// F5　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            //Todo: 条件渡し
            try
            {
                int[] i車輌List = new int[0];
                if (!string.IsNullOrEmpty(車輌指定))
                {
                    string[] 車輌List = 車輌指定.Split(',');
                    i車輌List = new int[車輌List.Length];

                    for (int i = 0; i < 車輌List.Length; i++)
                    {
                        string str = 車輌List[i];
                        int code;
                        if (!int.TryParse(str, out code))
                        {
                            this.ErrorMessage = "車輌指定の形式が不正です。";
                            return;
                        }
                        i車輌List[i] = code;
                    }
                }

                base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST06010_CSV, new object[] { 車輌IDFROM, 車輌IDTO, 自社部門IDFROM, 自社部門IDTO, 廃車区分, i車輌List }));
            }
            catch (Exception)
            {
                MessageBox.Show("印刷できませんでした。");
            }
        }

        /// <summary>
        /// F7 リボン　台帳印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF7Key(object sender, KeyEventArgs e)
        {
            base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_DLY17010, new object[] { 車輌IDFROM, 車輌IDTO }));

        }

        #region SetMstData()
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
        #endregion

        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;

            //Todo: 条件渡し
            try
            {
                int[] i車輌List = new int[0];
                if (!string.IsNullOrEmpty(車輌指定))
                {
                    string[] 車輌List = 車輌指定.Split(',');
                    i車輌List = new int[車輌List.Length];

                    for (int i = 0; i < 車輌List.Length; i++)
                    {
                        string str = 車輌List[i];
                        int code;
                        if (!int.TryParse(str, out code))
                        {
                            this.ErrorMessage = "車輌指定の形式が不正です。";
                            return;
                        }
                        i車輌List[i] = code;
                    }
                }

                base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST06010, new object[] { 車輌IDFROM, 車輌IDTO, 自社部門IDFROM, 自社部門IDTO, 廃車区分, i車輌List }));
            }
            catch (Exception)
            {
                MessageBox.Show("印刷できませんでした。");
            }
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

        #endregion

        #region 画面設定項目データ保持
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

	}
}
