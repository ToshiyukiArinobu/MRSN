using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Application.Windows.Views.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 仕入入力フォームクラス
    /// </summary>
    public partial class DLY01010 : WindowReportBase
    {
        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            伝票番号 = 0,
            品番コード = 1,
            自社品番 = 2,
            自社品名 = 3,
            賞味期限 = 4,
            数量 = 5,
            単位 = 6,
            単価 = 7,
            d金額 = 8,
            税区分 = 9,                        // No-94 Add
            摘要 = 10,
            消費税区分 = 11,
            商品分類 = 12,

            // 20190530CB-S
            色コード = 13,
            色名称 = 14,
            // 20190530CB-E

            金額 = 15,
        }

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        /// <summary>
        /// 商品分類 商品分類
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        /// <summary>
        /// 消費税区分
        /// </summary>
        private enum 消費税区分 : int
        {
            通常税率 = 0,
            軽減税率 = 1,
            非課税 = 2
        }

        /// <summary>
        /// 取引区分
        /// </summary>
        private enum 取引区分 : int
        {
            得意先 = 0,
            仕入先 = 1,
            相殺 = 3
        }

        /// <summary>
        /// 確定区分
        /// </summary>
        private enum 確定区分  :int
        {
            請求 = 0,
            支払 = 1
        }

        #endregion

        #region 定数定義

        #region サービスアクセス定義
        /// <summary>仕入情報検索</summary>
        private const string T03_GetData = "T03_GetData";
        /// <summary>仕入情報更新</summary>
        private const string T03_Update = "T03_Update";
        /// <summary>仕入情報削除</summary>
        private const string T03_Delete = "T03_Delete";
        /// <summary>取引先名称取得</summary>
        private const string MasterCode_Supplier = "UcSupplier";
        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProduct = "UcMyProduct";
        /// <summary>確定済チェック</summary>
        private const string FixCheck = "TKS90010_CheckFix";

        #endregion

        #region 使用テーブル名定義
        private const string HEADER_TABLE_NAME = "T03_SRHD";
        private const string DETAIL_TABLE_NAME = "T03_SRDTL";
        private const string ZEI_TABLE_NAME = "M73_ZEI";
        private const string FIX_TABLE_NAME = "S11_KAKUTEI";
        #endregion

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";
        /// <summary>グリッドの最大行数</summary>
        private const int GRID_MAX_ROW_COUNT = 10;

        #endregion

        #region 権限関係
        public UserConfig ucfg = null;
        CommonConfig ccfg = null;
        public class ConfigDLY01010 : FormConfigBase
        {
        }

        public ConfigDLY01010 frmcfg = null;
        #endregion

        #region バインディングデータ

        /// <summary>
        /// ヘッダバインディングデータ行
        /// </summary>
        public DataRow _SearchHeader;
        public DataRow SearchHeader
        {
            get { return _SearchHeader; }
            set
            {
                _SearchHeader = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 明細バインディングデータテーブル
        /// </summary>
        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return _SearchResult; }
            set
            {
                this._SearchResult = value;
                NotifyPropertyChanged();
            }
        }
        //▼課題No299 Add Start 2019/12/20
        private int _通常税率消費税;
        public int 通常税率消費税
        {
            get { return _通常税率消費税; }
            set { _通常税率消費税 = value; NotifyPropertyChanged(); }
        }

        private int _軽減税率消費税;
        public int 軽減税率消費税
        {
            get { return _軽減税率消費税; }
            set { _軽減税率消費税 = value; NotifyPropertyChanged(); }
        }
        //▲課題No299 Add End 2019/12/20
        // No-58 Strat
        // 削除済みレコード情報
        public DataTable SearchDeleteDetail;
        // No-58 End

        private string[] _仕入先LinkItem = new []{ "1,3", "0", "",""};
        public string[] 仕入先LinkItem
        {
            get { return this._仕入先LinkItem; }
            set { this._仕入先LinkItem = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridCtl;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

        /// <summary>
        /// 編集中の行番号
        /// </summary>
        private int _編集行;

        /// <summary>
        /// 確定情報
        /// </summary>
        DataTable FixData;

        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 仕入入力 コンストラクタ
        /// </summary>
        public DLY01010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// 画面が表示された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 画面初期設定(権限設定等)
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY01010)ucfg.GetConfigValue(typeof(ConfigDLY01010));

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY01010();
                // 画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Top;
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            AppCommon.SetutpComboboxList(this.c仕入区分, false);
            gridCtl = new GcSpreadGridController(SearchGrid);

            ScreenClear();
            ChangeKeyItemChangeable(true);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.c会社名.Text1 = ccfg.自社コード.ToString();
            this.c会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);
            this.c伝票番号.Focus();

        }

        #endregion

        #region << データ受信 >>
        /// <summary>
        /// 取得データの取り込み
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
                    case T03_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();
                            
                            // No.162-1 Add Start
                            bool blnEnabled = true;
                            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                            {
                                blnEnabled = false;
                            }
                            // 入力制御
                            setDispHeaderEnabled(blnEnabled);
                            // No.162-1 Add End

                            // 確定モード画面制御
                            setFixDisplay(this.MaintenanceMode);
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.c伝票番号.Focus();
                        }
                        break;

                    case T03_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T03_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case FixCheck:
                        // 確定済チェック結果受信
                        if (IsFixCheck(tbl))
                        {
                            // 確定済エラー
                            MessageBox.Show("すでに確定済の仕入先です。登録・編集できません。", "確定済仕入先", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }

                        if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                        return;
                        
                        Update();
                        break;
                    case MasterCode_Supplier:
                        // 仕入先名称取得
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            this.c仕入先名.Content = tbl.Rows[0]["名称"].ToString();
                            SearchHeader["Ｓ支払消費税区分"] = tbl.Rows[0]["Ｓ支払消費税区分"];
                            SearchHeader["Ｓ税区分ID"] = tbl.Rows[0]["Ｓ税区分ID"];
                        }
                        // 編集中に売上日変更された場合の仕入先名称
                        else
                        {
                            this.c仕入先名.Content = string.Empty;
                            SearchHeader["Ｓ支払消費税区分"] = 1;   // 1:一括
                            SearchHeader["Ｓ税区分ID"] = 9; // 9:税なし
                        }
                        //▼課題No299 Del Start 2019/12/20
                        //summaryCalculation();
                        //▲課題No299 Del End 2019/12/20

                        break;

                    case MasterCode_MyProduct:
                        #region 自社品番手入力時
                        DataTable ctbl = data as DataTable;

                        int columnIdx = gridCtl.ActiveColumnIndex;
                        int rIdx = gridCtl.ActiveRowIndex;

                        // フォーカス移動後の項目が異なる場合または編集行が異なる場合は処理しない。
                        if ((columnIdx != (int)GridColumnsMapping.自社品名) || _編集行 != rIdx) return;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            gridCtl.SetCellValue((int)GridColumnsMapping.伝票番号, this.c伝票番号.Text);
                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.金額, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, string.Empty);      // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, 0);    // [軽減税率対象]0:通常税率
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, 3);      // [商品分類]3:その他
                            // 20190530CB-S

                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, string.Empty);

                            // 20190530CB-E

                        }
                        else if (ctbl.Rows.Count > 1)
                        {
                            var colVal = gridCtl.GetCellValueToString((int)GridColumnsMapping.自社品番);

                            // 自社品番の場合
                            if (string.IsNullOrEmpty(this.c仕入先.Text1) || string.IsNullOrEmpty(this.c仕入先.Text2))
                            {
                                MessageBox.Show("仕入先が設定されていません。", "仕入先未設定", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                c仕入先.SetFocus();
                                return;
                            }

                            int code = int.Parse(this.c仕入先.Text1);
                            int eda = int.Parse(this.c仕入先.Text2);

                            SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.txtCode.Text = colVal;
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 1;

                            if (myhin.ShowDialog(this) == true)
                            {
                                gridCtl.SetCellValue((int)GridColumnsMapping.伝票番号, this.c伝票番号.Text);
                                gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                                gridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.単価, myhin.TwinTextBox.Text3);
                                gridCtl.SetCellValue((int)GridColumnsMapping.d金額, string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ?
                                                                                        0 : AppCommon.DecimalParse(myhin.TwinTextBox.Text3));
                                gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(myhin.SelectedRowData["消費税区分"]));      // No-94 Add
                                gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, myhin.SelectedRowData["消費税区分"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);

                                // 20190530CB-S
                                gridCtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                                // 20190530CB-E

                                // 自社品番のセルをロック
                                gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                                gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);       // No-94 Add

                                // 集計計算をおこなう
                                summaryCalculation();

                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            SearchGrid.BeginEdit();
                            gridCtl.SetCellValue((int)GridColumnsMapping.伝票番号, this.c伝票番号.Text);
                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, drow["品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, drow["自社品番"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, drow["自社品名"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, drow["単位"]);
                            //gridCtl.SetCellValue((int)GridColumnsMapping.単価, drow["原価"] == DBNull.Value ? 0 : Convert.ToInt32(drow["原価"]));
                            //gridCtl.SetCellValue((int)GridColumnsMapping.金額, drow["原価"] == DBNull.Value ? 0 : Convert.ToInt32(drow["原価"]));
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, drow["原価"] == DBNull.Value ? 0 : Convert.ToDecimal(drow["原価"]));
                            //gridCtl.SetCellValue((int)GridColumnsMapping.金額, drow["原価"] == DBNull.Value ? 0 : Convert.ToInt32(drow["原価"]));
                            gridCtl.SetCellValue((int)GridColumnsMapping.d金額, AppCommon.DecimalParse(drow["原価"].ToString()));

                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(drow["消費税区分"]));        // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, drow["消費税区分"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);

                            // 20190530CB-S
                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, drow["自社色"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, drow["自社色名"]);
                            // 20190530CB-E

                            SearchGrid.CommitCellEdit();
                            // 自社品番のセルをロック
                            // 数量以外はロック
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);

                            // 20190704CB-S
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品名, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.単位, true);
                            //gridCtl.SetCellLocked((int)GridColumnsMapping.単価, true);                // No90 Mod
                            gridCtl.SetCellLocked((int)GridColumnsMapping.金額, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.消費税区分, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.商品分類, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.色コード, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.色名称, true);
                            // 20190704CB-E
                            gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);               // No-94 Add




                            summaryCalculation();

                        }

                        SearchResult.Rows[gridCtl.ActiveRowIndex].EndEdit();

                        #endregion

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

        /// <summary>
        /// データエラー受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
            DebugLog.WriteLine("=================================");
            DebugLog.WriteLine(message.GetParameters().GetValue(0));
            DebugLog.WriteLine("=================================");
        }

        #endregion

        #region << 処理ロジック群 >>

        #region 取得内容を各コントロールに設定
        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 画面表示モードを設定
            SetDispMode(ds);

            // 仕入ヘッダ情報設定
            DataTable tblHd = ds.Tables[HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 仕入詳細情報設定
            DataTable tblDtl = ds.Tables[DETAIL_TABLE_NAME];
            SearchResult = tblDtl;

            SearchResult.Columns.Add("d金額", Type.GetType("System.Decimal"));

            foreach (DataRow dr in SearchResult.Rows)
            {
                dr["d金額"] = dr["金額"];
            }

            SearchResult.AcceptChanges();

            // No-58 Add Strat
            // 仕入明細情報（削除）設定
            SearchDeleteDetail = SearchResult.Clone();
            // No-58 Add End

            // 消費税情報保持
            taxCalc = new TaxCalculator(ds.Tables[ZEI_TABLE_NAME]);

            // データ状態から編集状態を設定
            if (tblDtl.Select("品番コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchResult.NewRow();
                    row["伝票番号"] = AppCommon.IntParse(tblHd.Rows[0]["伝票番号"].ToString());
                    row["行番号"] = (i + 1);

                    SearchResult.Rows.Add(row);

                }

                this.c仕入先.Text1 = string.Empty;
                this.c仕入先.Text2 = string.Empty;
                this.c入荷先.Text1 = string.Empty;
                this.c仕入日.Focus();

            }
            else
            {
                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in SearchGrid.Rows)
                {
                    row.Cells[(int)GridColumnsMapping.自社品番].Locked = true;
                    row.Cells[(int)GridColumnsMapping.税区分].Locked = true;
                    
                    //intの金額をdecimalの金額に代入する
                    row.Cells[(int)GridColumnsMapping.d金額].Value = row.Cells[(int)GridColumnsMapping.金額].Value;
                }
                gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);

            }

            //▼課題No299 Mod Start 2019/12/20
            // グリッド内容の再計算を実施
            //summaryCalculation();
            // ヘッダの情報を設定
            this.c消費税.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["消費税"].ToString()));
            this.lbl通常税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["通常税率対象金額"].ToString()));
            this.lbl軽減税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["軽減税率対象金額"].ToString()));
            this.txb通常税率消費税.Text = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["通常税率消費税"].ToString()));
            this.txb軽減税率消費税.Text = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["軽減税率消費税"].ToString()));
            this.c小計.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["小計"].ToString()));
            this.c総合計.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["総合計"].ToString()));
            //▲課題No299 Mod End 2019/12/20

        }
        #endregion

        #region キー項目としてマークされた項目の入力可否を切り替える
        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            gridCtl.SetEnabled(!flag);
            this.c仕入日.IsEnabled = !flag;
            base.ChangeKeyItemChangeable(flag);

        }
        #endregion

        #region 画面ヘッダ部の入力制御
        // No.162-1 Add Start
        /// <summary>
        /// 画面ヘッダ部の入力設定を行う
        /// </summary>
        /// <param name="blnEnabled">true:入力可、false:入力不可</param>
        private void setDispHeaderEnabled(bool blnEnabled)
        {
            c入荷先.IsEnabled = blnEnabled;
            c仕入区分.IsEnabled = blnEnabled;
        }
        // No.162-1 Add End
        #endregion

        #region 確定モード時の画面制御
        /// <summary>
        /// 確定モード時の画面制御
        /// </summary>
        /// <param name="Mode">画面モード</param>
        private void setFixDisplay(string Mode)
        {
            bool blnEnabled = Mode == AppConst.MAINTENANCEMODE_FIX ? true : false;

            // リボンボタン表示制御
            this.F1.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F2.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F5.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F6.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F9.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F12.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;

            // ヘッダー項目
            this.c会社名.IsEnabled = !blnEnabled;
            this.c伝票番号.IsEnabled = !blnEnabled;
            this.c仕入日.IsEnabled = !blnEnabled;
            this.c仕入先.IsEnabled = !blnEnabled;
            this.c仕入先名.IsEnabled = !blnEnabled;
            this.c発注番号.IsEnabled = !blnEnabled;
            this.c入荷先.IsEnabled = Mode == AppConst.MAINTENANCEMODE_ADD ? true : false;
            this.c仕入区分.IsEnabled = Mode == AppConst.MAINTENANCEMODE_ADD ? true : false;

            // 明細
            this.SearchGrid.IsEnabled = !blnEnabled;
        }
        #endregion

        #region 画面項目の初期化をおこなう
        /// <summary>
        /// 画面項目の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            if (SearchHeader != null)
                SearchHeader = null;
            if (SearchResult != null)
            {
                SearchResult.Clear();
                for (int i = 0; i < GRID_MAX_ROW_COUNT; i++)
                    SearchResult.Rows.Add(SearchResult.NewRow());

            }
            if (FixData != null)
                FixData = null;

            this.c備考.Text1 = string.Empty;

            string initValue = string.Format(PRICE_FORMAT_STRING, 0);
            // No-94 Add Start
            lbl通常税率対象金額.Content = initValue;
            lbl軽減税率対象金額.Content = initValue;
            //▼課題No299 Mod Start 2019/12/20
            txb通常税率消費税.Text = initValue;
            txb軽減税率消費税.Text = initValue;
            //▲課題No299 Mod End 2019/12/20
            // No-94 Add End
            this.c小計.Content = initValue;
            this.c消費税.Content = initValue;
            this.c総合計.Content = initValue;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();
            this.c伝票番号.Focus();

        }
        #endregion

        #region 画面表示モードの設定
        /// <summary>
        /// 画面表示モードの設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetDispMode(DataSet ds)
        {
            DataTable tblHd = ds.Tables[HEADER_TABLE_NAME];
            DataTable tblDtl = ds.Tables[DETAIL_TABLE_NAME];
            FixData = ds.Tables[FIX_TABLE_NAME];

            if (tblDtl.Rows.Count == 0)
            {
                // 新規モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                return;
            }

            int val = -1;
            int 仕入先コード = int.TryParse(tblHd.Rows[0]["仕入先コード"].ToString(), out val) ? val : -1;
            int 仕入先枝番 = int.TryParse(tblHd.Rows[0]["仕入先枝番"].ToString(), out val) ? val : -1;

            // 仕入先確定データ
            var shiData = FixData.AsEnumerable()
                            .Where(w => w.Field<int?>("取引先コード") == 仕入先コード &&
                                        w.Field<int?>("枝番") == 仕入先枝番);

            // 相殺確定データ
            var soData = FixData.AsEnumerable()
                            .Where(w => w.Field<int?>("取引区分") == (int)取引区分.相殺);

            DateTime? 仕入日 = tblHd.Rows[0].Field<DateTime?>("仕入日");
            DateTime wkDt;
            DateTime? shr確定日 = null;

            shr確定日 = DateTime.TryParse(shiData.Where(w => w.Field<int?>("確定区分") == (int)確定区分.支払).Select(s => s.Field<DateTime?>("確定日")).FirstOrDefault().ToString(), out wkDt) ?
                                wkDt : (DateTime?)null;

            if (soData.Any())
            {
                DLY03010 dly3010 = new DLY03010();
                shr確定日 = dly3010.getSousaiFixDay(shiData.ToList());
            }

            // 仕入先確定日が仕入日以降の場合、編集不可
            if (shr確定日 != null && shr確定日 >= 仕入日)
            {
                // 確定モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_FIX;
            }
            else
            {
                // 編集モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }
        }
        #endregion

        #region 確定済チェック
        /// <summary>
        /// 確定済チェック
        /// </summary>
        /// <param name="fixDt"></param>
        /// <returns>true:確定済/false:未確定</returns>
        private bool IsFixCheck(DataTable fixDt)
        {
            DateTime dt;
            DateTime? fixDay;       // 仕入先確定日
            DateTime? shrDay;       // 仕入日

            if (fixDt == null)
            {
                return false;
            }

            // 仕入先の確定データ
            var shrData = fixDt.AsEnumerable().Where(w => w.Field<string>("取引先コード") == this.c仕入先.Text1 &&
                                                        w.Field<string>("枝番") == this.c仕入先.Text2)
                                                        .OrderByDescending(o => o.Field<DateTime?>("確定日")).ToList();

            if (shrData.Any())
            {
                fixDay = DateTime.TryParse(shrData[0].Field<DateTime?>("確定日").ToString(), out dt) ? dt : (DateTime?)null;
                shrDay = DateTime.TryParse(this.c仕入日.Text, out dt) ? dt : (DateTime?)null;

                if (fixDay != null && shrDay != null && fixDay >= shrDay)
                {
                    // 確定済エラー
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 仕入情報の登録
        /// <summary>
        /// 仕入情報の登録
        /// </summary>
        private void Update()
        {
            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.c消費税.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-94 Add Start
            SearchHeader["通常税率対象金額"] = AppCommon.IntParse(this.lbl通常税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率対象金額"] = AppCommon.IntParse(this.lbl軽減税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            //▼課題No299 Mod Start 2019/12/20
            SearchHeader["通常税率消費税"] = AppCommon.IntParse(this.txb通常税率消費税.Text, System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率消費税"] = AppCommon.IntParse(this.txb軽減税率消費税.Text, System.Globalization.NumberStyles.Number);
            //▲課題No299 Mod End 2019/12/20
            // No-94 Add End
            // No-95 Add Start
            SearchHeader["小計"] = AppCommon.IntParse(this.c小計.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["総合計"] = AppCommon.IntParse(this.c総合計.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-95 Add End

            // No-58 Add Start
            // 仕入明細情報（削除）を仕入明細情報に追加する
            // (※Rows.AddだとRowStateがAddedに変更されるため1行ずつImportする)
            if (SearchDeleteDetail.Rows.Count != 0)
            {
                for (int intIdx = 0; intIdx < SearchDeleteDetail.Rows.Count; intIdx++)
                {
                    SearchResult.ImportRow(SearchDeleteDetail.Rows[intIdx]);
                }
            }
            // No-58 Add End

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T03_Update,
                    new object[] {
                        SearchResult.DataSet,
                        ccfg.ユーザID
                    }));
        }
        #endregion

        #endregion

        #region << リボン >>

        #region F1 マスタ参照
        /// <summary>
        /// F1 リボン　マスタ参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);

                if (spgrid == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                    // 仕入先の場合は個別に処理
                    // REMARKS:消費税関連の情報を取得する為
                    var twinText = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);
                    if (twinText != null && twinText.Name == this.c仕入先.Name)
                        SearchSupplier_cTextChanged(this.c仕入先, null);

                }
                else
                {
                    #region スプレッド内のイベント処理

                    if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.自社品番)
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (gridCtl.CellLocked == true)
                            return;

                        if (string.IsNullOrEmpty(this.c仕入先.Text1) || string.IsNullOrEmpty(this.c仕入先.Text2))
                        {
                            MessageBox.Show("仕入先が設定されていません。", "仕入先未設定", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            c仕入先.SetFocus();
                            return;
                        }

                        int code = int.Parse(this.c仕入先.Text1);
                        int eda = int.Parse(this.c仕入先.Text2);

                        // 自社品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.TwinTextBox.LinkItem = 1;
                        if (myhin.ShowDialog(this) == true)
                        {
                            //入力途中のセルを未編集状態に戻す
                            spgrid.CancelCellEdit();

                            gridCtl.SetCellValue((int)GridColumnsMapping.伝票番号, this.c伝票番号.Text);
                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, myhin.TwinTextBox.Text3);
                            gridCtl.SetCellValue((int)GridColumnsMapping.d金額, string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ?
                                                                                    0 : AppCommon.DecimalParse(myhin.TwinTextBox.Text3));
                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(myhin.SelectedRowData["消費税区分"]));       // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, myhin.SelectedRowData["消費税区分"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);

                            // 20190530CB-S
                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                            // 20195030CB-E

                            // 設定自社品番の編集を不可とする
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);       // No-94 Add

                            // 集計計算をおこなう
                            summaryCalculation();

                        }

                    }
                    else if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
                    {
                        // TODO:全角６文字を超える可能性アリ
                        SCHM11_TEK tek = new SCHM11_TEK();
                        tek.TwinTextBox = new UcLabelTwinTextBox();
                        if (tek.ShowDialog(this) == true)
                            gridCtl.SetCellValue(tek.TwinTextBox.Text2);

                    }

                    SearchResult.Rows[gridCtl.ActiveRowIndex].EndEdit();

                    #endregion

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F2 マスタ入力
        /// <summary>
        /// F2　リボン　マスタ入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);

                if (spgrid == null)
                    ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);

                else
                {
                    #region スプレッド内のイベント処理

                    if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.自社品番)
                    {
                        // 品番マスタ表示
                        MST02010 M09Form = new MST02010();
                        M09Form.Show(this);

                    }
                    else if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
                    {
                        // 摘要マスタ表示
                        MST08010 M11Form = new MST08010();
                        M11Form.Show(this);
                    }

                    #endregion

                }

            }
            catch (Exception ex)
            {
                appLog.Error("メンテ画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F5 行追加
        /// <summary>
        /// F5　リボン　行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            int delRowCount = (SearchResult.GetChanges(DataRowState.Deleted) == null) ? 0 : SearchResult.GetChanges(DataRowState.Deleted).Rows.Count;
            if (SearchResult.Rows.Count - delRowCount >= 10)
            {
                MessageBox.Show("明細行数が上限に達している為、これ以上追加できません。", "明細上限", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataRow dtlRow = SearchResult.NewRow();

            dtlRow["伝票番号"] = this.c伝票番号.Text;
            if (SearchResult.Rows.Count > 0)
            {
                dtlRow["行番号"] = (int)SearchResult.Rows[SearchResult.Rows.Count - 1]["行番号"] + 1;
            }
            else
            {
                dtlRow["行番号"] = 1;
            }

            SearchResult.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            // TODO:追加行が表示されるようにしたかったが追加行の上行までしか移動できない...
            // No-58 Mod Start
            //int newRowIdx = SearchResult.Rows.Count - delRowCount - 1;
            int newRowIdx = SearchResult.Rows.Count - 1;
            // No-58 Mod End

            gridCtl.ScrollShowCell(newRowIdx, (int)GridColumnsMapping.自社品名);

        }
        #endregion

        #region F6 行削除
        /// <summary>
        /// F6　リボン　行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            // No-106 Add Start
            if (gridCtl.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }
            // No-106 Add End

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            // No-58 Mod Start
            int intDelRowIdx = gridCtl.ActiveRowIndex;                              // 削除行Index

            // 選択行の削除
            // Spreadより該当行を削除する
            try
            {
                gridCtl.SpreadGrid.Rows.Remove(intDelRowIdx);
            }
            catch
            {
                // 削除処理をイベント不要のRemoveに変更する
                //SearchResult.Rows[intDelRowIdx].Delete();
                SearchResult.Rows.Remove(SearchResult.Rows[intDelRowIdx]);
            }

            // 追加行の判定（登録済みレコードの場合）
            if (SearchResult.Rows.Count > intDelRowIdx && SearchResult.Rows[intDelRowIdx].RowState != DataRowState.Added)
            {
                // 削除行を仕入明細情報（削除）(SearchDeleteDetail)に格納する
                SearchDeleteDetail.ImportRow(SearchResult.Rows[intDelRowIdx]);
            }

            // SearchDetailより該当行を削除する
            try
            {
                if (gridCtl.SpreadGrid.Rows.Count != SearchResult.Rows.Count)
                {
                    SearchResult.Rows.Remove(SearchResult.Rows[intDelRowIdx]);
                }
            }
            catch
            {
                // エラー処理なし
            }
            // No-58 Mod End
            
            // グリッド内容の再計算を実施
            summaryCalculation();

        }
        #endregion

        #region F9 登録
        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {


            foreach (var row in SearchGrid.Rows)
            {
                row.Cells[(int)GridColumnsMapping.自社品番].Locked = true;
                row.Cells[(int)GridColumnsMapping.税区分].Locked = true;

                if (row.Cells[(int)GridColumnsMapping.d金額].Value != null)
                {
                    //Decimalの金額をintの金額に代入する
                    row.Cells[(int)GridColumnsMapping.金額].Value = Decimal.ToInt32((Decimal)row.Cells[(int)GridColumnsMapping.d金額].Value);
                }
            }
            SearchGrid.CommitCellEdit();          // No-173 Add
            
            if (this.MaintenanceMode == null || SearchResult == null)
                return;

            // 業務入力チェックをおこなう
            if (!isFormValidation())
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.c伝票番号.Focus();
                return;
            }

            // 確定データチェック
            base.SendRequest(
               new CommunicationObject(
                   MessageType.RequestData,
                   FixCheck,
                   new object[] {
                            this.c会社名.Text1,
                            null,
                            null,
                            this.c仕入先.Text1,
                            this.c仕入先.Text2,
                        }));
        }
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F10　リボン　入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

        }
        #endregion

        #region F11 終了
        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                this.Close();

            else
            {
                if (DataUpdateVisible != Visibility.Hidden)
                {
                    var yesno = MessageBox.Show(
                            "編集中の伝票を保存せずに終了してもよろしいですか？",
                            "終了確認",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question,
                            MessageBoxResult.No);
                    if (yesno == MessageBoxResult.No)
                        return;

                }

                this.Close();

            }

        }
        #endregion

        #region F12 削除
        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            var yesno =
                MessageBox.Show("伝票を削除してもよろしいですか？",
                                "取消確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            // 削除処理実行
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T03_Delete,
                    new object[] {
                        this.c伝票番号.Text,
                        ccfg.ユーザID
                    }));

        }
        #endregion

        #endregion

        #region 入力検証処理

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.c会社名.Text1))
            {
                base.ErrorMessage = "会社名が入力されていません。";
                return isResult;
            }

            return isResult = true;

        }

        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isFormValidation()
        {
            bool isResult = false;
            // 【ヘッダ】必須入力チェック
            // 仕入日
            if (string.IsNullOrEmpty(this.c仕入日.Text))
            {
                this.c仕入日.Focus();
                base.ErrorMessage = string.Format("仕入日が入力されていません。");
                return isResult;

            }

            // 仕入区分
            if (this.c仕入区分.SelectedValue == null)
            {
                this.c仕入区分.Focus();
                base.ErrorMessage = string.Format("仕入区分が選択されていません。");
                return isResult;

            }

            // 仕入先
            if (string.IsNullOrEmpty(this.c仕入先.Text1) || string.IsNullOrEmpty(this.c仕入先.Text2))
            {
                this.c仕入先.Focus();
                base.ErrorMessage = string.Format("仕入先が入力されていません。");
                return isResult;

            }
            else if (string.IsNullOrEmpty(c仕入先名.Content.ToString()))
            {
                this.c仕入先.Focus();
                base.ErrorMessage = string.Format("仕入先がマスタに存在していないデータが入力されています。");
                return isResult;
            }

            // 入荷先
            if (string.IsNullOrEmpty(this.c入荷先.Text1))
            {
                this.c入荷先.Focus();
                base.ErrorMessage = string.Format("入荷先が入力されていません。");
                return isResult;

            }

            // 現在の明細行を取得
            var CurrentDetail = SearchResult.Select("", "", DataViewRowState.CurrentRows).AsEnumerable();

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchResult == null || CurrentDetail.Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                gridCtl.SpreadGrid.Focus();
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                return isResult;
            }

            // 【明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            int rIdx = 0;
            bool isDetailErr = false;
            foreach (DataRow row in SearchResult.Rows)
            {
                // 削除行は検証対象外
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // 追加行未入力レコードはスキップ
                if (row["品番コード"] == null || string.IsNullOrEmpty(row["品番コード"].ToString()) || row["品番コード"].ToString().Equals("0"))
                {
                    rIdx++;
                    continue;
                }

                // エラー情報をクリア
                gridCtl.ClearValidationErrors(rIdx);

                if (string.IsNullOrEmpty(row["数量"].ToString()))
                {
                    gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.数量, "数量が入力されていません。");
                    if (!isDetailErr)
                        gridCtl.SetCellFocus(rIdx, (int)GridColumnsMapping.数量);

                    isDetailErr = true;
                }

                if (string.IsNullOrEmpty(row["単価"].ToString()))
                {
                    gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.単価, "単価が入力されていません。");
                    if (!isDetailErr)
                        gridCtl.SetCellFocus(rIdx, (int)GridColumnsMapping.単価);

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」の場合はエラー
                    if (type.Equals((int)商品分類.食品))
                    {
                        gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "商品分類が『食品』の為、賞味期限の設定が必要です。");
                        isDetailErr = true;
                    }

                }

                rIdx++;

            }

            if (isDetailErr)
                return isResult;

            isResult = true;

            return isResult;

        }

        #endregion

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            // 画面が閉じられた時、データを保持する

        }

        #endregion

        #region << コントロールイベント >>

        #region 伝票番号でキーが押された時のイベント処理
        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // 検索項目検証
                if (!isKeyItemValidation())
                {
                    this.c伝票番号.Focus();
                    return;
                }

                // 全項目エラーチェック
                if (!base.CheckKeyItemValidation())
                {
                    this.c伝票番号.Focus();
                    return;
                }

                // 入力伝票番号で検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T03_GetData,
                        new object[] {
                            this.c会社名.Text1,
                            this.c伝票番号.Text,
                            ccfg.ユーザID
                    }));

            }

        }
        #endregion

        #region 発注番号でキーが押された時のイベント処理
        /// <summary>
        /// 発注番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c発注番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (SearchResult != null)
            {
                this.SearchGrid.ScrollCommands.ScrollToTop.Execute(SearchGrid);
                this.SearchGrid.ActiveCellPosition = new CellPosition(0, "自社品番");
                return;
            }
        }
        #endregion

        #region 仕入日変更時のイベント処理
        // No.175-2 Add Start
        /// <summary>
        /// 仕入日変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c仕入日_cTextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c仕入日.Text))
            {
               if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_FIX)
                {
                    string[] item = { "1,3", "0" };
                    仕入先LinkItem = item;
                }
                else
                {
                    string[] item = { "1,3", "0", this.c仕入日.Text, c会社名.Text1 };
                    仕入先LinkItem = item;
                }
                // 仕入先コードを再設定
                SearchSupplier_cTextChanged(sender, e);
            }
            //▼課題No299 Del Start 2019/12/20
            // グリッド内容の再計算を実施
            //summaryCalculation();
            //▲課題No299 Del End 2019/12/20
        }
        // No.175-2 Add End
        #endregion

        #region 仕入先コード変更時のイベント処理
        /// <summary>
        /// 仕入先コード変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSupplier_cTextChanged(object sender, RoutedEventArgs e)
        {
            // どちらかが入力されていない場合は処理しない
            if (string.IsNullOrEmpty(this.c仕入先.Text1) || string.IsNullOrEmpty(this.c仕入先.Text2))
            {
                this.c仕入先名.Content = string.Empty;
                return;
            }

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_Supplier,
                    new object[] {
                            this.c仕入先.DataAccessName,
                            this.c仕入先.Text1,
                            this.c仕入先.Text2,
                            this.c仕入先.LinkItem
                        }));

        }
        #endregion

        #region 仕入先ロストフォーカス時
        //▼課題No299 Add Start 2019/12/20
        /// <summary>
        /// 仕入先ロストフォーカス時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c仕入先_LostFocus(object sender, RoutedEventArgs e)
        {
            summaryCalculation();
        }
        //▲課題No299 Add End 2019/12/20
        #endregion

        #region 会社名変更時のイベント処理
        /// <summary>
        /// 会社名変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c会社名_cText1Changed(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c仕入日.Text))
            {
                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_FIX)
                {
                    string[] item = { "1,3", "0" };
                    仕入先LinkItem = item;
                }
                else
                {
                    string[] item = { "1,3", "0", this.c仕入日.Text, c会社名.Text1 };
                    仕入先LinkItem = item;
                }
                // 仕入先コードを再設定
                SearchSupplier_cTextChanged(sender, e);
            }
        }
        #endregion
        #endregion


        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            //明細行が存在しない場合は処理しない
            if (SearchResult == null) return;
            if (SearchResult.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            _編集行 = e.CellPosition.Row;

            switch (targetColumn)
            {
                case "自社品番":
                    var target = gridCtl.GetCellValueToString();
                    if (string.IsNullOrEmpty(target))
                        return;

                    //仕入先入力チェック
                    if (string.IsNullOrEmpty(c仕入先.Text1) || string.IsNullOrEmpty(c仕入先.Text2))
                    {
                        MessageBox.Show("仕入先を入力してください。");
                        return;
                    }
                    // 自社品番からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProduct,
                            new object[] {
                                target.ToString()
                                ,int.Parse(c仕入先.Text1)
                                ,int.Parse(c仕入先.Text2)
                            }));
                    break;

                case "単価":
                case "数量":
                    // 金額の再計算
                    Row targetRow = grid.Rows[grid.ActiveRowIndex];
                    decimal cost = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qty = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridCtl.SetCellValue((int)GridColumnsMapping.d金額, Math.Round(decimal.Multiply(cost, qty), 0, MidpointRounding.AwayFromZero));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchResult.Rows[targetRow.Index].EndEdit();

                    break;

                case "金額":
                case "d金額":
                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchResult.Rows[gridCtl.ActiveRowIndex].EndEdit();

                    break;
                default:
                    if (gridCtl.ActiveRowIndex >= 0)
                    {
                        // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                        SearchResult.Rows[gridCtl.ActiveRowIndex].EndEdit();
                    }
                    break;
            }
        }

        #endregion

        #region << 消費税関連処理 >>

        /// <summary>
        /// 明細内容を集計して結果を設定する
        /// </summary>
        private void summaryCalculation()
        {
            // 小計・消費税・総合計の再計算をおこなう
            //long subTotal =
            //    SearchResult.Select("", "", DataViewRowState.CurrentRows)
            //        .AsEnumerable()
            //        .Where(w => w.Field<int?>("金額") != null)
            //        .Select(x => x.Field<int>("金額"))
            //        .Sum();
            decimal conTax = 0;
            DateTime date = DateTime.Now;

            // No-94 Add Start
            int intTsujyo = 0;
            int intKeigen = 0;
            int intTaxTsujyo = 0;
            int intTaxKeigen = 0;
            int subTotal = 0;
            // No-94 Add End

            if (DateTime.TryParse(c仕入日.Text, out date))
            {
                foreach (DataRow row in SearchResult.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // 自社品番が空値(行追加のみのデータ)は処理対象外とする
                    if (string.IsNullOrEmpty(row["自社品番"].ToString()))
                        continue;

                    int taxKbnId = int.Parse(SearchHeader["Ｓ税区分ID"].ToString());
 
                    // No-94 Mod Start
                    int intZeikbn = row.Field<int>("消費税区分");
                    int intKingakuWk = Decimal.ToInt32(Math.Round(row.Field<decimal>("d金額"), 0, MidpointRounding.AwayFromZero));
                    
                    // No.272 Mod Start
                    int ival = 0;
                    int salesTaxKbn = int.TryParse(SearchHeader["Ｓ支払消費税区分"].ToString(), out ival)? ival : 1;
                    int intTaxWk = Decimal.ToInt32(taxCalc.CalculateTax(date, intKingakuWk, intZeikbn, taxKbnId, salesTaxKbn));
                    // No.272 Mod End

                    switch (intZeikbn)
                    {
                        case (int)消費税区分.通常税率:
                            intTsujyo += intKingakuWk;
                            intTaxTsujyo += intTaxWk;
                            break;
                        case (int)消費税区分.軽減税率:
                            intKeigen += intKingakuWk;
                            intTaxKeigen += intTaxWk;
                            break;
                        case (int)消費税区分.非課税:
                        default:
                            break;
                    }
                    subTotal += intKingakuWk;
                    conTax += intTaxWk;
                    // No-94 Mod End

                }

                long total = (long)(subTotal + conTax);

                // No-94 Add Start
                lbl通常税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intTsujyo);
                lbl軽減税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intKeigen);
                //▼課題No299 Mod Start 2019/12/20
                txb通常税率消費税.Text = string.Format(PRICE_FORMAT_STRING, intTaxTsujyo);
                txb軽減税率消費税.Text = string.Format(PRICE_FORMAT_STRING, intTaxKeigen);
                //▲課題No299 Mod End 2019/12/20
                // No-94 Add End

                c小計.Content = string.Format(PRICE_FORMAT_STRING, subTotal);
                c消費税.Content = string.Format(PRICE_FORMAT_STRING, conTax);
                c総合計.Content = string.Format(PRICE_FORMAT_STRING, total);

            }

        }

        //▼課題No299 Add Start 2019/12/20
        /// <summary>
        /// フッタ消費税編集時再計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb消費税_LostFocus(object sender, RoutedEventArgs e)
        {
            int intTsujyo;
            int intKeigen;
            if (int.TryParse(txb通常税率消費税.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out intTsujyo) == false)
            {
                ErrorMessage = "消費税は整数で入力してください。";
                return;
            }
            if (int.TryParse(txb軽減税率消費税.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out intKeigen) == false)
            {
                ErrorMessage = "消費税は整数で入力してください。";
                return;
            }
            int subTotal;
            if (int.TryParse(c小計.Content.ToString(), System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out subTotal) == false)
            {
                return;
            }

            c消費税.Content = string.Format(PRICE_FORMAT_STRING, intTsujyo + intKeigen);
            c総合計.Content = string.Format(PRICE_FORMAT_STRING, subTotal + intTsujyo + intKeigen);
        }

        //▲課題No299 Add End 2019/12/20
        #endregion


    }

}
