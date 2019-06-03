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

    /// <summary>
    /// 得意先別個建単価マスタ
    /// </summary>
    public partial class MST22010 : WindowReportBase
    {

        public class M03_YTAN4_MEMBER : INotifyPropertyChanged
        {
            public int _取引先ID { get; set; }
            public string _取引先名 { get; set; }
            public decimal _重量 { get; set; }
            public decimal _個数 { get; set; }
            public int _着地ID { get; set; }
            public string _着地名 { get; set; }
            public DateTime? _登録日時 { get; set; }
            public DateTime? _更新日時 { get; set; }
            public decimal? _個建単価 { get; set; }
            public int? _個建金額 { get; set; }
            public decimal? _d個建金額 { get; set; }
            public int? _運賃 { get; set; }
            public decimal? _d運賃 { get; set; }
            public string _S_個建単価 { get; set; }
            public string _S_個建金額 { get; set; }
            public string _S_運賃 { get; set; }
            public DateTime? _削除日付 { get; set; }


            public int 取引先ID { get { return _取引先ID; } set { _取引先ID = value; NotifyPropertyChanged(); } }
            public string 取引先名 { get { return _取引先名; } set { _取引先名 = value; NotifyPropertyChanged(); } }
            public decimal 重量 { get { return _重量; } set { _重量 = value; NotifyPropertyChanged(); } }
            public decimal 個数 { get { return _個数; } set { _個数 = value; NotifyPropertyChanged(); } }
            public int 着地ID { get { return _着地ID; } set { _着地ID = value; NotifyPropertyChanged(); } }
            public string 着地名 { get { return _着地名; } set { _着地名 = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public decimal? 個建単価 { get { return _個建単価; } set { _個建単価 = value; NotifyPropertyChanged(); } }
            public int? 個建金額 { get { return _個建金額; } set { _個建金額 = value; NotifyPropertyChanged(); } }
            public decimal? d個建金額 { get { return _d個建金額; } set { _d個建金額 = value; NotifyPropertyChanged(); } }
            public int? 運賃 { get { return _運賃; } set { _運賃 = value; NotifyPropertyChanged(); } }
            public decimal? d運賃 { get { return _d運賃; } set { _d運賃 = value; NotifyPropertyChanged(); } }
            public string S_個建単価 { get { return _S_個建単価; } set { _S_個建単価 = value; NotifyPropertyChanged(); } }
            public string S_個建金額 { get { return _S_個建金額; } set { _S_個建金額 = value; NotifyPropertyChanged(); } }
            public string S_運賃 { get { return _S_運賃; } set { _S_運賃 = value; NotifyPropertyChanged(); } }
            public DateTime? 削除日付 { get { return _削除日付; } set { _削除日付 = value; NotifyPropertyChanged(); } }


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
        //対象テーブル検索用
        private const string TargetTableNm = "M03_YTAN4_UC";
        //対象SPREAD用
        private const string TargetTableNm_Newold = "M03_YTAN4_UC_Newold";
        //対象テーブル検索用
        private const string TargetTableNm_Newold2 = "M03_YTAN4_UC_Newold2";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M03_YTAN4_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M03_YTAN4_DEL";
        //対象テーブル検索用(SPREAD)
        private const string spTargetTableNm = "spM03_YTAN4_UC";
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
            public bool 重量チェックボックス { get; set; }
            public bool 個建チェックボックス { get; set; }
            public byte[] spConfig = null;
        }
        /// ※ 必ず public で定義する。
        public ConfigMST22010 frmcfg = null;
        public byte[] spConfig = null;

        #endregion

        #region データバインド用プロパティ
        private string _得意先ID = string.Empty;
        public string 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }
        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
        }
        private string _着地ID = string.Empty;
        public string 着地ID
        {
            get { return this._着地ID; }
            set { this._着地ID = value; NotifyPropertyChanged(); }
        }
        private string _着地名 = string.Empty;
        public string 着地名
        {
            get { return this._着地名; }
            set { this._着地名 = value; NotifyPropertyChanged(); }
        }
        private decimal? _重量 = null;
        public decimal? 重量
        {
            get { return this._重量; }
            set { this._重量 = value; NotifyPropertyChanged(); }
        }
        private decimal? _個数 = null;
        public decimal? 個数
        {
            get { return this._個数; }
            set { this._個数 = value; NotifyPropertyChanged(); }
        }
        private decimal? _個建単価 = null;
        public decimal? 個建単価
        {
            get { return this._個建単価; }
            set { this._個建単価 = value; NotifyPropertyChanged(); }
        }
        private int? _個建金額;
        public int? 個建金額
        {
            get { return this._個建金額; }
            set { this._個建金額 = value; NotifyPropertyChanged(); }
        }

        private int? _運賃;
        public int? 運賃
        {
            get { return this._運賃; }
            set { this._運賃 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private string _取引先ID = string.Empty;
        public string 取引先ID
        {
            get { return this._取引先ID; }
            set { this._取引先ID = value; NotifyPropertyChanged(); }
        }

        private bool _重量チェックボックス値 = true;
        public bool 重量チェックボックス値
        {
            get { return this._重量チェックボックス値; }
            set { this._重量チェックボックス値 = value; NotifyPropertyChanged(); }
        }

        private bool _個数チェックボックス値 = true;
        public bool 個数チェックボックス値
        {
            get { return this._個数チェックボックス値; }
            set { this._個数チェックボックス値 = value; NotifyPropertyChanged(); }
        }
        
        //スプレッドバインド変数
        private List<M03_YTAN4_MEMBER> _KodateKingaku;
        public List<M03_YTAN4_MEMBER> KodateKingaku
        {
            get
            {
                return this._KodateKingaku;
            }
            set
            {
                this._KodateKingaku = value;
                this.spKodate.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        //表示用マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set
            {
                this._MstData = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region MST22010
        /// <summary>
        /// 得意先別個建単価マスタ
        /// </summary>
        public MST22010()
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
            //初期状態記憶
            this.spConfig = AppCommon.SaveSpConfig(this.spKodate);
            //初期化
            ScreenClear();

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            //着地ID用
            base.MasterMaintenanceWindowList.Add("M08_TIK_UC", new List<Type> { typeof(MST03010), typeof(SCH03010) });
            //データ取得

            base.SendRequest(new CommunicationObject(MessageType.RequestData, spTargetTableNm, new object[] { }));

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

            if (frmcfg.spConfig != null)
            {
                AppCommon.LoadSpConfig(this.spKodate, frmcfg.spConfig);
            }

			spKodate.InputBindings.Add(new KeyBinding(spKodate.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

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
                    //マスター登録、マスター情報検索時のデータをSPREADに表示。
                    case TargetTableNm:
                        //SPREADシート追加
                        // datatableをlistへ変換
                        KodateKingaku = (List<M03_YTAN4_MEMBER>)AppCommon.ConvertFromDataTable(typeof(List<M03_YTAN4_MEMBER>), tbl);
                        break;

                    case TargetTableNm_Newold:
                        // datatableをlistへ変換
                        KodateKingaku = (List<M03_YTAN4_MEMBER>)AppCommon.ConvertFromDataTable(typeof(List<M03_YTAN4_MEMBER>), tbl);
                        break;

                    case TargetTableNm_Newold2:
                        strData(tbl);
                        break;

                    //sp全件表示
                    case spTargetTableNm:
                        // datatableをlistへ変換
                        KodateKingaku = (List<M03_YTAN4_MEMBER>)AppCommon.ConvertFromDataTable(typeof(List<M03_YTAN4_MEMBER>), tbl);
                        break;

                    case TargetTableNmDelete:
                        ScreenClear();
                        break;
                    case TargetTableNmUpdate:
                        ScreenClear();
                        break;

                    default:
                        //ScreenClear();
                        //KodateKingaku = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void strData(DataTable tbl)
        {
            if (tbl.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                {
                    this.ErrorMessage = "既に削除されているデータです。";
                    MessageBox.Show("既に削除されたデータです。");
                    KodateKingaku = null;
                    ScreenClear();
                    return;
                }

                MstData = tbl.Rows[0];
                得意先ID = tbl.Rows[0]["取引先ID"].ToString();
                着地ID = tbl.Rows[0]["着地ID"].ToString();
                重量 = Convert.ToDecimal(tbl.Rows[0]["重量"]);
                個数 = Convert.ToDecimal(tbl.Rows[0]["個数"]);
                個建単価 = Convert.ToDecimal(tbl.Rows[0]["個建単価"]);
                個建金額 = Convert.ToInt32(tbl.Rows[0]["個建金額"]);

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                //編集モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }
            else
            {
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                //新規作成モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
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
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F2 マスタメンテ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテ画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F8 リボン　リスト一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST22020 mst22020 = new MST22020();
            mst22020.ShowDialog(this);
        }

        /// <summary>
        /// F9　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
        }



        /// <summary>
        /// F10　リボン入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            //メッセージボックス
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            //OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
                KodateKingaku = null;
            }
        }

        /// <summary>
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            AppCommon.LoadSpConfig(this.spKodate, this.spConfig);
            this.Close();
        }

        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            try
            {
                if (MstData == null)
                {
                    this.ErrorMessage = "登録内容がありません。";
                    MessageBox.Show("登録内容がありません。");
                    return;
                }

                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                {
                    this.ErrorMessage = "新規登録データは削除できません。";
                    MessageBox.Show("新規登録データは削除できません。");
                    SetFocusToTopControl();
                    return;
                }

                int iTokuisakiId = 0;
                int iTyakutiId = 0;
                if (int.TryParse(得意先ID, out iTokuisakiId) && int.TryParse(着地ID, out iTyakutiId))
                {

                    //メッセージボックス
                    MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                                    , "確認"
                                    , MessageBoxButton.YesNo
                                    , MessageBoxImage.Question
                                    , MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { iTokuisakiId, iTyakutiId, 重量, 個数 }));
                    }

                }
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
			spKodate.InputBindings.Clear();
			KodateKingaku = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 処理メソッド
        private void ScreenClear()
        {
            MstData = null;
            得意先ID = string.Empty;
            重量 = null;
            個数 = null;
            着地ID = string.Empty;
            個建単価 = null;
            個建金額 = null;
            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタン常時Enabled
            btnserch.IsEnabled = true;
            //重量チェックボックス常時Enabled
            JyuryoCheck.IsEnabled = true;
            //個数チェックボックス常時Enabled
            KosuuCheck.IsEnabled = true;
            ResetAllValidation();
            SetFocusToTopControl();
        }

        public void Update()
        {
            try
            {
                int iTokuisakiId = 0;
                int iTyakutiId = 0;

                if (個建金額 == null)
                {
                    個建金額 = 0;
                }

                if (string.IsNullOrEmpty(得意先ID))
                {
                    this.ErrorMessage = "得意先IDは必須入力項目です。";
                    MessageBox.Show("得意先IDは必須入力項目です。");
                    return;
                }
                if (string.IsNullOrEmpty(着地ID))
                {
                    this.ErrorMessage = "着地IDは必須入力項目です。";
                    MessageBox.Show("着地IDは必須入力項目です。");
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                decimal? 運賃 = (個建単価 * 個数) + 個建金額;
                string 運賃桁数 = Math.Floor((decimal)運賃).ToString();
                int 運賃Count = 運賃桁数.Length;
                if (運賃Count > 9)
                {
                    this.ErrorMessage = "入力された内容では登録できませんでした";
                    MessageBox.Show("入力された内容では登録できませんでした。\r\n一度入力内容の単価・個数・金額をお確かめ下さい。");
                    return;
                }

                if (int.TryParse(得意先ID, out iTokuisakiId) && int.TryParse(着地ID, out iTyakutiId))
                {

                    var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.Yes)
                    {
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { iTokuisakiId, iTyakutiId, 重量, 個数, 個建単価, 個建金額 }));
                    }
                    else
                    {
                        this.SetFocusToTopControl();
                    }
                }
            }
            catch
            {
                //更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";
                return;
            }
        }




        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (KodateTanka.Text == string.Empty)
                {
                    個建単価 = 0;
                }
                else
                {
                    decimal Decit;
                    if (decimal.TryParse(KodateTanka.Text, out Decit) == true)
                    {
                        個建単価 = Decit;
                    }
                }
            }
        }


        /// <summary>
        /// 登録処理
        /// </summary>
        private void Update_UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }


        private void 個建単価M_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            var gcsp = (sender as GcSpreadGrid);
            if (gcsp == null)
            {
                return;
            }

            try
            {
                int val_1;
                int val_2;
                decimal? val_3;
                decimal? val_4;
                decimal? val_5;
                int? val_6;

                var row = gcsp.Rows[e.CellPosition.Row];
                val_1 = (int)row.Cells[gcsp.Columns["取引先ID"].Index].Value;
                val_2 = (int)row.Cells[gcsp.Columns["着地ID"].Index].Value;
                val_3 = (decimal?)row.Cells[gcsp.Columns["重量"].Index].Value;
                val_4 = (decimal?)row.Cells[gcsp.Columns["個数"].Index].Value;
                val_5 = (decimal?)row.Cells[gcsp.Columns["個建単価"].Index].Value;
                val_6 = row.Cells[gcsp.Columns["d個建金額"].Index].Value == null ? null : (int?)AppCommon.IntParse(row.Cells[gcsp.Columns["d個建金額"].Index].Value.ToString());
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { val_1, val_2, val_3, val_4, val_5, val_6 }));
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "入力内容が不正です。";
            }
        }


        //検索ボタン押下時
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //項目クリア
            this.ScreenClear();
            //SPREADロード
            base.SendRequest(new CommunicationObject(MessageType.RequestData, spTargetTableNm, new object[] { }));
        }


        //得意先ID　検索時イベント
        //重量、個数チェックボックス：trueが検索対象
        private void Tokuisaki_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int? i得意先ID = null;
                    int? i着地ID = null;

                    //得意先ID入力時
                    if (!string.IsNullOrEmpty(得意先ID))
                    {
                        i得意先ID = AppCommon.IntParse(得意先ID);
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i得意先ID, 重量, 個数, i着地ID, 重量チェックボックス値, 個数チェックボックス値 }));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
                return;
            }

        }

        //着地ID　検索時イベント
        private void Tyakuchi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int? i得意先ID = null;
                    int? i着地ID = null;

                    //着地ID入力時
                    if (!string.IsNullOrEmpty(着地ID))
                    {
                        i得意先ID = AppCommon.IntParse(得意先ID);
                        i着地ID = AppCommon.IntParse(着地ID);
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i得意先ID, 重量, 個数, i着地ID, 重量チェックボックス値, 個数チェックボックス値 }));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
                return;
            }
        }

        //重量　検索時イベント
        private void Jyuryo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int? i得意先ID = null;
                int? i着地ID = null;

                //重量入力時
                if (LabelTextOmosa.Text == string.Empty)
                {
                    重量 = 0;
                }
                else
                {
                    try
                    {
                        decimal Decit;
                        if (decimal.TryParse(LabelTextOmosa.Text, out Decit) == true)
                        {
                            重量 = Decit;
                        }

                        i得意先ID = AppCommon.IntParse(得意先ID);
                        i着地ID = AppCommon.IntParse(着地ID);
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i得意先ID, 重量, 個数, i着地ID, 重量チェックボックス値, 個数チェックボックス値 }));
                    }
                    catch (Exception ex)
                    {
                        appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                        this.ErrorMessage = ex.Message;
                        return;
                    }
                }
            }
        }

        //個数　検索時イベント
        private void Kosuu_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int? i得意先ID = null;
                int? i着地ID = null;

                //個数入力時
                if (LabelTextkosuu.Text == string.Empty)
                {
                    個数 = 0;
                }
                else
                {
                    decimal Decit;
                    if (decimal.TryParse(LabelTextkosuu.Text, out Decit) == true)
                    {
                        個数 = Decit;
                    }
                }

                if (!(LabelTextTokuisaki.CheckValidation() && LabelTextTyakuti.CheckValidation()))
                {
                    return;
                }

                try
                {
                    i得意先ID = AppCommon.IntParse(得意先ID);
                    i着地ID = AppCommon.IntParse(着地ID);
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm_Newold, new object[] { i得意先ID, 重量, 個数, i着地ID, 重量チェックボックス値, 個数チェックボックス値 }));
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm_Newold2, new object[] { i得意先ID, 重量, 個数, i着地ID }));
                }
                catch (Exception ex)
                {
                    appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    this.ErrorMessage = ex.Message;
                    return;
                }
            }
        }
    }
        #endregion
}