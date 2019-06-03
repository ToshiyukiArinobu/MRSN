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

using GrapeCity.Windows.SpreadGrid;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 燃料単価マスタ入力
    /// </summary>
    public partial class JMI11010 : WindowMasterMainteBase
    {
        // データメンバー定義
        public class S13_DRVSB_Member : INotifyPropertyChanged
        {
            public int _乗務員KEY { get; set; }
            public int _集計年月 { get; set; }
            public int _経費項目ID { get; set; }
            public DateTime? _登録日時 { get; set; }
            public DateTime? _更新日時 { get; set; }
            public string _経費項目名 { get; set; }
            public int? _固定変動区分 { get; set; }
            public decimal _金額 { get; set; }
            public int? _経費区分 { get; set; }



            public int 乗務員KEY { get { return _乗務員KEY; } set { _乗務員KEY = value; NotifyPropertyChanged(); } }
            public int 集計年月 { get { return _集計年月; } set { _集計年月 = value; NotifyPropertyChanged(); } }
            public int 経費項目ID { get { return _経費項目ID; } set { _経費項目ID = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public string 経費項目名 { get { return _経費項目名; } set { _経費項目名 = value; NotifyPropertyChanged(); } }
            public int? 固定変動区分 { get { return _固定変動区分; } set { _固定変動区分 = value; NotifyPropertyChanged(); } }
            public decimal 金額 { get { return _金額; } set { _金額 = value; NotifyPropertyChanged(); } }
            public int? 経費区分 { get { return _経費区分; } set { _経費区分 = value; NotifyPropertyChanged(); } }


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



        #region 定数定義
        private const string TargetTableNm = "S13_DRV";
        private const string UpdateTable = "S13_DRV_UP";
        private const string DeleteTable = "S13_DRV_DEL";
        //変動費
        private const string S11TableNm_Hendo = "S13_DRVSB_Hendo";
        private const string S11UpdateTable_Hendo = "S13_DRVSB_UP_Hendo";
        private const string S11DeleteTable_Hendo = "S13_DRVSB_DEL_Hendo";
        //人件費
        private const string S11TableNm_Jinken = "S13_DRVSB_Jinken";
        private const string S11UpdateTable_Jinken = "S13_DRVSB_UP_Jinken";
        private const string S11DeleteTable_Jinken = "S13_DRVSB_DEL_Jinken";
        //固定費
        private const string S11TableNm_Kotei = "S13_DRVSB_Kotei";
        private const string S11UpdateTable_Kotei = "S13_DRVSB_UP_Kotei";
        private const string S11DeleteTable_Kotei = "S13_DRVSB_DEL_Kotei";

        private const string M04_DRVTableNm = "M04_DRV";
        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigJMI11010 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigJMI11010 frmcfg = null;

        #endregion


        #region バインド用変数


        private DataRow _rowS01;
        public DataRow rowS01
        {
            get { return this._rowS01; }
            set
            {
                try
                {
                    this._rowS01 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS01Data = new DataTable();
        public DataTable rowS01Data
        {
            get { return this._rowS01Data; }
            set
            {
                this._rowS01Data = value;
                if (value == null)
                {
                    this.rowS01 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS01 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS01 = value.NewRow();
                        value.Rows.Add(this.rowS01);
                    }
                }
                NotifyPropertyChanged();
            }
        }


        private DataRow _rowS11;
        public DataRow rowS11
        {
            get { return this._rowS11; }
            set
            {
                try
                {
                    this._rowS11 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS11Data = new DataTable();
        public DataTable rowS11Data
        {
            get { return this._rowS11Data; }
            set
            {
                this._rowS11Data = value;
                if (value == null)
                {
                    this.rowS11 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS11 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS11 = value.NewRow();
                        value.Rows.Add(this.rowS11);
                    }
                }
                NotifyPropertyChanged();
            }
        }

        //変動費
        //private DataRow _rowHendo;
        //public DataRow rowHendo
        //{
        //    get { return this._rowHendo; }
        //    set
        //    {
        //        try
        //        {
        //            this._rowHendo = value;
        //            NotifyPropertyChanged();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        //private DataTable _rowHendoData = new DataTable();
        //public DataTable rowHendoData
        //{
        //    get { return this._rowHendoData; }
        //    set
        //    {
        //        this._rowHendoData = value;
        //        if (value == null)
        //        {
        //            this.rowHendo = null;
        //        }
        //        else
        //        {
        //            if (value.Rows.Count > 0)
        //            {
        //                this.rowHendo = value.Rows[0];
        //            }
        //            else
        //            {
        //                this.rowHendo = value.NewRow();
        //                value.Rows.Add(this.rowHendo);
        //            }
        //        }
        //        NotifyPropertyChanged();
        //    }
        //}

        private List<S13_DRVSB_Member> _rowHendoData;
        public List<S13_DRVSB_Member> rowHendoData
        {
            //get { return this._dUriageData2; }
            //set { this._dUriageData2 = value; NotifyPropertyChanged(); }
            get
            {
                return this._rowHendoData;
            }
            set
            {
                this._rowHendoData = value;
                this.sp変動費.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }



        //人件費
        //private DataRow _rowJinken;
        //public DataRow rowJinken
        //{
        //    get { return this._rowJinken; }
        //    set
        //    {
        //        try
        //        {
        //            this._rowJinken = value;
        //            NotifyPropertyChanged();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        //private DataTable _rowJinkenData = new DataTable();
        //public DataTable rowJinkenData
        //{
        //    get { return this._rowJinkenData; }
        //    set
        //    {
        //        this._rowJinkenData = value;
        //        if (value == null)
        //        {
        //            this.rowJinken = null;
        //        }
        //        else
        //        {
        //            if (value.Rows.Count > 0)
        //            {
        //                this.rowJinken = value.Rows[0];
        //            }
        //            else
        //            {
        //                this.rowJinken = value.NewRow();
        //                value.Rows.Add(this.rowJinken);
        //            }
        //        }
        //        NotifyPropertyChanged();
        //    }
        //}

        private List<S13_DRVSB_Member> _rowJinkenData;
        public List<S13_DRVSB_Member> rowJinkenData
        {
            //get { return this._dUriageData2; }
            //set { this._dUriageData2 = value; NotifyPropertyChanged(); }
            get
            {
                return this._rowJinkenData;
            }
            set
            {
                this._rowJinkenData = value;
                this.sp人件費.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        //固定費
        //private DataRow _rowKotei;
        //public DataRow rowKotei
        //{
        //    get { return this._rowKotei; }
        //    set
        //    {
        //        try
        //        {
        //            this._rowKotei = value;
        //            NotifyPropertyChanged();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        //private DataTable _rowKoteiData = new DataTable();
        //public DataTable rowKoteiData
        //{
        //    get { return this._rowKoteiData; }
        //    set
        //    {
        //        this._rowKoteiData = value;
        //        if (value == null)
        //        {
        //            this.rowKotei = null;
        //        }
        //        else
        //        {
        //            if (value.Rows.Count > 0)
        //            {
        //                this.rowKotei = value.Rows[0];
        //            }
        //            else
        //            {
        //                this.rowKotei = value.NewRow();
        //                value.Rows.Add(this.rowKotei);
        //            }
        //        }
        //        NotifyPropertyChanged();
        //    }
        //}

        private List<S13_DRVSB_Member> _rowKoteiData;
        public List<S13_DRVSB_Member> rowKoteiData
        {
            //get { return this._dUriageData2; }
            //set { this._dUriageData2 = value; NotifyPropertyChanged(); }
            get
            {
                return this._rowKoteiData;
            }
            set
            {
                this._rowKoteiData = value;
                this.sp固定経.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }


        private decimal? _小計B = null;
        public decimal? 小計B
        {
            get { return this._小計B; }
            set { this._小計B = value; NotifyPropertyChanged(); }
        }
        private decimal? _小計C = null;
        public decimal? 小計C
        {
            get { return this._小計C; }
            set { this._小計C = value; NotifyPropertyChanged(); }
        }
        private decimal? _小計D = null;
        public decimal? 小計D
        {
            get { return this._小計D; }
            set { this._小計D = value; NotifyPropertyChanged(); }
        }

        private int? _乗務員KEY = null;
        public int? 乗務員KEY
        {
            get { return this._乗務員KEY; }
            set { this._乗務員KEY = value; NotifyPropertyChanged(); }
        }
        private int? _乗務員ID = null;
        public int? 乗務員ID
        {
            get { return this._乗務員ID; }
            set { this._乗務員ID = value; NotifyPropertyChanged(); }
        }

        private DateTime? _処理年月 = null;
        public DateTime? 処理年月
        {
            get { return this._処理年月; }
            set { this._処理年月 = value; NotifyPropertyChanged(); }
        }

        private decimal? _限界利益 = null;
        public decimal? 限界利益
        {
            get { return this._限界利益; }
            set { this._限界利益 = value; NotifyPropertyChanged(); }
        }

        private decimal? _乗務員直接費合計 = null;
        public decimal? 乗務員直接費合計
        {
            get { return this._乗務員直接費合計; }
            set { this._乗務員直接費合計 = value; NotifyPropertyChanged(); }
        }

        private decimal? _直接利益 = null;
        public decimal? 直接利益
        {
            get { return this._直接利益; }
            set { this._直接利益 = value; NotifyPropertyChanged(); }
        }

        private decimal? _当月利益 = null;
        public decimal? 当月利益
        {
            get { return this._当月利益; }
            set { this._当月利益 = value; NotifyPropertyChanged(); }
        }
        private decimal? _当月利益率 = null;
        public decimal? 当月利益率
        {
            get { return this._当月利益率; }
            set { this._当月利益率 = value; NotifyPropertyChanged(); }
        }
        private decimal? _空車ＫＭ = null;
        public decimal? 空車ＫＭ
        {
            get { return this._空車ＫＭ; }
            set { this._空車ＫＭ = value; NotifyPropertyChanged(); }
        }

        private decimal? _燃費 = null;
        public decimal? 燃費
        {
            get { return this._燃費; }
            set { this._燃費 = value; NotifyPropertyChanged(); }
        }

        private int? _収入 = null;
        public int? 収入
        {
            get { return this._収入; }
            set { this._収入 = value; NotifyPropertyChanged(); }
        }

        private int? _輸送原価 = null;
        public int? 輸送原価
        {
            get { return this._輸送原価; }
            set { this._輸送原価 = value; NotifyPropertyChanged(); }
        }

        int iChkFlg = 0;
        
        #endregion

        /// <summary>
        /// 燃料単価マスタ入力
        /// </summary>
        public JMI11010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
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
            #endregion
            frmcfg = (ConfigJMI11010)ucfg.GetConfigValue(typeof(ConfigJMI11010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigJMI11010();
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
            //画面サイズをタスクバーをのぞいた状態で表示させる
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;


            ChangeKeyItemChangeable(true);
            SetFocusToTopControl();

            ScreenClear();

            //乗務員ID用
            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { typeof(MST04010), typeof(SCH04010) });
            //base.MasterMaintenanceWindowList.Add("S13_DRV", new List<Type> { null, typeof(SCH14010) });


        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            rowS01 = null;
            rowS11 = null;

            this.sp変動費.ItemsSource = null;
            this.sp人件費.ItemsSource = null;
            this.sp固定経.ItemsSource = null;

            限界利益 = 0;
            乗務員直接費合計 = 0;
            直接利益 = 0;
            当月利益 = 0;
            当月利益率 = 0;
            空車ＫＭ = 0;
            燃費 = 0;
            収入 = 0;
            輸送原価 = 0;

            小計B = 0;
            小計C = 0;
            小計D = 0;
            限界利益 = 0;
            乗務員直接費合計 = 0;
            直接利益 = 0;
            当月利益 = 0;
            当月利益率 = 0;
            空車ＫＭ = 0;
            燃費 = 0;
            収入 = 0;
            輸送原価 = 0;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            SetFocusToTopControl();

            this.ErrorMessage = string.Empty;
            ResetAllValidation();
        }

        #region 計算処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void Keisan()
        {
            if (rowS01 == null || rowHendoData == null || rowJinkenData == null || rowKoteiData == null)
            {
                return;
            }

            //System.Windows.Forms.Application.DoEvents();

            小計B = 0;
            小計C = 0;
            小計D = 0;

            decimal dJyosu;

            小計B = rowHendoData.Select(q => q.金額).Sum();
            小計C = rowJinkenData.Select(q => q.金額).Sum();
            小計D = rowKoteiData.Select(q => q.金額).Sum();
            //小計D = rowKoteiData.Compute("sum(金額)", null) == null ? 0 : AppCommon.IntParse(rowKoteiData.Compute("sum(金額)", null).ToString());

            限界利益 = AppCommon.IntParse((rowS01["運送収入"] == null ? 0 : rowS01["運送収入"]).ToString()) - 小計B;
            乗務員直接費合計 = 小計C + 小計D;
            直接利益 = 限界利益 - 乗務員直接費合計;
            当月利益 = 直接利益 - AppCommon.IntParse((rowS01["一般管理費"] == null ? 0 : rowS01["一般管理費"]).ToString());

            decimal.TryParse(rowS01["運送収入"].ToString(), out dJyosu);
            if (dJyosu != 0)
            {
                当月利益率 = Math.Round(AppCommon.DecimalParse((当月利益 * 100).ToString()) / dJyosu, 1, MidpointRounding.AwayFromZero);
            }
            else
            {
                当月利益率 = 0;
            }

            空車ＫＭ = AppCommon.IntParse((rowS01["走行ＫＭ"] == null ? 0 : rowS01["走行ＫＭ"]).ToString()) - AppCommon.IntParse((rowS01["実車ＫＭ"] == null ? 0 : rowS01["実車ＫＭ"]).ToString());

            decimal.TryParse(rowS01["燃料Ｌ"].ToString(), out dJyosu);
            if (dJyosu != 0)
            {
                燃費 = Math.Round((AppCommon.DecimalParse((rowS01["走行ＫＭ"] == null ? 0 : rowS01["走行ＫＭ"]).ToString()) / dJyosu), 1, MidpointRounding.AwayFromZero);
            }
            else
            {
                燃費 = 0;
            }

            decimal.TryParse(rowS01["走行ＫＭ"].ToString(), out dJyosu);
            if (dJyosu != 0)
            {
                収入 = AppCommon.IntParse(Math.Round(AppCommon.DecimalParse((rowS01["運送収入"] == null ? 0 : rowS01["運送収入"]).ToString()) / dJyosu, 0, MidpointRounding.AwayFromZero).ToString());
            }
            else
            {
                収入 = 0;
            }

            decimal.TryParse(rowS01["走行ＫＭ"].ToString(), out dJyosu);
            if (dJyosu != 0)
            {
                輸送原価 = AppCommon.IntParse(Math.Round(AppCommon.DecimalParse((AppCommon.IntParse(rowS01["一般管理費"].ToString()) + 小計B + 小計C + 小計D).ToString()) / dJyosu, 0, MidpointRounding.AwayFromZero).ToString());
            }
            else
            {
                輸送原価 = 0;
            }

            Koteihi_Color();


        }
        #endregion

        #region リボン

        /// <summary>
        /// F1 マスタ検索
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
        /// F8 リスト一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            //MST25020 view = new MST25020();
            //view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            try
            {

                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (乗務員ID == null)
                {
                    this.ErrorMessage = "乗務員IDは入力必須項目です。";
                    MessageBox.Show("乗務員IDは入力必須項目です。");
                    return;
                }

                if (処理年月 == null)
                {
                    this.ErrorMessage = "処理年月は入力必須項目です。";
                    MessageBox.Show("処理年月は入力必須項目です。");
                    return;
                }

                //rowS01["乗務員KEY"] = 乗務員ID;
                //rowS01["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                //rowS11["乗務員KEY"] = 乗務員ID;
                //rowS11["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;

                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {

                    //データ登録[]
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 
                                                                                            this.rowS01
                                                                                            }));

                    var rowHendo = new DataTable();
                    var rowJinken = new DataTable();
                    var rowKotei = new DataTable();
                    //リストをデータテーブルへ
                    AppCommon.ConvertToDataTable(rowHendoData, rowHendo);
                    AppCommon.ConvertToDataTable(rowJinkenData, rowJinken);
                    AppCommon.ConvertToDataTable(rowKoteiData, rowKotei);

                    //データ登録
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11UpdateTable_Hendo, new object[] { 
                                                                                            this.rowS01,
                                                                                            rowHendo,
                                                                                            rowJinken,
                                                                                            rowKotei,
                                                                                            }));
                    ////データ登録
                    //base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11UpdateTable_Jinken, new object[] { 
                    //                                                                                            this.rowJinkenData
                    //                                                                                            }));
                    ////データ登録
                    //base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11UpdateTable_Kotei, new object[] { 
                    //                                                                                            this.rowKoteiData
                    //                                                                                            }));


                    ScreenClear();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }
        }

        /// <summary>
        /// F11 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

			if (乗務員ID == null)
			{
				this.ErrorMessage = "乗務員IDは入力必須項目です。";
				MessageBox.Show("乗務員IDは入力必須項目です。");
				return;
			}

			if (処理年月 == null)
			{
				this.ErrorMessage = "処理年月は入力必須項目です。";
				MessageBox.Show("処理年月は入力必須項目です。");
				return;
			}

            int? 年月;

            年月 = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;

            //データ削除
			base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { 乗務員ID, 年月 }));
            //データ削除
			base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11DeleteTable_Hendo, new object[] { 乗務員ID, 年月 }));
        }

        #endregion

        #region リボンボタン系

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

        #region 受信系処理
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

                if (data is DataTable)
                {
                    switch (message.GetMessageName())
                    {

                        //データ取得
                        case TargetTableNm:
                            if (!(乗務員ID > 0))
                            {
                                iChkFlg = 9;
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                                //rowS01Data = tbl;
                                SetTblData(tbl);

                                int? 年月;

                                年月 = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                                年月 = AppCommon.IntParse(年月.ToString());

                                //マスタ表示
                                base.SendRequest(new CommunicationObject(MessageType.RequestData, S11TableNm_Hendo, new object[] { 乗務員ID, 年月 }));
                                //マスタ表示
                                base.SendRequest(new CommunicationObject(MessageType.RequestData, S11TableNm_Jinken, new object[] { 乗務員ID, 年月 }));
                                //マスタ表示
                                base.SendRequest(new CommunicationObject(MessageType.RequestData, S11TableNm_Kotei, new object[] { 乗務員ID, 年月 }));
                            }
                            else
                            {
                                this.ErrorMessage = "データがありません。";
                                iChkFlg = 9;
                            }

                            break;

                        //変動データ取得
                        case S11TableNm_Hendo:
                            if (!(乗務員ID > 0))
                            {
                                break;
                            }
                            if (tbl.Rows.Count > 0)
                            {
                                rowHendoData = (List<S13_DRVSB_Member>)AppCommon.ConvertFromDataTable(typeof(List<S13_DRVSB_Member>), tbl);
                                //this.sp変動費.ItemsSource = this.rowHendoData.DefaultView;
                                //this.sp変動費.SelectedItems.Clear();

                            }
                            else
                            {
                                return;
                            }

                            break;

                        //人件データ取得
                        case S11TableNm_Jinken:
                            if (!(乗務員ID > 0))
                            {
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                rowJinkenData = (List<S13_DRVSB_Member>)AppCommon.ConvertFromDataTable(typeof(List<S13_DRVSB_Member>), tbl);
                                //this.sp人件費.ItemsSource = this.rowJinkenData.DefaultView;
                                //this.sp人件費.SelectedItems.Clear();
                            }
                            else
                            {
                                return;
                            }
                            break;

                        //固定データ取得
                        case S11TableNm_Kotei:
                            if (!(乗務員ID > 0))
                            {
                                SetFocusToTopControl();
                                this.ErrorMessage = "データがありません。";
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                rowKoteiData = (List<S13_DRVSB_Member>)AppCommon.ConvertFromDataTable(typeof(List<S13_DRVSB_Member>), tbl);
                                //this.sp固定経.ItemsSource = this.rowKoteiData.DefaultView;
                                //this.sp固定経.SelectedItems.Clear();
                            }
                            else
                            {
                                SetFocusToTopControl();
                                this.ErrorMessage = "データがありません。";
                                return;
                            }

                            if (iChkFlg == 0)
                            {
                                System.Windows.Forms.Application.DoEvents();
                                Keisan();
                                //キーをfalse
                                ChangeKeyItemChangeable(false);
                                SetFocusToTopControl();
                            }
                            else
                            {
                                SetFocusToTopControl();
                                this.ErrorMessage = "データがありません。";
                                return;
                            }


                            break;

                        case UpdateTable:

                            break;
                        case DeleteTable:
                            ScreenClear();
                            break;
                        //case M04_DRVTableNm:
                        //    string Henkan;
                        //    Henkan = tbl.Rows[0]["Ｔ締日"].ToString();
                        //    締日 = AppCommon.IntParse(Henkan);
                        //    break;

                        default:
                            break;
                    }
                }
                else
                {

                    //DataTable tbl = data as DataTable;
                    switch (message.GetMessageName())
                    {
                        case TargetTableNm:
                            iChkFlg = 9;
                            break;
                        case S11TableNm_Hendo:

                            rowS11 = tbl.Rows[0];

                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            rowS11 = tbl.Rows[0];

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;
                        //人件データ取得
                        case S11TableNm_Jinken:
                            return;
                        //固定データ取得
                        case S11TableNm_Kotei:
                            if (iChkFlg == 9)
                            {
                                this.ErrorMessage = "データがありません。";
                            }
                            return;
                        case UpdateTable:
                            return;
                        case DeleteTable:
                            ScreenClear();
                            break;
                        default:
                            break;
                    }
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

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {

            rowS01 = tbl.Rows[0];

            //string Henkan;

            //Henkan = tbl.Rows[0]["乗務員KEY"].ToString();
            //乗務員KEY = AppCommon.IntParse(Henkan);
            //Henkan = tbl.Rows[0]["集計年月"].ToString();
            //処理年月 = DateTime.Parse(Henkan);

        }

        #region  経費先前次ボタン 現在使用してない

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M04_DRVTableNm, new object[] { null, 0 }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //前データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M04_DRVTableNm, new object[] { 乗務員KEY, -1 }));

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 次データを検索する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M04_DRVTableNm, new object[] { 乗務員KEY, 1 }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 最後のデータを検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //最後尾検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M04_DRVTableNm, new object[] { null, 1 }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion



        #region  マスタ前次ボタン

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 乗務員KEY, null, 0 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 乗務員KEY, 処理年月, -1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 次データを検索する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //次データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 乗務員KEY, 処理年月, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 最後のデータを検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 乗務員KEY, null, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion


        /// <summary>
        /// コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, RoutedEventArgs e)
        {
            //try
            //{
                //if (e.Key == Key.Enter)
                //{
                    try
                    {

                        if (!base.CheckAllValidation())
                        {
                            MessageBox.Show("入力内容に誤りがあります。");
                            SetFocusToTopControl();
                            return;
                        }
                        int? 年月;

                        年月 = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                        年月 = AppCommon.IntParse(年月.ToString());

                        //データチェックフラグ初期化
                        iChkFlg = 0;
                        //マスタ表示
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 乗務員ID, 年月 }));
                       
                    }
                    catch (Exception ex)
                    {
                        appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                        this.ErrorMessage = ex.Message;
                        return;
                    }

                //}
            //}
            //catch (Exception ex)
            //{
            //    appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            //    this.ErrorMessage = ex.Message;
            //}
        }


        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            /* 経費先のボタン
            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;
            */

            //FistIdButton2.IsEnabled = pBool;
            //BeforeIdButton2.IsEnabled = pBool;
            //NextIdButton2.IsEnabled = pBool;
            //LastIdButoon2.IsEnabled = pBool;
        }


        private void UcTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.Key == Key.Enter)
                {
                    OnF9Key(sender, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }


        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
			rowHendoData = null;
			rowJinkenData = null;
			rowKoteiData = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        private void KOUSHIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keisan();
            }
        }

        //SpreadでEnterを押下した時
        private void KOUSHIN_KeyDown(object sender, SpreadCellEditEndedEventArgs e)
        {
            Keisan();
        }

        #region スプレッドの固定費色変更
        //画面が閉じられた時、データを保持する
        private void Koteihi_Color()
        {
            for (int cnt = 0; sp変動費.RowCount > cnt; cnt++)
            {
                if (Convert.ToInt32(sp変動費[cnt, 2].Value) == 0)
                {
                    sp変動費.Cells[cnt, 0].Foreground = new SolidColorBrush(Colors.Blue);

                }
            }
            for (int cnt = 0; sp人件費.RowCount > cnt; cnt++)
            {
                if (Convert.ToInt32(sp人件費[cnt, 2].Value) == 0)
                {
                    sp人件費.Cells[cnt, 0].Foreground = new SolidColorBrush(Colors.Blue);

                }
            }
            for (int cnt = 0; sp固定経.RowCount > cnt; cnt++)
            {
                if (Convert.ToInt32(sp固定経[cnt, 2].Value) == 0)
                {
                    sp固定経.Cells[cnt, 0].Foreground = new SolidColorBrush(Colors.Blue);

                }
            }
        }
        #endregion

    }
}
