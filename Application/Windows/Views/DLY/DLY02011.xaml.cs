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
    using KyoeiSystem.Application.Windows.Views.Common;

    /// <summary>
    /// 揚り入力
    /// </summary>
    public partial class DLY02011 : RibbonWindowViewBase
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
        public class ConfigDLY02011 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY02011 frmcfg = null;

        #endregion

        #region 定数定義

        #region サービスアクセス定義
        /// <summary>更新用_在庫数チェック</summary>
        private const string UpdateData_StockCheck = "T04_UpdateData_CheckStock";       // No-222 Add
        /// <summary>セット品番情報検索</summary>
        private const string SearchTableToShin = "T04_GetM10_Shin";
        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProduct = "UcCustomerProduct";

        #endregion

        #region 使用テーブル名定義
        #endregion

        #endregion

        #region 画面間パラメータ
        /// <summary>揚りヘッダ情報</summary>
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

        /// <summary>揚り明細情報</summary>
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

        /// <summary>揚り部材明細情報</summary>
        private DataTable _innerDetail;
        public DataTable InnerDetail
        {
            get { return _innerDetail; }
            set
            {
                _innerDetail = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            色コード = 1,
            自社品名 = 2,
            色名称 = 3,
            品番コード = 4,
            賞味期限 = 5,
            数量 = 6,
            単位 = 7,
            必要数= 8,
            在庫数量 = 9,
            商品分類 = 10
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

        #endregion

        #region バインディングデータ
 
        /// <summary>
        /// セット品データテーブル
        /// </summary>
        public DataTable _SetHin;
        public DataTable SetHin
        {
            get { return _SetHin; }
            set
            {
                this._SetHin = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 編集中の行番号
        /// </summary>
        private int _編集行;

        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridDtl;
        GcSpreadGridController gridDtb;

        /// <summary>警告フラグ(true:警告あり、false:警告なし)</summary>
        private bool blnWarningFlg = false;

        /// <summary>画面を閉じる(true:閉じる、false:閉じない)</summary>
        private bool blnCloseFlg = false;

        #endregion

        #region << 初期処理群 >>

        /// <summary>
        /// 揚り入力　コンストラクタ
        /// <param name="行番号"></param>
        /// <param name=""></param>
        /// </summary>
        public DLY02011()
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

            frmcfg = (ConfigDLY02011)ucfg.GetConfigValue(typeof(ConfigDLY02011));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY02011();
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

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M09_HIN_SET", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            // グリッドコントローラ設定
            gridDtl = new GcSpreadGridController(gcDetailSpreadGrid);
            gridDtb = new GcSpreadGridController(gcInnerSpreadGrid);

            // セット品情報を取得
            sendSearchForShin(SearchDetail.Rows[0]["品番コード"].ToString());

            // 画面初期化　
            ScreenClear();

            // 入力可否切り替え
            ChangeKeyItemChangeable(false);

            gcDetailSpreadGrid.IsEnabled = false;

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
            {
                // ボタン押下不可
                SetDispRibbonEnabled(false);
                // Spread入力不可
                SetDispInnerSpreadGridLocked(true);

            }
            else if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                // Spread入力不可
                SetDispInnerSpreadGridLocked(true);                 // No.424 Add
            }

            // フォーカス設定
            gcInnerSpreadGrid.Focus();                              // No.425 Add
            gridDtl.SetCellFocus(0, 0);

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
                    case UpdateData_StockCheck:
                        // No-222 Add Start
                        // 在庫数チェック結果受信
                        Dictionary<string, string> updateList = data as Dictionary<string, string>;
                        string zaiUpdateMessage = AppConst.CONFIRM_UPDATE;
                        var zaiMBImage = MessageBoxImage.Question;

                        foreach (DataRow row in SearchDetail.Select("", "", DataViewRowState.CurrentRows))
                        {
                            if (updateList.Count > 0)
                            {
                                zaiMBImage = MessageBoxImage.Warning;
                                zaiUpdateMessage = "在庫がマイナスになる品番が存在しますが、\r\n登録してもよろしいでしょうか？";
                                break;
                            }
                        }

                        if (MessageBox.Show(zaiUpdateMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;

                        // No-222 Add End

                        Update();

                        // 致命的なエラーの場合、以降の処理を中止する
                        if (blnCloseFlg == false)
                        {
                            return;
                        }

                        // 警告がない場合、当該画面を終了する
                        if (blnWarningFlg == true)
                        {
                            // 処理続行確認メッセージを表示する
                            var yesno = MessageBox.Show("警告がありますが処理を続行しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                            if (yesno == MessageBoxResult.No)
                            {
                                return;
                            }
                            CloseDataEdited();
                        }
                        else
                        {
                            CloseDataEdited();
                        }

                        break;

                    case SearchTableToShin:
                        // セット品で検索された場合
                        SetHin = tbl.Copy();
                        break;

                    case MasterCode_MyProduct:
                        #region 自社品番手入力時
                        DataTable ctbl = data as DataTable;

                        int columnIdx = gridDtb.ActiveColumnIndex;
                        int rIdx = gridDtb.ActiveRowIndex;

                        // フォーカス移動後の項目が異なる場合または編集行が異なる場合は処理しない。
                        if ((columnIdx != (int)GridColumnsMapping.色コード) || _編集行 != rIdx) return;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            gridDtb.SetCellValue((int)GridColumnsMapping.品番コード, 0);
                            gridDtb.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            gridDtb.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            gridDtb.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            gridDtb.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            gridDtb.SetCellValue((int)GridColumnsMapping.商品分類, (int)商品分類.その他);
                            gridDtb.SetCellValue((int)GridColumnsMapping.色コード, string.Empty);
                            gridDtb.SetCellValue((int)GridColumnsMapping.色名称, string.Empty);

                            // フォーカス位置の設定
                            gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.自社品番);               // No.425 Add

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            int cIdx = gcInnerSpreadGrid.ActiveColumnIndex;
                            var colVal = gridDtb.GetCellValueToString((int)GridColumnsMapping.自社品番);

                            int code = int.Parse(SearchHeader["外注先コード"].ToString());
                            int eda = int.Parse(SearchHeader["外注先枝番"].ToString());

                            // 自社品番の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.txtCode.Text = colVal;
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 2;

                            if (myhin.ShowDialog(this) == true)
                            {
                                gridDtb.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.数量, 1m);
                                gridDtb.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                                gridDtb.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);

                                // フォーカス位置の設定
                                gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.自社品名);             // No.425 Add
                            }
                            else
                            {
                                // フォーカス位置の設定
                                gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.自社品番);             // No.425 Add
                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gcInnerSpreadGrid.BeginEdit();
                            gridDtb.SetCellValue((int)GridColumnsMapping.品番コード, drow["品番コード"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.自社品番, drow["自社品番"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.自社品名, drow["自社品名"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridDtb.SetCellValue((int)GridColumnsMapping.単位, drow["単位"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.色コード, drow["自社色"]);
                            gridDtb.SetCellValue((int)GridColumnsMapping.色名称, drow["色名称"]);

                            // 自社品番のセルをロック
                            // 数量以外はロック
                            gridDtb.SetCellLocked((int)GridColumnsMapping.品番コード, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.自社品名, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.単位, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.商品分類, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.色コード, true);
                            gridDtb.SetCellLocked((int)GridColumnsMapping.色名称, true);

                            // フォーカス位置の設定
                            gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.自社品名);                 // No.425 Add
                        }

                        InnerDetail.Rows[rIdx].EndEdit();

                        #endregion

                        break;
 
                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        #region セット品 データ取得
        /// <summary>
        /// セット品情報を取得する
        /// <param name="strProduct">品番コード（セット品）</param>
        /// </summary>
        private void sendSearchForShin(string strProduct)
        {
            if (!string.IsNullOrEmpty(strProduct))
            {
                int intProduct = int.Parse(strProduct);

                // セット品の構成品を取得
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        SearchTableToShin,
                        new object[] {
                            intProduct
                        }));

            }
        }
        #endregion
        
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

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

        }
        #endregion

        #region 画面を閉じる
        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void CloseDataEdited()
        {

            this.DialogResult = true;
            this.Close();

        }
        #endregion

        #region << リボン >>

        #region F1 マスタ検索
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
                var twintxt = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    #region グリッドファンクションイベント
                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.自社品番.GetHashCode())
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (spgrid.Cells[rIdx, cIdx].Locked == true)
                        {
                            return;
                        }

                        int code = int.Parse(SearchHeader["外注先コード"].ToString());
                        int eda = int.Parse(SearchHeader["外注先枝番"].ToString());

                        // 自社品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.IsDisabledItemTypes = null;              // No.362 Mod
                        myhin.TwinTextBox.LinkItem = 2;
                        if (myhin.ShowDialog(this) == true)
                        {
                            //入力途中のセルを未編集状態に戻す
                            spgrid.CancelCellEdit();

                            spgrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = myhin.SelectedRowData["品番コード"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = myhin.SelectedRowData["自社品番"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = myhin.SelectedRowData["自社品名"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 1m;
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.単位].Value = myhin.SelectedRowData["単位"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = myhin.SelectedRowData["商品分類"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.色コード].Value = myhin.SelectedRowData["自社色"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.色名称].Value = myhin.SelectedRowData["自社色名"];

                            // 設定自社品番の編集を不可とする
                            gridDtb.SetCellLocked((int)GridColumnsMapping.自社品番, true);

                        }

                    }

                    InnerDetail.Rows[rIdx].EndEdit();

                    #endregion

                }
                else
                {
                    if (!(twintxt is UcLabelTwinTextBox))
                    {
                        return;
                    }
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                    var twinText = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);
                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F5 構成品行追加
        /// <summary>
        /// F05　リボン　構成品行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            // 画面初期状態の場合、以降の処理を中止する
            if (this.MaintenanceMode == null)
            {
                return;
            }

            // 行数の取得を行う
            int delRowCount = (InnerDetail.GetChanges(DataRowState.Deleted) == null) ? 0 : InnerDetail.GetChanges(DataRowState.Deleted).Rows.Count;
            DataRow dtlRow = InnerDetail.NewRow();

            InnerDetail.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            int newRowIdx = InnerDetail.Rows.Count - delRowCount - 1;
            gridDtb.SetCellFocus(newRowIdx, (int)GridColumnsMapping.自社品番);             // No.425 Mod

        }
        #endregion

        #region F6 構成品行削除
        /// <summary>
        /// F06　リボン　構成品行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            // 画面初期状態の場合、以降の処理を中止する
            if (this.MaintenanceMode == null)
            {
                return;
            }

            // 行未選択の場合、以降の処理を中止する
            if (gcInnerSpreadGrid.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }

            // 確認メッセージ出力
            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;


            // 行削除
            int intDelRowIdx = gridDtb.ActiveRowIndex;

            try
            {
                gridDtb.SpreadGrid.Rows.Remove(intDelRowIdx);
            }
            catch
            {
                InnerDetail.Rows.Remove(InnerDetail.Rows[intDelRowIdx]);
            }

            try
            {
                if (gridDtb.SpreadGrid.Rows.Count != InnerDetail.Rows.Count)
                {
                    InnerDetail.Rows.Remove(InnerDetail.Rows[intDelRowIdx]);
                }
            }
            catch
            {
                // エラー処理なし
            }

        }
        #endregion

        #region F9 登録
        /// <summary>
        /// F09　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            // Spread編集内容を確定させる
            gcDetailSpreadGrid.CommitCellEdit();
            gcInnerSpreadGrid.CommitCellEdit();

            // 画面初期状態または揚り部材明細が未設定の場合、以降の処理を中止する
            if (MaintenanceMode == null || InnerDetail == null)
            {
                return;
            }

           // No.423 Add Start
           #region 空行削除
            for (int rowIdx = gridDtb.SpreadGrid.Rows.Count - 1; rowIdx >= 0; rowIdx--)
            {
                // 品番コードがnullのデータは削除
                if (gridDtb.SpreadGrid.Cells[rowIdx, "品番コード"].Value == null || (int)gridDtb.SpreadGrid.Cells[rowIdx, "品番コード"].Value == 0)
                {
                    try
                    {
                        // フォーカスがない状態で削除するとエラーが発生するためフォーカスを設定
                        gridDtb.SpreadGrid.Focus();
                        gridDtb.SpreadGrid.Rows.Remove(rowIdx);
                    }
                    catch
                    {
                        InnerDetail.Rows.Remove(InnerDetail.Rows[rowIdx]);
                    }

                    if (gridDtb.SpreadGrid.Rows.Count != InnerDetail.Rows.Count)
                    {
                        try
                        {
                            InnerDetail.Rows.Remove(InnerDetail.Rows[rowIdx]);
                        }
                        catch
                        {
                            // エラー処理なし
                        }
                    }
                }
            }
            #endregion
            // No.423 Add End

            // 在庫チェックを行う
            DataSet ds = new DataSet();
            ds.Tables.Add(SearchHeader.Table.Copy());
            ds.Tables.Add(SearchDetail.Copy());
            ds.Tables.Add(InnerDetail.Copy());

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UpdateData_StockCheck,
                    new object[] {
                        ds,
                        ccfg.ユーザID
                    }));

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
            {
                this.Close();
            }    
            else
            {
                // 登録ボタン押下後、終了可能な場合、当該画面を終了する
                if (blnCloseFlg == true)
                {
                    CloseDataEdited();
                    return;
                }
                
                if (DataUpdateVisible != Visibility.Hidden)
                {
                    var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (yesno == MessageBoxResult.No)
                        return;

                }

                this.Close();

            }

        }
        #endregion

        #endregion

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY02011(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // TODO:処理が必要な場合に記述する 
        }

        /// <summary>
        /// 揚り情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            blnCloseFlg = false;

            // 業務入力チェックをおこなう
            if (isFormValidation() == false)
            {
                return;
            }

            if (blnWarningFlg == false)
            {
                // 全項目エラーチェック
                if (base.CheckAllValidation() == false)
                {
                    return;
                }
            }

            blnCloseFlg = true;

        }

        #endregion

        #region << 入力検証処理 >>

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns>bool</returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            // TODO:処理が必要な場合に記述する 

            return isResult = true;

        }

        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns>bool</returns>
        private bool isFormValidation()
        {
            bool isResult = false;
            int intSyohinkeitaibunrui = 0;

            blnWarningFlg = false;

            // 揚り明細より情報を取得する
            foreach (DataRow row in SearchDetail.Rows)
            {
                intSyohinkeitaibunrui = int.Parse(row["商品形態分類"].ToString());
            }

            // 現在の明細行を取得
            var CurrentDetail = InnerDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable();

            // 【明細】詳細データが１件もない場合はエラー
            if (InnerDetail == null || CurrentDetail.Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                gridDtb.SpreadGrid.Focus();
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                return isResult;
            }

            // 【明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            int rIdx = 0;
            bool isDetailErr = false;
            foreach (DataRow row in InnerDetail.Rows)
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
                gridDtb.ClearValidationErrors(rIdx);

                DateTime? row賞味期限 = DBNull.Value.Equals(row["賞味期限"]) ? (DateTime?)null : Convert.ToDateTime(row["賞味期限"]);
                int? row品番コード = DBNull.Value.Equals(row["品番コード"]) ? (int?)null : Convert.ToInt32(row["品番コード"]);
                if (CurrentDetail.Where(x => x.Field<int?>("品番コード") == row品番コード && x.Field<DateTime?>("賞味期限") == row賞味期限).Count() > 1)
                {
                    gridDtb.SpreadGrid.Focus();
                    base.ErrorMessage = string.Format("同じ商品が存在するので、一つに纏めて下さい。");
                    gridDtb.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "同じ商品が存在するので、一つに纏めて下さい。");
                    if (!isDetailErr)
                        gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                    isDetailErr = true;
                }

                if (string.IsNullOrEmpty(row["数量"].ToString()))
                {
                    gridDtb.AddValidationError(rIdx, (int)GridColumnsMapping.数量, "数量が入力されていません。");
                    if (!isDetailErr)
                        gridDtb.SetCellFocus(rIdx, (int)GridColumnsMapping.数量);

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」かつ数量が0以外の場合はエラー
                    if (type.Equals((int)商品分類.食品) && row["数量"].ToString().Equals("0.00") == false)
                    {
                        gridDtb.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "商品分類が『食品』の為、賞味期限の設定が必要です。");
                        isDetailErr = true;
                    }

                }

                // 商品形態分類がセット品の場合、エラーチェックを行う
                if (intSyohinkeitaibunrui == 1)           // No-279 Add
                {
                    // セット品番マスタチェック（存在しない）
                    if (DLY02010.isCheckShinNotExist(SetHin, row["品番コード"].ToString()) == false)
                    {
                        gridDtb.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "製品を構成しない部材が入力されています。");
                        blnWarningFlg = true;
                    }
                }
                rIdx++;

            }

            // 商品形態分類がセット品の場合、エラーチェックを行う
            if (intSyohinkeitaibunrui == 1)           // No-279 Add
                {
                // セット品番マスタチェック（構成しない部材）
                if (DLY02010.isCheckShinUnnecessary(SetHin, InnerDetail) == false)
                {
                    gridDtb.SpreadGrid.Focus();
                    base.ErrorMessage = string.Format("製品を構成する部材が入力されていません。");
                    blnWarningFlg = true;
                }

                // セット品番マスタチェック（数量）
                string strErrMsg = string.Empty;
                if (DLY02010.isCheckShinQuantity(SetHin, SearchDetail, InnerDetail, out strErrMsg) == false)
                {
                    gridDtb.SpreadGrid.Focus();
                    base.ErrorMessage = string.Format(strErrMsg);
                    blnWarningFlg = true;
                }
            }

            if (isDetailErr)
                return isResult;

            isResult = true;

            return isResult;

        }

        #endregion

        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            gridDtl.SetEnabled(!flag);
            gridDtb.SetEnabled(!flag);

        }

        #region 画面リボンの入力制御
        /// <summary>
        /// 画面リボンの入力制御
        /// </summary>
        /// <param name="blnEnabled">true:入力可、false:入力不可</param>
        private void SetDispRibbonEnabled(bool blnEnabled)
        {
            // 使用設定（可・不可）
            this.RibbonF1.IsEnabled = blnEnabled;
            this.RibbonF5.IsEnabled = blnEnabled;
            this.RibbonF6.IsEnabled = blnEnabled;
            this.RibbonF9.IsEnabled = blnEnabled;
        }

        #endregion

        #region 揚り部材明細Spreadの入力制御
        /// <summary>
        /// 揚り部材明細Spreadの入力制御
        /// </summary>
        /// <param name="blnLocked">true:ロック、false:ロック解除</param>
        private void SetDispInnerSpreadGridLocked(bool blnLocked)
        {
            // 取得明細の自社品番をロック(編集不可)に設定
            foreach (var row in gcInnerSpreadGrid.Rows)
            {
                // No.424 Add Start
                if (row.Cells[(int)GridColumnsMapping.品番コード].Value != null)
                {
                    row.Cells[(int)GridColumnsMapping.自社品番].Locked = blnLocked;

                    // 編集時のロック制御
                    if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                    {
                        row.Cells[(int)GridColumnsMapping.賞味期限].Locked = blnLocked;
                        row.Cells[(int)GridColumnsMapping.数量].Locked = blnLocked;
                    }
                }
                // No.424 Add End
            }

        }

        #endregion

        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcInnerSpreadGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            //明細行が存在しない場合は処理しない
            if (InnerDetail == null) return;
            if (InnerDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            _編集行 = e.CellPosition.Row;

            switch (targetColumn)
            {
                case "自社品番":
                    var target = gridDtb.GetCellValueToString();
                    if (string.IsNullOrEmpty(target))
                        return;

                    // 自社品番からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProduct,
                            new object[] {
                                target.ToString()
                               ,string.Empty,
                                string.Empty
                            }));
                    break;

                case "数量":
                case "賞味期限":

                    InnerDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();

                    break;

                default:
                    break;

            }

        }
        #endregion

        #region << SpreadGrid部品 >>
        /// <summary>
        /// 指定セルの値を取得する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <returns></returns>
        private object getSpreadGridValue(int rIdx, GridColumnsMapping column)
        {
            if (gcInnerSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
            {
                return null;
            }

            return gcInnerSpreadGrid.Cells[rIdx, column.GetHashCode()].Value;

        }

        /// <summary>
        /// 指定セルの値を設定する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <param name="value">設定値</param>
        private void setSpreadGridValue(int rIdx, GridColumnsMapping column, object value)
        {
            if (gcInnerSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
            {
                return;
            }
    
            gcInnerSpreadGrid.Cells[rIdx, column.GetHashCode()].Value = value;

        }

        #endregion

    }

}
