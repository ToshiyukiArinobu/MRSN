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
using KyoeiSystem.Application.Windows.Views;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    using SpreadGridRow = GrapeCity.Windows.SpreadGrid.Row;
    using UcLabelTwinTextBox = Framework.Windows.Controls.UcLabelTwinTextBox;

    /// <summary>
    /// セット品番マスタ
    /// </summary>
    public partial class MST03010 : WindowReportBase
    {
        /// <summary>
        /// スプレッドシート列定義クラス
        /// </summary>
        public class M10_SHIN_MEMBER : INotifyPropertyChanged
        {
            public string _自社品名;
            public int _品番コード;
            public int _行;
            public string _材料品番;
            public string _材料品名;
            public string _材料色;
            public string _数量;

            public string 自社品名 { get { return _自社品名; } set { _自社品名 = value; NotifyPropertyChanged(); } }
            public int 品番コード { get { return _品番コード; } set { _品番コード = value; NotifyPropertyChanged(); } }
            public int 行 { get { return _行; } set { _行 = value; NotifyPropertyChanged(); } }
            public string 材料品番 { get { return _材料品番; } set { _材料品番 = value; NotifyPropertyChanged(); } }
            public string 材料品名 { get { return _材料品名; } set { _材料品名 = value; NotifyPropertyChanged(); } }
            public string 材料色 { get { return _材料色; } set { _材料色 = value; NotifyPropertyChanged(); } }
            public string 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }


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

        #region 定数
        /// <summary>セット品番情報検索</summary>
        private const string TargetTableNm = "M10_SHIN";
        /// <summary>品番情報取得</summary>
        private const string TargetTableNm_GetProduct = "M09_HIN_getNamedData";
        /// <summary>セット品番情報更新</summary>
        private const string TargetTableNm_Update = "M10_SHIN_Update";

        #endregion

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
        public class ConfigMST22010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }
        /// ※ 必ず public で定義する。
        public ConfigMST22010 frmcfg = null;
        public byte[] spConfig = null;

        #endregion

        #region データバインド用プロパティ

        //スプレッドバインド変数
        private List<M10_SHIN_MEMBER> _SetKouseihin;
        public List<M10_SHIN_MEMBER> SetKouseihin
        {
            get
            {
                return this._SetKouseihin;
            }
            set
            {
                this._SetKouseihin = value;
                this.spComponent.ItemsSource = null;
                this.spComponent.ItemsSource = value;
                NotifyPropertyChanged();
            }

        }

        /// <summary>
        /// 表示用マスタデータ
        /// </summary>
        private M10_SHIN_MEMBER _MstData;
        public M10_SHIN_MEMBER MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST03010
        /// <summary>
        /// セット品番マスタ
        /// </summary>
        public MST03010()
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期化
            ScreenClear();

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST22010)ucfg.GetConfigValue(typeof(ConfigMST22010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST22010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
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

            //if (frmcfg.spConfig != null)
            //    AppCommon.LoadSpConfig(this.spComponent, frmcfg.spConfig);

            spComponent.InputBindings.Add(new KeyBinding(spComponent.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { null, typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M06_IRO", new List<Type> { null, typeof(SCHM06_IRO) });

            if (!string.IsNullOrEmpty(this.txtMyProduct.Text1))
            {
                // セット品構成品取得
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TargetTableNm,
                        new object[] {
                            this.txtMyProduct.Text1,
                            this.txtMyColor.Text1
                        }));

            }

            // 初期状態記憶
            //this.spConfig = AppCommon.SaveSpConfig(this.spComponent);

        }
        #endregion

        #region エラー表示用
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

        #region データ受信時イベント
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
                    // マスター登録、マスター情報検索時のデータをSPREADに表示。
                    case TargetTableNm:
                        // -- 検索処理結果
                        // SPREADシート追加
                        setTableBlankRows(tbl);
                        // datatableをlistへ変換
                        SetKouseihin = (List<M10_SHIN_MEMBER>)AppCommon.ConvertFromDataTable(typeof(List<M10_SHIN_MEMBER>), tbl);
                        if (SetKouseihin.Count > 0)
                        {
                            this.spComponent.IsEnabled = true;
                            this.spComponent.Focus();
                            this.spComponent.ActiveCellPosition = new CellPosition(0, 2);
                            txtMyProduct.Text2 = SetKouseihin[0].自社品名;
                            ChangeKeyItemChangeable(false);
                        }
                        break;

                    case TargetTableNm_GetProduct:
                        // -- 入力品番情報取得結果
                        setInputRowData(tbl);
                        break;

                    case TargetTableNm_Update:
                        // -- 編集情報更新結果
                        MessageBox.Show("データの登録が完了しました。");
                        ScreenClear();
                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void setInputRowData(DataTable tbl)
        {
            SpreadGridRow row = spComponent.ActiveRow;

            if (tbl == null || tbl.Rows.Count == 0)
            {
                // 現在行を初期化して終了
                row.Cells[0].Value = 0;
                row.Cells[3].Value = string.Empty;
                row.Cells[4].Value = string.Empty;

                return;
            }

            DataRow dr = tbl.Rows[0];

            row.Cells[0].Value = int.Parse(dr["品番コード"].ToString());
            row.Cells[2].Value = dr["自社品番"].ToString();
            row.Cells[3].Value = dr["自社品名"].ToString();
            row.Cells[4].Value = dr["自社色名"].ToString();

        }

        /// <summary>
        /// 取得データを設定する
        /// </summary>
        /// <param name="tbl"></param>
        private void strData(DataTable tbl)
        {
            if (tbl.Rows.Count > 0)
            {
                MstData = getRowData<DataRow>(tbl.Rows[0]);

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

                // 編集モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

            }
            else
            {
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

                // 新規作成モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

            }

        }

        private M10_SHIN_MEMBER getRowData<T>(T dataRow)
        {
            M10_SHIN_MEMBER member = new M10_SHIN_MEMBER();
            if (dataRow is DataRow)
            {
                var dr = dataRow as DataRow;
                member.品番コード = int.Parse(dr["品番コード"].ToString());
                member.行 = int.Parse(dr["行"].ToString());
                member.材料品番 = dr["材料品番"].ToString();
                member.材料品名 = dr["材料品名"].ToString();
                member.材料色 = dr["材料色"].ToString();
                member.数量 = dr["数量"].ToString();

            }
            else if (dataRow is SpreadGridRow)
            {
                var sgr = dataRow as SpreadGridRow;
                if (sgr.Cells[0].Value == null)
                    return member;

                member.品番コード = parseObject(sgr.Cells[0].Value, 0);
                member.行 = parseObject(sgr.Cells[1].Value, 0);
                member.材料品番 = sgr.Cells[2].Value == null ? string.Empty : sgr.Cells[2].Value.ToString();
                member.材料品名 = sgr.Cells[3].Value == null ? string.Empty : sgr.Cells[3].Value.ToString();
                member.材料色 = sgr.Cells[4].Value == null ? string.Empty : sgr.Cells[4].Value.ToString();
                member.数量 = sgr.Cells[5].Value == null ? string.Empty : sgr.Cells[5].Value.ToString();

            }

            return member;

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
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);
                if (spgrid != null)
                {
                    int actrow = spgrid.ActiveRowIndex;
                    spgrid.CommitCellEdit();

                    UcLabelTwinTextBox dmy = new UcLabelTwinTextBox();
                    switch (spgrid.ActiveColumn.Name)
                    {
                        case "材料品番":
                            var val = spComponent.Cells[spComponent.ActiveRowIndex, spgrid.ActiveColumn.Name].Value;
                            SCHM09_MYHIN myHin = new SCHM09_MYHIN();
                            myHin.chkItemClass_1.IsChecked = false;
                            myHin.chkItemClass_1.IsEnabled = false;
                            myHin.TwinTextBox = dmy;
                            if (myHin.ShowDialog(this) ?? false)
                            {
                                SpreadGridRow row = spComponent.Rows[spComponent.ActiveRowIndex];

                                string myCode = myHin.SelectedRowData["自社品番"].ToString();
                                int cnt = SetKouseihin.Where(x => x.材料品番 == myCode).Count();
                                if (cnt > 0)
                                {
                                    MessageBox.Show("同じ材料品番が既に登録されています。");
                                    return;
                                }

                                row.Cells[0].Value = myHin.SelectedRowData["品番コード"].ToString();
                                row.Cells[2].Value = myCode;
                                row.Cells[3].Value = myHin.SelectedRowData["自社品名"].ToString();
                                row.Cells[4].Value = myHin.SelectedRowData["自社色名"].ToString();

                                NotifyPropertyChanged();

                            }
                            break;

                        default:
                            break;
                    }

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

        /// <summary>
        /// F8　リボン　リスト一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            // TODO:仕様書が無い為、未実装
            //MST22020 mst22020 = new MST22020();
            //mst22020.ShowDialog(this);

        }

        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (SetKouseihin == null)
                return;

            string errMsg = isSpreadInputValidation();
            if (!string.IsNullOrEmpty(errMsg))
            {
                string msg = errMsg;
                this.ErrorMessage = msg;
                return;
            }

            if (!base.CheckAllValidation())
            {
                string msg = "入力内容に誤りがあります。";
                this.ErrorMessage = msg;
                SetFocusToTopControl();
                return;
            }

            Update();

        }

        /// <summary>
        /// F10　リボン　入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            // メッセージボックス
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            // OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
                SetKouseihin = null;

            }

        }

        /// <summary>
        /// F11　リボン　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            AppCommon.SaveSpConfig(this.spComponent);
            this.Close();

        }

        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                {
                    this.ErrorMessage = "新規登録データは削除できません。";
                    MessageBox.Show("新規登録データは削除できません。");
                    SetFocusToTopControl();
                    return;

                }

                // 削除用の空データセットを作成
                DataSet ds = new DataSet();
                ds.Tables.Add("result");

                this.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNm_Update,
                        new object[] {
                            txtMyProduct.Text1,
                            txtMyColor.Text1,
                            ds,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception)
            {
                this.ErrorMessage = "更新時にエラーが発生しました。";
                return;

            }

        }

        #endregion

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
        {
            spComponent.InputBindings.Clear();
            SetKouseihin = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 処理メソッド

        /// <summary>
        /// 画面表示を初期化する
        /// </summary>
        private void ScreenClear()
        {
            MstData = null;
            SetKouseihin = null;
            ChangeKeyItemChangeable(true);
            ResetAllValidation();
            SetFocusToTopControl();

        }

        /// <summary>
        /// セット品番情報更新
        /// </summary>
        public void Update()
        {
            try
            {
                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.No)
                    return;

                DataTable dt = new DataTable();
                AppCommon.ConvertToDataTable(SetKouseihin, dt);

                // 引き渡しパラメータを生成
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Clone());
                ds.Tables[0].TableName = "result";
                foreach (DataRow dr in dt.Select("材料品番 <> ''"))
                {
                    DataRow nRow = ds.Tables[0].NewRow();
                    nRow.ItemArray = dr.ItemArray;
                    ds.Tables[0].Rows.Add(nRow);
                }

                // 更新処理実行
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNm_Update,
                        new object[] {
                            txtMyProduct.Text1,
                            txtMyColor.Text1,
                            ds,
                            ccfg.ユーザID
                        }));

            }
            catch
            {
                // 更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";
                return;

            }

        }

        /// <summary>
        /// 構成品Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spComponent_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            var spGrid = (sender as GcSpreadGrid);
            if (spGrid == null)
                return;

            try
            {
                // 編集列が『材料品番』の場合
                if (spGrid.ActiveColumn.Index == 2)
                {
                    SpreadGridRow row = spGrid.Rows[e.CellPosition.Row];
                    string myCode = (string)row.Cells[spGrid.Columns["材料品番"].Index].Value;

                    if (string.IsNullOrEmpty(myCode))
                    {
                        // 材料品番が空値の場合は各項目を初期化する
                        ClearSpreadRow(myCode);
                        NotifyPropertyChanged();

                    }
                    else
                    {
                        int cnt = SetKouseihin.Where(x => x.材料品番 == myCode).Count();
                        if (cnt > 1)
                        {
                            MessageBox.Show("同じ材料品番が既に登録されています。");
                            return;
                        }

                        // 入力値より品番情報を取得
                        this.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                TargetTableNm_GetProduct,
                                new object[] {
                                    myCode,
                                    new int[] { 2, 3, 4 }     // 対象商品形態：2:単品・材料、3:雑コード、4:副資材
                                }));

                    }

                }

            }
            catch (Exception)
            {
                this.ErrorMessage = "入力内容が不正です。";
            }

        }

        private void ClearSpreadRow(string myCode)
        {
            spComponent.BeginEdit();

            var data = SetKouseihin.Where(x => x.材料品番 == myCode).FirstOrDefault();

            if (data != null)
            {
                data.品番コード = 0;
                data.材料品番 = string.Empty;
                data.材料品名 = string.Empty;
                data.材料色 = string.Empty;
                data.数量 = string.Empty;

            }

            spComponent.CommitCellEdit();
        
        }

        /// <summary>
        /// 規定数に達するまで空行を追加する
        /// </summary>
        /// <param name="tbl"></param>
        private void setTableBlankRows(DataTable tbl)
        {
            for (int i = tbl.Rows.Count + 1; i <= 32; i++)
            {
                DataRow row = tbl.NewRow();
                row["行"] = i;
                tbl.Rows.Add(row);

            }

        }

        /// <summary>
        /// 条件指定部(セット色指定)でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // セット品構成品取得
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TargetTableNm,
                        new object[] {
                            this.txtMyProduct.Text1,
                            this.txtMyColor.Text1
                        }));

            }

        }

        #endregion

        private T parseObject<T>(object obj, T defValue) where T : struct
        {
            if (obj == null)
                return defValue;

            Type type = typeof(T);

            if (type == typeof(int))
            {
                int ival = 0;
                if (int.TryParse(obj.ToString(), out ival))
                    return (T)(object)ival;

                else
                    return defValue;

            }
            else if (type == typeof(string))
            {
                if (obj == null)
                    return defValue;

                return (T)(object)obj;

            }
            else
            {
            }

            return (T)obj;

        }

        /// <summary>
        /// スプレッドシート内のエラーチェックをおこなう
        /// </summary>
        /// <returns></returns>
        private string isSpreadInputValidation()
        {
            string errMsg = string.Empty;

            int line = 1;
            foreach (var data in SetKouseihin)
            {
                if (string.IsNullOrEmpty(data.材料品番))
                    continue;

                if (string.IsNullOrEmpty(data.材料品名))
                {
                    // 名称＝空値 => 対象外(SET品)の品番 or 品番登録なし
                    errMsg = string.Format("{0}行目：品番は登録されていません。", line);
                    return errMsg;
                }

                if (string.IsNullOrEmpty(data.数量))
                {
                    // 数量は必須
                    errMsg = string.Format("{0}行目：数量が入力されていません。", line);
                    return errMsg;
                }

                line++;

            }

            return string.Empty;

        }

        /// <summary>
        /// スプレッドグリッドでキーが押される時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spComponent_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var spGrid = (sender as GcSpreadGrid);

                if (spGrid == null || spGrid.ActiveRow == null)
                    return;

                int rowIdx = spGrid.ActiveRow.Index;
                if (spGrid.ActiveColumn.Index == 2)
                {
                    // 選択列が『材料品番』だった場合、設定内容をクリア
                    string val = (string)spGrid.Cells[rowIdx, 2].Value;
                    ClearSpreadRow(val);
                    return;

                }

            }

            // 上記処理パターン以外の場合はベースのイベントを実行
            base.OnPreviewKeyDown(e);

        }

    }

}