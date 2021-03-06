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
using System.Diagnostics;
using System.ComponentModel;

using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;


using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using GrapeCity.Windows.SpreadGrid;
using System.Reflection;

using System.Windows.Threading;

namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 出金入力
    /// </summary>
    public partial class DLY06010 : RibbonWindowViewBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        CommonConfig ccfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigDLY06010 : FormConfigBase
        {
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY06010 frmcfg = null;

        #endregion

		#region 定数定義

        /// <summary>出金情報取得</summary>
		private const string T12_GetData = "T12_GetData";
        /// <summary>出金情報更新</summary>
        private const string T12_Update = "T12_Update";
        /// <summary>出金情報削除</summary>
        private const string T12_Delete = "T12_Delete";
        
        /// <summary>出金ヘッダ テーブル名</summary>
        private const string T12_HEADER_TABLE_NAME = "T12_PAYHD";
        /// <summary>出金明細 テーブル名</summary>
        private const string T12_DETAIL_TABLE_NAME = "T12_PAYDTL";

        /// <summary>
        /// グリッドの金種コンボで使用されるデータ
        /// </summary>
        private Dictionary<int, string> 金種Dic = new Dictionary<int, string>()
        {
            { 0, "" },  // No.145 Mod
        };

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            金種コード = 0,
            金種名 = 1,
            金額 = 2,
            期日 = 3,
            摘要 = 4
        }

        /// <summary>
        /// 自社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        #endregion

        #region バインディングデータ

        /// <summary>出金ヘッダ情報</summary>
        private DataRow _searchHeader;
        public DataRow SearchHeader
        {
            get { return _searchHeader; }
            set
            {
                _searchHeader = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>出金明細情報</summary>
        private DataTable _searchDetail;
        public DataTable SearchDetail
        {
            get { return _searchDetail; }
            set
            {
                _searchDetail = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        #region << 初期処理群 >>

        /// <summary>
        /// 支払入力　コンストラクタ
        /// </summary>
        public DLY06010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            frmcfg = (ConfigDLY06010)ucfg.GetConfigValue(typeof(ConfigDLY06010));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY06010();
                ucfg.SetConfigValue(frmcfg);
                //画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
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

            gcSpreadGrid.InputBindings.Add(new KeyBinding(gcSpreadGrid.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            ScreenClear();
            ChangeKeyItemChangeable(true);

            #region 金種コードのドロップダウンを生成
            // No.145 Add Start
            Window view = System.Windows.Window.GetWindow(this.gcSpreadGrid);
            List<CodeData> codeList = AppCommon.GetComboboxCodeList(view, "随時", "出金問合せ", "金種", false);
            codeList = codeList.Where(x => x.コード != 0).ToList();
            foreach (CodeData code in codeList)
            {
                金種Dic.Add(code.コード, code.表示名);
            }
            // No.145 Add End

            GrapeCity.Windows.SpreadGrid.ComboBoxCellType c1 = new GrapeCity.Windows.SpreadGrid.ComboBoxCellType();
            c1.ItemsSource = 金種Dic;
            // リストに表示されるアイテムを定義します
            c1.ContentPath = "Value";
            // 表示される各アイテムに対応したデータを定義します
            c1.SelectedValuePath = "Key";
            c1.ImeConversionMode = ImeConversionModeValues.Alphanumeric;
            c1.FocusDropDownControlOnOpen = true;
            c1.ValueType = ComboBoxValueType.SelectedValue;
            c1.UpdateSourceTrigger = SpreadUpdateSourceTrigger.PropertyChanged;

            this.gcSpreadGrid.Columns[0].CellType = c1;

            #endregion

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt会社名.Text1 = ccfg.自社コード.ToString();
            this.txt会社名.IsEnabled = ccfg.自社販社区分.Equals(自社販社区分.自社.GetHashCode());

            this.txt伝票番号.Focus();
            //削除機能制御
            if (ccfg.自社販社区分 == (int)自社販社区分.自社)
            {
                rbnF12.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                rbnF12.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// データ受信処理
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
                    case T12_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            this.txt出金日.Focus();     // No.245 Add
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    case T12_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T12_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;
                    default:
                        break;

                }

            }
            catch
            {

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

        #region << リボン >>

        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);
                var tok_ctl = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    #region グリッドファンクションイベント
                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.摘要.GetHashCode())
                    {
                        SCHM11_TEK tek = new SCHM11_TEK();
                        tek.TwinTextBox = new UcLabelTwinTextBox();
                        if (tek.ShowDialog(this) == true)
                        {
                            spgrid.Cells[rIdx, cIdx].Value = tek.TwinTextBox.Text2;
                        }

                    }

                    SearchDetail.Rows[rIdx].EndEdit();

                    #endregion

                }
                else if (tok_ctl != null)
                {
                    this.txt得意先.OpenSearchWindow(this);

                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }

        // No-271 Add Start
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

            if (SearchDetail.Rows.Count >= 10)
            {
                MessageBox.Show("明細行数が上限に達している為、これ以上追加できません。", "明細上限", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataRow dtlRow = SearchDetail.NewRow();

            dtlRow["伝票番号"] = this.txt伝票番号.Text;
            if (SearchDetail.Rows.Count > 0)
            {
                dtlRow["行番号"] = (int)SearchDetail.Rows[SearchDetail.Rows.Count - 1]["行番号"] + 1;
            }
            else
            {
                dtlRow["行番号"] = 1;
            }

            SearchDetail.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            int newRowIdx = SearchDetail.Rows.Count - 1;
            gcSpreadGrid.ActiveCellPosition = new CellPosition(newRowIdx, (int)GridColumnsMapping.金種コード);
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

            if (gcSpreadGrid.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            int intDelRowIdx = gcSpreadGrid.ActiveRowIndex;                              // 削除行Index

            // 選択行の削除
            // Spreadより該当行を削除する
            try
            {
                gcSpreadGrid.Rows.Remove(intDelRowIdx);
            }
            catch
            {
                SearchDetail.Rows.Remove(SearchDetail.Rows[intDelRowIdx]);
            }

            // SearchDetailより該当行を削除する
            try
            {
                if (gcSpreadGrid.Rows.Count != SearchDetail.Rows.Count)
                {
                    SearchDetail.Rows.Remove(SearchDetail.Rows[intDelRowIdx]);
                }
            }
            catch
            {
                // エラー処理なし
            }

        }
        #endregion
        // No-271 Add End

        /// <summary>
        /// F09　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void OnF9Key(object sender, KeyEventArgs e)
        {

            gcSpreadGrid.CommitCellEdit();          // No-173 Add

            if (MaintenanceMode == null)
                return;

            // 業務入力チェックをおこなう
            if (!isFormValidation())
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.txt出金日.Focus();
                return;
            }

            if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            Update();

        }

        /// <summary>
        /// F10　リボン　入力取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            var yesno = MessageBox.Show(AppConst.CONFIRM_CANCEL, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

        }

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
					var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
					if (yesno == MessageBoxResult.No)
						return;

                }

                this.Close();

            }

        }

        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
            {
                return;
            }

            if (rbnF12.Visibility == Visibility.Visible)
            {
                var yesno = MessageBox.Show("表示中の伝票を削除してもよろしいですか？", "削除確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.Yes)
                {
                    Delete();
                }
            }
        }

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 出金ヘッダ情報設定
            DataTable tblHd = ds.Tables[T12_HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 出金明細情報設定
            DataTable tblDtl = ds.Tables[T12_DETAIL_TABLE_NAME];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

            // データ状態から編集状態を設定
            if (SearchDetail.Select("金種コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchDetail.NewRow();
                    row["伝票番号"] = AppCommon.IntParse(tblHd.Rows[0]["伝票番号"].ToString());
                    row["行番号"] = (i + 1);

                    SearchDetail.Rows.Add(row);

                }

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                this.txt出金日.Focus();

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 金種名称を設定
                int rIdx = 0;
                foreach (DataRow row in SearchDetail.Rows)
                {
                    if (string.IsNullOrEmpty(row["金種コード"].ToString()))
                        continue;

                    setSpreadGridValue(rIdx, GridColumnsMapping.金種名, 金種Dic[int.Parse(row["金種コード"].ToString())]);

                    rIdx++;

                }

                // 不足分レコードを追加
                for (int i = SearchDetail.Rows.Count; i < 10; i++)
                {
                    DataRow row = SearchDetail.NewRow();
                    row["伝票番号"] = AppCommon.IntParse(tblHd.Rows[0]["伝票番号"].ToString());
                    row["行番号"] = (i + 1);

                    SearchDetail.Rows.Add(row);

                }

                this.gcSpreadGrid.Focus();
                this.gcSpreadGrid.ActiveCellPosition = new CellPosition(0, GridColumnsMapping.金種コード.GetHashCode());

            }

        }

        /// <summary>
        /// 出金情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T12_Update,
                    new object[] {
                        SearchDetail.DataSet,
                        ccfg.ユーザID
                    }));

        }

        /// <summary>
        /// 入金情報の削除処理をおこなう
        /// </summary>
        private void Delete()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T12_Delete,
                    new object[] {
                        SearchDetail.DataSet,
                        ccfg.ユーザID
                    }));

        }

        #endregion

        #region << 入力検証処理 >>

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.txt会社名.Text1))
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

            #region 【ヘッダ】必須入力チェック

            // 出金日
            if (string.IsNullOrEmpty(this.txt出金日.Text))
            {
                this.txt出金日.Focus();
                base.ErrorMessage = string.Format("出金日が入力されていません。");
                return isResult;
            }

            // 得意先・出金先販社
            // REMARKS:どちらかの設定が必要
            if ((string.IsNullOrEmpty(this.txt得意先.Text1) || string.IsNullOrEmpty(txt得意先.Text2)) && string.IsNullOrEmpty(txt出金先販社.Text1))
            {
                this.txt得意先.Focus();
                base.ErrorMessage = string.Format("得意先または出金先販社が入力されていません。");
                return isResult;
            }
            else if ((!string.IsNullOrEmpty(this.txt得意先.Text1) || !string.IsNullOrEmpty(txt得意先.Text2)) && !string.IsNullOrEmpty(txt出金先販社.Text1))
            {
                this.txt得意先.Focus();
                base.ErrorMessage = string.Format("得意先または出金先販社のどちらかしか設定できません。");
                return isResult;
            }

            #endregion

            #region 【明細】入力チェック

            // 【明細】詳細データが１件もない場合はエラー

            // No-271 Mod Start
            // 金種コード入力件数取得
            int intInpCnt = SearchDetail.AsEnumerable()
                                .Where(w => w.Field<string>("金種コード") != null && !w.Field<string>("金種コード").Equals("0"))
                                .Count();
            // 明細件数チェック
            if (SearchDetail == null || SearchDetail.Rows.Count == 0 || intInpCnt == 0)
            // No-271 Mod End
            {
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                return isResult;
            }

            int rIdx = 0;
            bool isDetailErr = false;
            foreach (DataRow row in SearchDetail.Rows)
            {
                // 削除行は検証対象外
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // 追加行未入力レコードはスキップ
                if (row["金種コード"] == null || string.IsNullOrEmpty(row["金種コード"].ToString()) || row["金種コード"].ToString().Equals("0"))
                    continue;

                // エラー情報をクリア
                gcSpreadGrid.Rows[rIdx].ValidationErrors.Clear();

                // 金額の未入力チェック
                if (string.IsNullOrEmpty(row["金額"].ToString()))
                {
                    gcSpreadGrid.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError("金額が入力されていません。", null, rIdx, GridColumnsMapping.金額.GetHashCode()));
                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, GridColumnsMapping.金額.GetHashCode());

                    isDetailErr = true;
                }

                // 金種が「手形」の場合は期日が必須
                if (string.IsNullOrEmpty(row["期日"].ToString()))
                {
                    if (int.Parse(row["金種コード"].ToString()).Equals(金種Dic.FirstOrDefault(x => x.Value.Equals("手形")).Key))  // No.145 Mod
                    {
                        gcSpreadGrid.Rows[rIdx]
                            .ValidationErrors.Add(new SpreadValidationError("金種が「手形」の場合は期日の設定が必要です。", null, rIdx, GridColumnsMapping.期日.GetHashCode()));
                        if (!isDetailErr)
                            gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, GridColumnsMapping.期日.GetHashCode());

                        isDetailErr = true;

                    }

                }

                rIdx++;

            }

            if (isDetailErr)
                return isResult;

            #endregion

            return true;

        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            if (SearchHeader != null)
                SearchHeader = null;
            if (SearchDetail != null)
            {
                SearchDetail.Clear();
                for (int i = 0; i < 10; i++)
                    SearchDetail.Rows.Add(SearchDetail.NewRow());

            }

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            this.txt会社名.Text1 = ccfg.自社コード.ToString();
            this.txt伝票番号.Focus();

        }
        #endregion

        #region コントロールの入力可否変更
        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            this.gcSpreadGrid.IsEnabled = !flag;

        }
        #endregion

        #region << コントロールイベント >>

        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // 検索項目検証
                if (!isKeyItemValidation())
                {
                    this.txt伝票番号.Focus();
                    return;
                }

                // 全項目エラーチェック
                if (!base.CheckKeyItemValidation())
                {
                    this.txt伝票番号.Focus();
                    return;
                }

                // 入力伝票番号で検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T12_GetData,
                        new object[] {
                            this.txt会社名.Text1,
                            this.txt伝票番号.Text,
                            ccfg.ユーザID
                        }));

            }

        }

        /// <summary>
        /// 明細番号ページングボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagingButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int sendParam = btn.Name == PrevSlip.Name ? -1 : 1;

            // 検索項目検証
            if (!isKeyItemValidation())
            {
                this.txt伝票番号.Focus();
                return;
            }

            // 全項目エラーチェック
            if (!base.CheckKeyItemValidation())
            {
                this.txt伝票番号.Focus();
                return;
            }

            // 入力伝票番号で検索
            //base.SendRequest(
            //    new CommunicationObject(
            //        MessageType.RequestData,
            //        T02_GetData,
            //        new object[] {
            //                this.txt自社名.Text1,
            //                this.txt伝票番号.Text,
            //                sendParam,
            //                ccfg.ユーザID
            //            }));

        }

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY06010(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

        #endregion

        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            switch (e.CellPosition.ColumnName)
            {
                case "金種コード":
                    var target = getSpreadGridValue(e.CellPosition.Row, GridColumnsMapping.金種コード);
                    if (target == null)
                        return;

                    int val = int.Parse(target.ToString());
                    setSpreadGridValue(e.CellPosition.Row, GridColumnsMapping.金種名, 金種Dic.ContainsKey(val) ? 金種Dic[val] : string.Empty);
                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// スプレッドグリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            // Delete押下時の処理
            // REMARKS:編集状態でない場合のDeleteキーは無視する
            if (e.Key == Key.Delete && !grid.Cells[gcSpreadGrid.ActiveRowIndex, grid.ActiveColumnIndex].IsEditing)
                e.Handled = true;

        }

        /// <summary>
        /// 指定セルの値を取得する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <returns></returns>
        private object getSpreadGridValue(int rIdx, GridColumnsMapping column)
        {
            if (gcSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
                return null;

            return gcSpreadGrid.Cells[rIdx, column.GetHashCode()].Value;

        }

        /// <summary>
        /// 指定セルの値を設定する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <param name="value">設定値</param>
        private void setSpreadGridValue(int rIdx, GridColumnsMapping column, object value)
        {
            if (gcSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
                return;

            gcSpreadGrid.Cells[rIdx, column.GetHashCode()].Value = value;

        }

        #endregion

    }

}
