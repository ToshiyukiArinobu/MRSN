using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{

    /// <summary>
    /// 担当者権限マスタ入力
    /// </summary>
    public partial class MST90100 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string 権限ID新規取得 = "M74_GET_NEW_KGRPID";
        private const string 権限マスタデータ取得 = "M74_AUTHORITY_SEL";
        private const string 権限マスタデータ更新 = "M74_AUTHORITY_UPD";
        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigMST90100 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST90100 frmcfg = null;

        //ログイン情報
        CommonConfig ccfg = null;

        #endregion

        #region メニュー項目
        Dictionary<string, ControlChildren> MenuList = new Dictionary<string, ControlChildren>();

        class ControlChildren
        {
            public string[] TabID;
            public string[] ProgramID;
            public int[] No;
            public string[] Name;
        }
        #endregion

        #region バインド用変数(ローカル)
        private DataTable _権限マスタデータ;
        public DataTable 権限マスタデータ
        {
            get { return this._権限マスタデータ; }
            set { this._権限マスタデータ = value; NotifyPropertyChanged(); }
        }

        private int? _IN_グループ権限ID = null;
        public int? IN_グループ権限ID
        {
            get { return this._IN_グループ権限ID; }
            set { this._IN_グループ権限ID = value; NotifyPropertyChanged(); }
        }

        private string _IN_グループ権限名 = null;
        public string IN_グループ権限名
        {
            get { return this._IN_グループ権限名; }
            set { this._IN_グループ権限名 = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region << 画面表示初期処理 >>

        /// <summary>
        /// 権限マスタ入力 コンストラクタ
        /// </summary>
        public MST90100()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Load(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigMST90100)ucfg.GetConfigValue(typeof(ConfigMST90100));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST90100();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion
            
            //メニューオブジェクト取得処理
            MAINMANU menu = new MAINMANU();

            //タブコントロール（ループ出来ないので単独）
            if (menu.Tab1.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab1);
            if (menu.Tab2.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab2);
            if (menu.Tab3.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab3);
            if (menu.Tab4.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab4);
            if (menu.Tab5.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab5);
            if (menu.Tab6.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab6);
            if (menu.Tab7.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab7);
            if (menu.Tab8.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab8);
            if (menu.Tab9.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab9);
            if (menu.Tab10.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab10);
            if (menu.Tab11.Visibility != System.Windows.Visibility.Hidden)
                PrintLogicalChildren(menu.Tab11);

            ScreenClear();

            base.MasterMaintenanceWindowList.Add("M74_AUTHORITY_NAME", new List<Type> { null, typeof(SCHM74_KGRPNAME) });

        }

        #endregion

        #region 画面オブジェクト取得
        public class WpfUtil
        {
            /// <summary>
            /// targetの論理ツリー上の子要素全てに対してactionを実行します。
            /// actionはtarget自身にも作用する。再帰処理なのでスタックフレームに注意。
            /// </summary>
            /// <param name="target">ルートとするオブジェクト</param>
            /// <param name="action">実行するメソッドのデリゲート</param>
            public static void OperateLogicalChildren(DependencyObject target, Action<DependencyObject> action)
            {
                action(target);
                foreach (var child in LogicalTreeHelper.GetChildren(target))
                {
                    if (child is DependencyObject)
                    {
                        OperateLogicalChildren((DependencyObject)child, action);
                    }
                }
            }
        }

        /// <summary>
        /// target以下の論理ツリー上の子要素をコンソール出力。階層分インデントする。
        /// 要素がControlでNameを持っていた場合は併せて出力。
        /// </summary>
        private void PrintLogicalChildren(DependencyObject target)
        {
            //代入用変数定義
            ControlChildren SetVal = new ControlChildren();
            var TabId = string.Empty;
            var MenuName = string.Empty;
            var TabID = new List<string>();
            var ProgramID = new List<string>();
            var No = new List<int>();
            var Name = new List<string>();

            WpfUtil.OperateLogicalChildren(target, t =>
            {
                String contName = "";
                Control cont = t as Control;
                if (cont != null && cont.Name.Length > 0)
                {
                    if (cont.GetType() == typeof(Button))
                    {
                        // 非表示のボタンは表示対象外とする
                        if (cont.Visibility != System.Windows.Visibility.Hidden)
                        {
                            // タブID
                            TabID.Add(TabId);
                            // プログラムID
                            ProgramID.Add(cont.Name);
                            // メニュー番号
                            int SubNo = Int32.Parse(Convert.ToString(((Button)(cont)).Content).Substring(0, Convert.ToString(((Button)(cont)).Content).IndexOf(".")));
                            No.Add(SubNo);
                            // メニュー名称
                            Name.Add(Convert.ToString(((Button)(cont)).Content).Substring(Convert.ToString(((Button)(cont)).Content).IndexOf(".") + 1));

                        }

                    }
                    else if (cont.GetType() == typeof(TabItem))
                    {
                        MenuName = Convert.ToString(((HeaderedContentControl)(cont)).Header);
                        TabId = cont.Name;
                    }
                }

                if(!contName.Equals(string.Empty)) { Console.WriteLine(contName);}
            }
            );

            //メニュ項目設定
            SetVal.TabID = TabID.ToArray();
            SetVal.ProgramID = ProgramID.ToArray();
            SetVal.No = No.ToArray();
            SetVal.Name = Name.ToArray();
            MenuList.Add(MenuName, SetVal);
        }

        /// <summary>
        /// メニューオブジェクトのソート後DataTable作成
        /// </summary>
        /// <returns></returns>
        private DataTable DispDataSet(DataTable DbTable)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("権限マスタデータ");
            // 列を追加
            dt.Columns.Add("TabID", typeof(int));
            dt.Columns.Add("メニュー名称", typeof(string));
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("プログラム名称", typeof(string));
            dt.Columns.Add("グループ権限ID", typeof(int));
            dt.Columns.Add("プログラムID", typeof(string));
            dt.Columns.Add("使用可能FLG", typeof(Boolean));
            dt.Columns.Add("データ更新FLG", typeof(Boolean));
            dt.Columns.Add("登録日時", typeof(DateTime));
            dt.Columns.Add("更新日時", typeof(DateTime));
            dt.Columns.Add("削除日付", typeof(DateTime));
            dt.Columns["削除日付"].AllowDBNull = true;

            ds.Tables.Add(dt);

            // メニュー項目設定
            foreach (KeyValuePair<string, ControlChildren> DicList in MenuList)
            {
                var MenuName = DicList.Key;

                ControlChildren SelData = DicList.Value;

                for (int Cnt = 0; Cnt < SelData.No.Length; Cnt++)
                {
                    DataRow dr = ds.Tables["権限マスタデータ"].NewRow();
                    dr["TabID"] = SelData.TabID[Cnt].Replace("Tab", "");
                    dr["メニュー名称"] = MenuName;
                    dr["No"] = SelData.No[Cnt];
                    dr["プログラム名称"] = SelData.Name[Cnt];
                    dr["グループ権限ID"] = IN_グループ権限ID;
                    dr["プログラムID"] = SelData.ProgramID[Cnt].Trim();
                    dr["使用可能FLG"] = true;
                    dr["データ更新FLG"] = true;
                    dr["登録日時"] = DateTime.Today;
                    dr["更新日時"] = DateTime.Today;
                    dr["削除日付"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
            }

            DataTable table = dt.Clone();
            DataRow[] rows = dt.Select(null, "TabID,No ASC");
            foreach (DataRow row in rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                // DataTableに格納
                table.Rows.Add(addRow);
            }

            // データマージ
            rows = DbTable.Select();
            foreach (DataRow row in rows)
            {
                try
                {
                    DataRow[] SelectRow = table.Select("プログラムID = '" + Convert.ToString(row["プログラムID"]).Trim() + "'" );
                    if (SelectRow.Length > 0)
                    {
                        foreach(DataRow dr in SelectRow){
                            dr["使用可能FLG"] = row["使用可能FLG"];
                            dr["データ更新FLG"] = row["データ更新FLG"];
                            dr["登録日時"] = row["登録日時"];
                            dr["更新日時"] = row["更新日時"];
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
            
            return table;
        }
        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            // 初期表示
            IN_グループ権限ID = null;

            this.MaintenanceMode = string.Empty;

            sp権限マスタデータ.FilterDescriptions.Clear();
            権限マスタデータ = null;
            sp権限マスタデータ.RowCount = 0;

            // キーのみtrue
            ChangeKeyItemChangeable(true);
            BtnNewID.IsEnabled = true;

            ResetAllValidation();

            SetFocusToTopControl();

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
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F9 データ登録
        /// <summary>
        /// F9　リボン　データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
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
            if (sp権限マスタデータ.RowCount > 0)
            {
                MessageBoxResult result =
                    MessageBox.Show("保存せずに入力を取り消してよろしいですか？",
                        "確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                    ScreenClear();

            }

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
            this.Close();
        }
        #endregion

        #endregion

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region << 便利リンク >>

        /// <summary>
        /// リボン便利リンク　検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kensaku_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.yahoo.co.jp/");
        }

        /// <summary>
        /// リボン便利リンク　道路情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jartic.or.jp/");

        }

        /// <summary>
        /// リボン便利リンク　道路ナビボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroNabi_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://highway.drivenavi.net/");
        }

        /// <summary>
        /// リボン便利リンク　渋滞情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JyuutaiJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.mapfan.com/");
        }

        /// <summary>
        /// リボン便利リンク　天気ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tenki_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://weathernews.jp/");
        }

        /// <summary>
        /// リボン　WebHomeボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_WebHome_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C:\\Program Files (x86)/Internet Explorer/iexplore.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　メールボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Meil_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Program Files (x86)//Windows Live//Mail//wlmail.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　電卓ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Dentaku_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Windows//System32/calc.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region << 受信系処理 >>
        /// <summary>
        /// データ受信メソッド
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
                    case 権限ID新規取得:
                        IN_グループ権限ID = (int)data;
                        break;

                    // 権限マスタデータ取得
                    case 権限マスタデータ取得:
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            SetTblData(tbl);
                            // 編集ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        }
                        else
                        {
                            // 新規ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        }

                        // 取得データ一覧表示
                        権限マスタデータ = DispDataSet(tbl);

                        // キーをfalse
                        ChangeKeyItemChangeable(false);
                        BtnNewID.IsEnabled = false;

                        SetFocusToTopControl();
                        break;

                    case 権限マスタデータ更新:
                        MessageBox.Show("データを更新しました。");
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

        /// <summary>
        /// データエラー受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region SetTblData

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            IN_グループ権限ID = AppCommon.IntParse(tbl.Rows[0]["グループ権限ID"].ToString());
        }

        #endregion

        #region 権限情報検索

        /// <summary>
        /// グループ権限IDコードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
					if (IN_グループ権限ID == null || IN_グループ権限ID == 0)
						return;

                    try
                    {
                        // 権限マスタデータ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, 権限マスタデータ取得, new object[] { IN_グループ権限ID, 0 })); 

                    }
                    catch (Exception)
                    {
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
            }

        }
        #endregion

        #region 登録

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {
                if (sp権限マスタデータ.RowCount > 0)
                {
                    if (string.IsNullOrEmpty(IN_グループ権限名))
                    {
                        txbGrpName.Focus();
                        MessageBox.Show("グループ権限名を入力してください。", "登録確認", MessageBoxButton.OK, MessageBoxImage.Question);
                        return;
                    }

                    if (IN_グループ権限ID == 0)
                    {
                        txbGrpName.Focus();
                        MessageBox.Show("システム管理者は権限は変更が出来ません。", "登録確認", MessageBoxButton.OK, MessageBoxImage.Question);
                        return;
                    }

                    var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.Yes)
                    {
                        SendRequest(
                            new CommunicationObject(
                                MessageType.UpdateData,
                                権限マスタデータ更新,
                                new object[] {
                                    権限マスタデータ,
                                    IN_グループ権限ID,
                                    IN_グループ権限名
                                }));
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("一覧にデータが表示されていません。", "登録確認", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました";
                return;
            }

        }

        /*
        /// <summary>
        /// ConvertToList
        /// デバッグ用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                        pro.SetValue(objT, row[pro.Name]);
                }

                return objT;
            }).ToList();

        }
         */ 
        #endregion

        /// <summary>
        /// グループ権限ID入力制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void グループ権限ID_TextInput(object sender, TextCompositionEventArgs e)
        {
            // 0-9のみ
            e.Handled = !new Regex("[1-9]").IsMatch(e.Text);
        }

/*
        /// <summary>
        /// 全件使用可能ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllOkButoon_Click(object sender, RoutedEventArgs e)
        {
            for (var Cnt = 0; Cnt < sp権限マスタデータ.RowCount; Cnt++)
            {
                if (sp権限マスタデータ.Rows[Cnt].IsVisible)
                    sp権限マスタデータ.Cells[Cnt, "使用可能FLG"].Value = true;

            }

        }

        /// <summary>
        /// 全件使用不可ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllNGButoon_Click(object sender, RoutedEventArgs e)
        {
            for (var Cnt = 0; Cnt < sp権限マスタデータ.RowCount; Cnt++)
            {
                if (sp権限マスタデータ.Rows[Cnt].IsVisible)
                    sp権限マスタデータ.Cells[Cnt, "使用可能FLG"].Value = false;

            }

        }
*/

        /// <summary>
        /// チェック状態変更ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            string targetColumnName = btn.Name.Equals(EnableAllChange.Name) ? "使用可能FLG" : "データ更新FLG";
            int targetColumnIdx = sp権限マスタデータ.Columns[targetColumnName].Index;

            bool isVisible = true;

            foreach (var row in sp権限マスタデータ.Rows)
            {
                var cell = row.Cells[targetColumnIdx];

                if (row.Index == 0)
                    // 設定基準として先頭データの選択状態を取得
                    isVisible = cell.Value is bool ? (bool)cell.Value : false;

                if (row.IsVisible && ((bool)cell.Value) == isVisible)
                    // 現在の選択状態を反転させて設定
                    cell.Value = !isVisible;

            }

        }

        /// <summary>
        /// 新規権限ID取得ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewID_Click(object sender, RoutedEventArgs e)
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    権限ID新規取得,
                    new object[] { }));

        }

    }

}
