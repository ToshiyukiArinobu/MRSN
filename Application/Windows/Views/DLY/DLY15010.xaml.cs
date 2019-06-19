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
    /// 売上明細問合せ
    /// </summary>
    public partial class DLY15010 : RibbonWindowViewBase
    {

		public class DLY15010_PREVIEW : INotifyPropertyChanged
		{
			private int? _得意先ID;
			private DateTime? _入金日付;
			private string _得意先名;
			private string _入金区分;
			private decimal _入金金額;
			private decimal _入金合計;
			private string _摘要名;
			private DateTime? _手形日付;
			private int? _明細番号;
			private int? _明細行;
			private int _明細区分;
			private string _得意先指定;
			private DateTime? _検索日付From;
			private DateTime? _検索日付To;
			private string _検索日付選択;
			private string _表示順序;


			public int? 得意先ID { get { return _得意先ID; } set { _得意先ID = value; NotifyPropertyChanged(); } }
			public DateTime? 入金日付 { get { return _入金日付; } set { _入金日付 = value; NotifyPropertyChanged(); } }
			public string 得意先名 { get { return _得意先名; } set { _得意先名 = value; NotifyPropertyChanged(); } }
			public string 入金区分 { get { return _入金区分; } set { _入金区分 = value; NotifyPropertyChanged(); } }
			public decimal 入金金額 { get { return _入金金額; } set { _入金金額 = value; NotifyPropertyChanged(); } }
			public decimal 入金合計 { get { return _入金合計; } set { _入金合計 = value; NotifyPropertyChanged(); } }
			public string 摘要名 { get { return _摘要名; } set { _摘要名 = value; NotifyPropertyChanged(); } }
			public DateTime? 手形日付 { get { return _手形日付; } set { _手形日付 = value; NotifyPropertyChanged(); } }
			public int? 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
			public int? 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
			public int 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }
			public string 得意先指定 { get { return _得意先指定; } set { _得意先指定 = value; NotifyPropertyChanged(); } }
			public DateTime? 検索日付From { get { return _検索日付From; } set { _検索日付From = value; NotifyPropertyChanged(); } }
			public DateTime? 検索日付To { get { return _検索日付To; } set { _検索日付To = value; NotifyPropertyChanged(); } }
			public string 検索日付選択 { get { return _検索日付選択; } set { _検索日付選択 = value; NotifyPropertyChanged(); } }
			public string 表示順序 { get { return _表示順序; } set { _表示順序 = value; NotifyPropertyChanged(); } }


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

        #region データアクセス用ID
        const string SelectedChar = "a";
        const string UnselectedChar = "";
        const string ReportFileName = @"Files\DLY\DLY15010.rpt";

        private const string GET_DATA = "DLY15010";
        private const string UPDATE_ROW = "DLY15010_UPDATE";
        private const string GET_MST1 = "DLY15010_MST1";
        private const string GET_MST2 = "DLY15010_MST2";
        private const string GET_MST3 = "DLY15010_MST3";
        private const string GET_MST4 = "DLY15010_MST4";
        private const string GET_MST5 = "DLY15010_MST5";
        private const string GET_MST6 = "DLY15010_MST6";
        private const string GET_MST7 = "DLY15010_MST7";
        private const string GET_MST8 = "DLY15010_MST8";
        private const string GET_MST9 = "DLY15010_MST9";

        //対象テーブル検索用
        private const string TargetTableNm = "DLY15010_GetData";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "DLY15010_UPDATE";
        //対象テーブル CSV用
        private const string TargetTableNmCsv = "DLY15010_CSV";
        //対象テーブル印刷用
        private const string TargetTableNmPreview = "DLY15010_PREVIEW";
        //対象テーブル印刷用
        private const string rptFullPathName_PIC = @"Files\DLY\DLY15010.rpt";

        #endregion


        // SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
        string _originalText = null;


        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigDLY15010 : FormConfigBase
        {
            public string[] 表示順 { get; set; }
            public bool[] 表示順方向 { get; set; }
            // コンボボックスの位置
            public int 自社部門index { get; set; }

            public byte[] spConfig20180118 = null;

            public string 作成年 { get; set; }
            public string 作成月 { get; set; }
            public string 締日 { get; set; }
            public string 集計期間From { get; set; }
            public string 集計期間To { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public bool? チェック { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY15010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd売上詳細表示 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd売上詳細表示(GcSpreadGrid gcSpreadGrid)
            {
                this._gcSpreadGrid = gcSpreadGrid;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;
            public void OnCanExecuteChanged()
            {
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
            public void Execute(object parameter)
            {
                CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                if (cellCommandParameter.Area == SpreadArea.Cells)
                {
                    int rowNo = cellCommandParameter.CellPosition.Row;
                    var row = this._gcSpreadGrid.Rows[rowNo];
                    var mNo = row.Cells[this._gcSpreadGrid.Columns["明細番号"].Index].Value;
                    var wnd = GetWindow(this._gcSpreadGrid);

                    DLY08010 frm = new DLY08010();
                    frm.初期明細番号 = (int?)mNo;
					frm.ShowDialog(wnd);
					if (frm.IsUpdated)
					{
						// 日報側で更新された場合、再検索を実行する
						var parent = ViewBaseCommon.FindVisualParent<DLY15010>(this._gcSpreadGrid);
						parent.Button_Click_1(null, null);
					}

                }
            }
        }
        #endregion

        #region Member

        /// <summary>
        ///　メンバー変数
        /// </summary>

		//private UcLabelComboBox[] orderComboboxes = new UcLabelComboBox[] { null, null, null, null, null };

        #endregion

        #region バインド用プロパティ

        private string _担当者ID = null;
        public string 担当者ID
        {
            set { _担当者ID = value; NotifyPropertyChanged(); }
            get { return _担当者ID; }
        }

        bool _請求内訳条件Enabled = false;
        public bool 請求内訳条件Enabled
        {
            get { return this._請求内訳条件Enabled; }
            set { this._請求内訳条件Enabled = value; NotifyPropertyChanged(); }
        }

        string _検索日付選択 = null;
        public string 検索日付選択
        {
            get { return this._検索日付選択; }
            set { this._検索日付選択 = value; NotifyPropertyChanged(); }
        }

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

        string _適用開始日From = string.Empty;
        public string 適用開始日From
        {
            get { return this._適用開始日From; }
            set { this._適用開始日From = value; NotifyPropertyChanged(); }
        }

        string _適用開始日To = string.Empty;
        public string 適用開始日To
        {
            get { return this._適用開始日To; }
            set { this._適用開始日To = value; NotifyPropertyChanged(); }
        }

        private int _入金区分 = 0;
        public int 入金区分
        {
            get { return this._入金区分; }
            set { this._入金区分 = value; NotifyPropertyChanged(); }
        }

        private decimal? _入金合計 = 0;
        public decimal? 入金合計
        {
            get { return this._入金合計; }
            set { this._入金合計 = value; NotifyPropertyChanged(); }
        }

        string _ピックアップ種類 = null;
        public string ピックアップ種類
        {
            get { return this._ピックアップ種類; }
            set
            {
                this._ピックアップ種類 = value;
                NotifyPropertyChanged();
                PickupSelect(value);
            }
        }

        //string _検索ボタンラベル = "検 索";
        //public string 検索ボタンラベル
        //{
        //	get { return this._検索ボタンラベル; }
        //	set
        //	{
        //		this._検索ボタンラベル = value;
        //		NotifyPropertyChanged();
        //	}
        //}

        string[] _表示順 = new string[] { "", "", "", "", "", };
        public string[] 表示順
        {
            get { return this._表示順; }
            set { this._表示順 = value; NotifyPropertyChanged(); }
        }

        string[] _表示順名 = new string[] { "", "", "", "", "", };
        public string[] 表示順名
        {
            get { return this._表示順名; }
            set { this._表示順名 = value; NotifyPropertyChanged(); }
        }

        bool[] _表示順方向 = new bool[] { false, false, false, false, false };
        public bool[] 表示順方向
        {
            get { return this._表示順方向; }
            set { this._表示順方向 = value; NotifyPropertyChanged(); }
        }

        private string _車輌ID = null;
        public string 車輌ID
        {
            set
            {
                _車輌ID = value;
                NotifyPropertyChanged();
            }
            get { return _車輌ID; }
        }

        private string _乗務員ID = null;
        public string 乗務員ID
        {
            set
            {
                _乗務員ID = value;
                NotifyPropertyChanged();
            }
            get { return _乗務員ID; }
        }

        private string _車種ID = null;
        public string 車種ID
        {
            set
            {
                _車種ID = value;
                NotifyPropertyChanged();
            }
            get { return _車種ID; }
        }

        Dictionary<string, Visibility> _RangeVisibilities = new Dictionary<string, Visibility>()
		{
			{ "得意先", Visibility.Hidden },
			{ "支払先", Visibility.Hidden },
			{ "仕入先", Visibility.Hidden },
			{ "乗務員", Visibility.Hidden },
			{ "車輌", Visibility.Hidden },
			{ "車種", Visibility.Hidden },
			{ "発地", Visibility.Hidden },
			{ "着地", Visibility.Hidden },
			{ "商品", Visibility.Hidden },
		};


        public Dictionary<string, Visibility> RangeVisibilities
        {
            get { return _RangeVisibilities; }
            set
            {
                _RangeVisibilities = value;
                NotifyPropertyChanged();
            }
        }

        private string _入力区分 = string.Empty;
        public string 入力区分
        {
            set { _入力区分 = value; NotifyPropertyChanged(); }
            get { return _入力区分; }
        }

        private string _乗務員名 = string.Empty;
        public string 乗務員名
        {
            set { _乗務員名 = value; NotifyPropertyChanged(); }
            get { return _乗務員名; }
        }

        private string _車輌番号 = string.Empty;
        public string 車輌番号
        {
            set { _車輌番号 = value; NotifyPropertyChanged(); }
            get { return _車輌番号; }
        }

        private int _支払税区分;
        public int 支払税区分
        {
            set { _支払税区分 = value; NotifyPropertyChanged(); }
            get { return _支払税区分; }
        }

        private decimal _支払単価;
        public decimal 支払単価
        {
            set { _支払単価 = value; NotifyPropertyChanged(); }
            get { return _支払単価; }
        }

        private decimal? _才数;
        public decimal? 才数
        {
            set { _才数 = value; NotifyPropertyChanged(); }
            get { return _才数; }
        }

        private decimal _支払割増１;
        public decimal 支払割増１
        {
            set { _支払割増１ = value; NotifyPropertyChanged(); }
            get { return _支払割増１; }
        }

        private string _請求内訳ID = null;
        public string 請求内訳ID
        {
            set { _請求内訳ID = value; NotifyPropertyChanged(); }
            get { return _請求内訳ID; }
        }

        private string _支払先ID = null;
        public string 支払先ID
        {
            set { _支払先ID = value; NotifyPropertyChanged(); }
            get { return _支払先ID; }
        }

        private string _自社部門ID = null;
        public string 自社部門ID
        {
            set { _自社部門ID = value; NotifyPropertyChanged(); }
            get { return _自社部門ID; }
        }

        private string _発地名 = "";
        public string 発地名
        {
            set { _発地名 = value; NotifyPropertyChanged(); }
            get { return _発地名; }
        }
        private string _着地名 = "";
        public string 着地名
        {
            set { _着地名 = value; NotifyPropertyChanged(); }
            get { return _着地名; }
        }

        private string _商品名 = "";
        public string 商品名
        {
            set { _商品名 = value; NotifyPropertyChanged(); }
            get { return _商品名; }
        }

        private string _請求摘要 = "";
        public string 請求摘要
        {
            set { _請求摘要 = value; NotifyPropertyChanged(); }
            get { return _請求摘要; }
        }

        private string _社内備考 = "";
        public string 社内備考
        {
            set { _社内備考 = value; NotifyPropertyChanged(); }
            get { return _社内備考; }
        }

        private int? _売上未定区分 = null;
        public int? 売上未定区分
        {
            set { _売上未定区分 = value; NotifyPropertyChanged(); }
            get { return _売上未定区分; }
        }

        // 絞り込みコード範囲
        private string pickupCodeFROM = null;
        private string pickupCodeTO = null;

        private string _得意先FROM = null;
        public string 得意先FROM
        {
            get { return _得意先FROM; }
            set { _得意先FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _得意先TO = null;
        public string 得意先TO
        {
            get { return _得意先TO; }
            set { _得意先TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _支払先FROM = null;
        public string 支払先FROM
        {
            get { return _支払先FROM; }
            set { _支払先FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _支払先TO = null;
        public string 支払先TO
        {
            get { return _支払先TO; }
            set { _支払先TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _仕入先FROM = null;
        public string 仕入先FROM
        {
            get { return _仕入先FROM; }
            set { _仕入先FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _仕入先TO = null;
        public string 仕入先TO
        {
            get { return _仕入先TO; }
            set { _仕入先TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _乗務員FROM = null;
        public string 乗務員FROM
        {
            get { return _乗務員FROM; }
            set { _乗務員FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _乗務員TO = null;
        public string 乗務員TO
        {
            get { return _乗務員TO; }
            set { _乗務員TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _車輌FROM = null;
        public string 車輌FROM
        {
            get { return _車輌FROM; }
            set { _車輌FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _車輌TO = null;
        public string 車輌TO
        {
            get { return _車輌TO; }
            set { _車輌TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _車種FROM = null;
        public string 車種FROM
        {
            get { return _車種FROM; }
            set { _車種FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _車種TO = null;
        public string 車種TO
        {
            get { return _車種TO; }
            set { _車種TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _発地FROM = null;
        public string 発地FROM
        {
            get { return _発地FROM; }
            set { _発地FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _発地TO = null;
        public string 発地TO
        {
            get { return _発地TO; }
            set { _発地TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _着地FROM = null;
        public string 着地FROM
        {
            get { return _着地FROM; }
            set { _着地FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _着地TO = null;
        public string 着地TO
        {
            get { return _着地TO; }
            set { _着地TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }
        private string _商品FROM = null;
        public string 商品FROM
        {
            get { return _商品FROM; }
            set { _商品FROM = value; pickupCodeFROM = value; NotifyPropertyChanged(); }
        }
        private string _商品TO = null;
        public string 商品TO
        {
            get { return _商品TO; }
            set { _商品TO = value; pickupCodeTO = value; NotifyPropertyChanged(); }
        }

        // 絞り込み条件（締日）
        private string _ピックアップ締日 = null;
        public string ピックアップ締日
        {
            get { return _ピックアップ締日; }
            set { _ピックアップ締日 = value; NotifyPropertyChanged(); }
        }

        //----------------------------------------
        // 検索結果データ
        //----------------------------------------
        private DataTable _pickup得意先 = null;
        public DataTable Pickup得意先
        {
            get { return this._pickup得意先; }
            set { this._pickup得意先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup支払先 = null;
        public DataTable Pickup支払先
        {
            get { return this._pickup支払先; }
            set { this._pickup支払先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _Pickup仕入先 = null;
        public DataTable Pickup仕入先
        {
            get { return this._Pickup仕入先; }
            set { this._Pickup仕入先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup乗務員 = null;
        public DataTable Pickup乗務員
        {
            get { return this._pickup乗務員; }
            set { this._pickup乗務員 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup車輌 = null;
        public DataTable Pickup車輌
        {
            get { return this._pickup車輌; }
            set { this._pickup車輌 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup車種 = null;
        public DataTable Pickup車種
        {
            get { return this._pickup車種; }
            set { this._pickup車種 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup発地 = null;
        public DataTable Pickup発地
        {
            get { return this._pickup発地; }
            set { this._pickup発地 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup着地 = null;
        public DataTable Pickup着地
        {
            get { return this._pickup着地; }
            set { this._pickup着地 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickup商品 = null;
        public DataTable Pickup商品
        {
            get { return this._pickup商品; }
            set { this._pickup商品 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private DataTable _pickupdata = null;
        public DataTable PickupData
        {
            get { return this._pickupdata; }
            set { this._pickupdata = value; NotifyPropertyChanged(); }
        }

        private bool PickupSwitch = false;

        // 検索した結果データ
        //private DataTable _dUriageDataResult = null;
        //public DataTable 売上明細データ検索結果
        //{
        //    get
        //    {
        //        return this._dUriageDataResult;
        //    }
        //    set
        //    {
        //        this._dUriageDataResult = value;
        //        if (value == null)
        //        {
        //            this.売上明細データ = null;
        //        }
        //        else
        //        {
        //            this.売上明細データ = value.Copy();
        //        }
        //        NotifyPropertyChanged();
        //        //NotifyPropertyChanged("売上明細データ");
        //    }
        //}

        private List<DLY15010_PREVIEW> _dUriageData = null;
        public List<DLY15010_PREVIEW> 売上明細データ
        {
            get
            {
                return this._dUriageData;
            }
            set
            {
                this._dUriageData = value;
                this.sp入金明細データ.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        //画面下のBinding変数

        private decimal _現金通行料 = 0;
        public decimal 現金通行料
        {
            get { return _現金通行料; }
            set { _現金通行料 = value; NotifyPropertyChanged(); }
        }

        private decimal _プレート = 0;
        public decimal プレート
        {
            get { return _プレート; }
            set { _プレート = value; NotifyPropertyChanged(); }
        }

        private decimal _フェリー代 = 0;
        public decimal フェリー代
        {
            get { return _フェリー代; }
            set { _フェリー代 = value; NotifyPropertyChanged(); }
        }

        private decimal _電話代 = 0;
        public decimal 電話代
        {
            get { return _電話代; }
            set { _電話代 = value; NotifyPropertyChanged(); }
        }

        private decimal _その他 = 0;
        public decimal その他
        {
            get { return _その他; }
            set { _その他 = value; NotifyPropertyChanged(); }
        }

        private decimal _燃料代 = 0;
        public decimal 燃料代
        {
            get { return _燃料代; }
            set { _燃料代 = value; NotifyPropertyChanged(); }
        }

        private decimal _運行費 = 0;
        public decimal 運行費
        {
            get { return _運行費; }
            set { _運行費 = value; NotifyPropertyChanged(); }
        }

        private decimal _稼動金額 = 0;
        public decimal 稼動金額
        {
            get { return _稼動金額; }
            set { _稼動金額 = value; NotifyPropertyChanged(); }
        }

        private decimal _諸経費計 = 0;
        public decimal 諸経費計
        {
            get { return _諸経費計; }
            set { _諸経費計 = value; NotifyPropertyChanged(); }
        }

        private decimal _走行ｋｍ = 0;
        public decimal 走行ｋｍ
        {
            get { return _走行ｋｍ; }
            set { _走行ｋｍ = value; NotifyPropertyChanged(); }
        }

        private decimal _実車ｋｍ = 0;
        public decimal 実車ｋｍ
        {
            get { return _実車ｋｍ; }
            set { _実車ｋｍ = value; NotifyPropertyChanged(); }
        }

        private decimal _輸送屯数 = 0;
        public decimal 輸送屯数
        {
            get { return _輸送屯数; }
            set { _輸送屯数 = value; NotifyPropertyChanged(); }
        }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; NotifyPropertyChanged(); }
        }

        private string _表示固定列数 = "5";
        public string 表示固定列数
        {
            get { return _表示固定列数; }
            set { _表示固定列数 = value; NotifyPropertyChanged(); SetupSpreadFixedColumn(this.sp入金明細データ, value); }
        }

		private int _入金区分Combo = 0;
		public int 入金区分Combo
		{
			get { return _入金区分Combo; }
			set { _入金区分Combo = value; NotifyPropertyChanged(); }
		}

		private string _摘要指定 = "";
		public string 摘要指定
		{
			get { return _摘要指定; }
			set { _摘要指定 = value; NotifyPropertyChanged(); }
		}


        #endregion

        #region DLY15010

        /// <summary>
        /// 売上明細問合せ
        /// </summary>
        public DLY15010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region LOAD
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            //スプレッドリセット時
            this.sp_Config = AppCommon.SaveSpConfig(this.sp入金明細データ);

            //得意先検索
            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });
            //コンボボックス
			AppCommon.SetutpComboboxList(this.c日付区分, false);
			AppCommon.SetutpComboboxList(this.c入金区分, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY15010)ucfg.GetConfigValue(typeof(ConfigDLY15010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY15010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
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
				this.c日付区分.SelectedIndex = frmcfg.区分1;
				this.c入金区分.SelectedIndex = frmcfg.区分2;
                this.適用開始日From = frmcfg.集計期間From;
                this.適用開始日To = frmcfg.集計期間To;
            }
            #endregion

			AppCommon.LoadSpConfig(this.sp入金明細データ, frmcfg.spConfig20180118 != null ? frmcfg.spConfig20180118 : this.sp_Config);
			sp入金明細データ.InputBindings.Add(new KeyBinding(sp入金明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));


            //ComboBoxに値を設定する
            GetComboBoxItems();

            sp入金明細データ.RowCount = 0;


            if (frmcfg.表示順 != null)
            {
                if (frmcfg.表示順.Length == 5)
                {
                    this.表示順 = frmcfg.表示順;
                }
            }
            if (frmcfg.表示順方向 != null)
            {
                if (frmcfg.表示順方向.Length == 5)
                {
                    this.表示順方向 = frmcfg.表示順方向;
                }
            }

            //詳細
            //this.spPickupList.ActiveCellPosition = CellPosition.Empty;
            //this.spPickupList.RowCount = 0;
            ButtonCellType btn = this.sp入金明細データ.Columns[0].CellType as ButtonCellType;
            btn.Command = new cmd売上詳細表示(sp入金明細データ);

            this.TekiyouDay1.SetFocus();

            this.ピックアップ種類 = "得意先";
        }
        #endregion

        #region コンボボックス取得

        /// <summary>
        /// コンボボックスのアイテムをDBから取得
        /// </summary>
        private void GetComboBoxItems()
        {
            //部門コンボ初期値設定
            //cmb部門指定.SelectedIndex = 1;

			//orderComboboxes[0] = this.cmb表示順指定0;
			//orderComboboxes[1] = this.cmb表示順指定1;
			//orderComboboxes[2] = this.cmb表示順指定2;
			//orderComboboxes[3] = this.cmb表示順指定3;
			//orderComboboxes[4] = this.cmb表示順指定4;

            //AppCommon.SetutpComboboxList(this.cmb検索日付種類, false);
            //AppCommon.SetutpComboboxList(this.cmb部門指定, false);
            //AppCommon.SetutpComboboxList(this.cmb未定区分, false);
            //AppCommon.SetutpComboboxList(this.cmbPickup, false);

            AppCommon.SetutpComboboxList(this.cmb表示順指定0, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定1, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定2, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定3, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定4, false);
			//this.cmb表示順指定0.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定1.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定2.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定3.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定4.SelectionChanged += cmb表示順指定_SelectionChanged;

        }

        #endregion

        #region ﾋﾟｯｸｱｯﾌﾟ指定Grid読込

        /// <summary>
        /// ピックアップ指定のGridの読み込み
        /// </summary>
        private void GetPickupCodeList()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region データ受信メソッド

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                case TargetTableNm:
                    base.SetFreeForInput();
                    var ds = data as DataSet;
                    if (ds == null)
                    {
                        this.売上明細データ = null;
                    }
                    else
                    {
                        売上明細データ = (List<DLY15010_PREVIEW>)AppCommon.ConvertFromDataTable(typeof(List<DLY15010_PREVIEW>), ds.Tables["DataList"]);
                        if (this.売上明細データ.Count == 0)
                        {
                            Summary();
                            this.ErrorMessage = "該当するデータはありません。";
                            return;
                        }

                        DataReSort();
                        //Perform_Pickup();
                        Summary();
                        TekiyouDay1.Focus();
                    }


                    break;
                case GET_MST1:
                    this.Pickup得意先 = tbl;
                    break;
                case GET_MST2:
                    this.Pickup支払先 = tbl;
                    break;
                case GET_MST3:
                    this.Pickup仕入先 = tbl;
                    break;
                case GET_MST4:
                    this.Pickup乗務員 = tbl;
                    break;
                case GET_MST5:
                    this.Pickup車輌 = tbl;
                    break;
                case GET_MST6:
                    this.Pickup車種 = tbl;
                    break;
                case GET_MST7:
                    this.Pickup発地 = tbl;
                    break;
                case GET_MST8:
                    this.Pickup着地 = tbl;
                    break;
                case GET_MST9:
                    this.Pickup商品 = tbl;
                    break;


                //プレビュー出力用
                case TargetTableNmPreview:

                    base.SetFreeForInput();
                    var dp = data as DataSet;
                    if (dp == null)
                    {
                        this.売上明細データ = null;
                    }
                    else
                    {
                        DispPreviw(dp.Tables["DataList"]);
                        if (dp.Tables["DataList"].Rows.Count == 0)
                        {
                            this.ErrorMessage = "該当するデータはありません。";
                            return;
                        }
                    }
                    break;
				case TargetTableNmUpdate:
					if (CloseFlg) { CloseFlg = false; this.Close(); }
					break;
                
            }

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
        /// 詳細表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF4Key(object sender, KeyEventArgs e)
        {
            if (this.sp入金明細データ.ActiveCellPosition.Row < 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }
            DisplayDetail();
        }

        #region CSVファイル出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.売上明細データ == null || this.売上明細データ.Count == 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }
            DataTable CSVデータ = new DataTable();
            //リストをデータテーブルへ
            AppCommon.ConvertToDataTable(売上明細データ, CSVデータ);

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
                CSVData.SaveCSV(CSVデータ, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }
        #endregion

        #region 印刷
        /// <summary>
        /// 印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {

            PrintOut();
        }

        void PrintOut()
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;

            if (this.売上明細データ == null)
			{
				this.ErrorMessage = "印刷データがありません。";
                return;
            }
            if (this.売上明細データ.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }
            try
            {
                base.SetBusyForInput();
                var parms = new List<Framework.Reports.Preview.ReportParameter>()
				{
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付区分", VALUE=(this.c日付区分.Text==null?"":this.c日付区分.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="入金区分", VALUE=(this.c入金区分.Text==null?"":this.c入金区分.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付FROM", VALUE=(this.適用開始日From==null?"":this.適用開始日From)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付TO", VALUE=(this.適用開始日To==null?"":this.適用開始日To)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="得意先指定", VALUE=(this.T指定.Text2==null?"":this.T指定.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="表示順序", VALUE=string.Format("{0} {1} {2} {3} {4}", 表示順名[0], 表示順名[1], 表示順名[2], 表示順名[3], 表示順名[4])},
				};
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = null;

				DataTable 印刷データ = new DataTable("入金伝票一覧");
                //リストをデータテーブルへ
                //AppCommon.ConvertToDataTable(売上明細データ, 印刷データ);

				Dictionary<string, string> changecols = new Dictionary<string, string>()
				{
				};

				AppCommon.ConvertSpreadDataToTable<DLY15010_PREVIEW>(this.sp入金明細データ, 印刷データ, changecols);

				view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
				view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
				view.SetReportData(印刷データ);

				view.SetupParmeters(parms);

				base.SetFreeForInput();

				view.PrinterName = frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("得意先売上明細書の印刷時に例外が発生しました。", ex);
            }
        }

        #endregion

        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            base.OnF10Key(sender, e);
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

        #region プレビュー画面
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
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view.MakeReport("入金伝票問合せ", rptFullPathName_PIC, 0, 0, 0);
                view.SetReportData(tbl);
				view.ShowPreview();
				view.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 検索ボタン

        /// <summary>
        /// 検索ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

				sp入金明細データ.FilterDescriptions.Clear();

                if (ExpSyousai.IsExpanded == true)
                {
                    ExpSyousai.IsExpanded = false;
                }


                int? 担当者ID = null;
				int iwk;
				int 日付区分C;
				日付区分C = c日付区分.SelectedIndex + 1;
				int 入金区分C;
				入金区分C = c入金区分.SelectedIndex;
                if (int.TryParse(this.担当者ID, out iwk) == true)
                {
                    担当者ID = iwk;
                }
                //レポート表示用　コンボボックス表示内容取得
                string 表示順指定0 = cmb表示順指定0.Combo_Text.ToString();
                string 表示順指定1 = cmb表示順指定1.Combo_Text.ToString();
                string 表示順指定2 = cmb表示順指定2.Combo_Text.ToString();
                string 表示順指定3 = cmb表示順指定3.Combo_Text.ToString();
                string 表示順指定4 = cmb表示順指定4.Combo_Text.ToString();

                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }
                int? i得意先ID = null;
                DateTime? d適用開始日From = null;
                DateTime? d適用開始日To = null;

                if (!string.IsNullOrEmpty(適用開始日From))
                {
                    d適用開始日From = Convert.ToDateTime(適用開始日From);
                }

                if (!string.IsNullOrEmpty(適用開始日To))
                {
                    d適用開始日To = Convert.ToDateTime(適用開始日To);
                }

                //ピックアップ種類（呼び出し）
                //PickupSelect(this.ピックアップ種類);

                CommunicationObject com
				= new CommunicationObject(MessageType.UpdateData, TargetTableNm, new object[] { 担当者ID, 得意先ID, 得意先名, d適用開始日From, d適用開始日To, 日付区分C, 入金区分C, 表示順指定0, 表示順指定1, 表示順指定2, 表示順指定3, 表示順指定4, 摘要指定 });

                base.SendRequest(com);
                base.SetBusyForInput();
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Cmb表示順指定

        private void cmb表示順指定_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = (sender as UcLabelComboBox).SelectedIndex;
            int cnt = 0;
            // 同じ項目を2回以上指定できないようにする
			//foreach (var cmb in this.orderComboboxes)
			foreach (var cmb in new UcLabelComboBox[] { this.cmb表示順指定0, this.cmb表示順指定1, this.cmb表示順指定2, this.cmb表示順指定3, this.cmb表示順指定4, })
            {
                if (cmb.SelectedIndex < 1)
                {
                    continue;
                }
                if (cmb.SelectedIndex == sel)
                {
                    cnt++;
                    if (cnt > 1)
                    {
                        MessageBox.Show("既に指定されています。");
                        (sender as UcLabelComboBox).SelectedIndex = 0;
                        return;
                    }
                }
            }
        }

        #endregion

        #region ソートボタン

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            DataReSort();
        }

        #endregion

        #region DataReSort【ソート】

        void DataReSort()
        {
            if (売上明細データ == null)
            {
                return;
            }
            if (売上明細データ.Count == 0)
            {
                return;
            }
            this.sp入金明細データ.SortDescriptions.Clear();
            UcLabelComboBox[] cmblist = { this.cmb表示順指定0, this.cmb表示順指定1, this.cmb表示順指定2, this.cmb表示順指定3, this.cmb表示順指定4, };
            int ix = -1;
            foreach (var cmb in cmblist)
            {
                ix++;
                CodeData cd = cmb.Combo_SelectedItem as CodeData;
                if (cd == null)
                {
                    continue;
                }
                if (cd.表示名 == "設定なし")
                {
                    continue;
                }
                try
                {
                    var sort = new SpreadSortDescription();
                    sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    switch (cd.表示名)
                    {
                        case "入力順":
                            sort.ColumnName = "明細番号";
                            this.sp入金明細データ.SortDescriptions.Add(sort);
                            sort = new SpreadSortDescription();
                            sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                            sort.ColumnName = "明細行";
                            this.sp入金明細データ.SortDescriptions.Add(sort);
                            break;
                        case "指定日付":
                            sort.ColumnName = (c日付区分.Combo_SelectedItem as CodeData).表示名 + "日付";
                            this.sp入金明細データ.SortDescriptions.Add(sort);
                            break;
                        case "運行者名":
                            sort.ColumnName = "乗務員名";
                            this.sp入金明細データ.SortDescriptions.Add(sort);
                            break;
                        default:
                            sort.ColumnName = cd.表示名;
                            this.sp入金明細データ.SortDescriptions.Add(sort);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    appLog.Error("売上明細データSPREADのソート時に例外が発生しました。", ex);
                    //this.ErrorMessage = "データの並び替え中にシステムエラーが発生しました。サポートにお問い合わせください。";
                }
            }
        }

        #endregion

        #region AllSelect

        private void AllSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in this.PickupData.Rows)
            {
                row["選択"] = SelectedChar;
            }
        }

        #endregion

        #region AllClear

        private void AllClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in this.PickupData.Rows)
            {
                row["選択"] = UnselectedChar;
            }
        }

        #endregion


        #region 合計用メンバー

        // 合計計算用
        public class TOTAL_MEMBER
        {
            public decimal 数量 { get; set; }
            public decimal 重量 { get; set; }
            public int 売上金額 { get; set; }
            public int 通行料 { get; set; }
            public int 請求割増１ { get; set; }
            public int 請求割増２ { get; set; }
            public int 支払金額 { get; set; }
            public int 支払通行料 { get; set; }
        }

        #endregion

        #region 合計計算

        // 合計計算
        void Summary()
        {
            入金合計 = 0;

			if (sp入金明細データ.Columns[1].Name == null)
			{
				return;
			}

			DataTable 印刷データ = new DataTable("印刷データ");

			Dictionary<string, string> changecols = new Dictionary<string, string>()
			{
			};

			AppCommon.ConvertSpreadDataToTable<DLY15010_PREVIEW>(this.sp入金明細データ, 印刷データ, changecols);

			入金合計 = AppCommon.DecimalParse(印刷データ.Compute("Sum(入金金額)", null).ToString());

        }

        #endregion

        #region Cmb部門指定

        /// <summary>
        /// 部門のコンボボックスのリスト準備完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb部門指定_DataListInitialized(object sender, RoutedEventArgs e)
        {
            this.自社部門ID = "0";
        }

        private void PickupSelect(string selText)
        {
            foreach (var item in this.RangeVisibilities.Where(x => x.Key == selText).ToList())
            {
                RangeVisibilities[item.Key] = Visibility.Visible;
            }
            foreach (var item in this.RangeVisibilities.Where(x => x.Key != selText).ToList())
            {
                RangeVisibilities[item.Key] = Visibility.Hidden;
            }
            NotifyPropertyChanged("RangeVisibilities");

            if (string.IsNullOrWhiteSpace(selText))
            {
                return;
            }
            this.PickupData = null;
            this.PickupSwitch = true;
            switch (selText)
            {
                case "得意先":
                    this.PickupData = this.Pickup得意先;
                    break;
                case "支払先":
                    this.PickupData = this.Pickup支払先;
                    break;
                case "仕入先":
                    this.PickupData = this.Pickup仕入先;
                    break;
                case "乗務員":
                    this.PickupData = this.Pickup乗務員;
                    break;
                case "車輌":
                    this.PickupData = this.Pickup車輌;
                    break;
                case "車種":
                    this.PickupData = this.Pickup車種;
                    break;
                case "発地":
                    this.PickupData = this.Pickup発地;
                    break;
                case "着地":
                    this.PickupData = this.Pickup着地;
                    break;
                case "商品":
                    this.PickupData = this.Pickup商品;
                    break;
            }
            //spPickupList.ActiveCellPosition = CellPosition.Empty;
        }

        #endregion


        #region LostFocus

        private void 得意先指定_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsExpanded)
            {
                TekiyouDay1.Focus();
                //cmb部門指定.Focus();
            }
            else
            {
                TekiyouDay1.Focus();
            }
            var txt = (sender as UcLabelTwinTextBox);
            if (string.IsNullOrWhiteSpace(txt.ValidationMessage))
            {
                if (string.IsNullOrWhiteSpace(txt.Text1))
                {
                    this.請求内訳条件Enabled = false;
                }
                else
                {
                    this.請求内訳条件Enabled = true;
                }
            }
            else
            {
                this.請求内訳条件Enabled = false;
            }
        }

        #endregion

        #region Changed

        /// <summary>
        /// 締日の入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUpSime_cTextChanged(object sender, RoutedEventArgs e)
        {
            var txtbx = (sender as UcLabelTextBox);
            if (string.IsNullOrWhiteSpace(txtbx.Text))
            {
                return;
            }
            txtbx.CheckValidation();
        }



        private void spPickupList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.PickupSwitch == true)
            {
                // 表示切替直後の時は何もしない。
                this.PickupSwitch = false;
                //spPickupList.ActiveCellPosition = CellPosition.Empty;
                return;
            }
            GcSpreadGrid gcsp = (sender as GcSpreadGrid);
            if (gcsp == null)
            {
                return;
            }
            try
            {
                var row = gcsp.ActiveRow;
                if (row == null)
                {
                    return;
                }
                var colM = gcsp.Columns.Where(x => x.Name == "選択").FirstOrDefault();
                if (colM == null)
                {
                    throw new Exception("システムエラー");
                }
                string val = (string)row.Cells[colM.Index].Value;
                if (string.IsNullOrWhiteSpace(val))
                {
                    row.Cells[colM.Index].Value = SelectedChar;
                }
                else
                {
                    row.Cells[colM.Index].Value = UnselectedChar;
                }
            }
            catch (Exception ex)
            {
            }
            //spPickupList.ActiveCellPosition = CellPosition.Empty;
        }

        #endregion

        private void 得意先指定_TextChanged(object sender, RoutedEventArgs e)
        {
            this.請求内訳ID = string.Empty;
        }

        
        #region KeyDown

        private void PickUpSime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Window_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
		{
			this.sp入金明細データ.InputBindings.Clear();
			this.売上明細データ = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigDLY15010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.表示順 = this.表示順;
                frmcfg.表示順方向 = this.表示順方向;
                //frmcfg.自社部門index = this.cmb部門指定.SelectedIndex;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.sp入金明細データ);

                ucfg.SetConfigValue(frmcfg);
            }
        }
        #endregion

        #region Pickup_Expanded

        private void pickup_Expanded(object sender, RoutedEventArgs e)
        {
            PickupSelect(this.ピックアップ種類);

        }

        #endregion

        #region 表示固定列数

        private void ColumnResert_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.sp入金明細データ, this.sp_Config);
            //Perform_Pickup();
            DataReSort();
            //this.表示固定列数 = this.sp入金明細データ.FrozenColumnCount.ToString();
        }

        #endregion

        #region Sprea SetupSpreadFixedColumn

        private void SetupSpreadFixedColumn(GcSpreadGrid gcsp, string colNum)
        {
            if (string.IsNullOrWhiteSpace(colNum))
            {
                return;
            }
            int cno;
            if (int.TryParse(colNum, out cno) != true)
            {
                return;
            }
            if (cno < 1)
            {
                return;
            }
            gcsp.FrozenColumnCount = cno;
        }

        #endregion

        #region DisplayDetail()

        private void DisplayDetail()
        {
            int rowNo = this.sp入金明細データ.ActiveCellPosition.Row;
            var row = this.sp入金明細データ.Rows[rowNo];
            var mNo = row.Cells[sp入金明細データ.Columns["明細番号"].Index].Value;
            DLY08010 frm = new DLY08010();
            frm.初期明細番号 = (int?)mNo;
			frm.ShowDialog(this);
			if (frm.IsUpdated)
			{
				// 日報側で更新された場合、再検索を実行する
				this.Button_Click_1(null, null);
			}
        }

        #endregion

        private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnKensaku);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }

        #region SPREAD CellEnter

        private void sp入金明細データ_CellEnter(object sender, SpreadCellEnterEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            this._originalText = grid.Cells[e.Row, e.Column].Text;
        }

        #endregion

        string CellName = string.Empty;
        string CellText = string.Empty;
        decimal Cell入金金額 = 0;


        #region SPREAD CellEditEnding

        private void sp入金明細データ_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            CellName = e.CellPosition.ColumnName;
            CellText = sp入金明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;

            Cell入金金額 = AppCommon.DecimalParse(sp入金明細データ.Cells[e.CellPosition.Row, "入金金額"].Text);

			//スプレッドコンボイベント関連付け解除			
			if (sp入金明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = sp入金明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged -= comboEdit_SelectionChanged;
				}
			}

			if (sp入金明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = sp入金明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
				if (gcchk != null)
				{
					gcchk.Checked -= checkEdit_Checked;
					gcchk.Unchecked -= checkEdit_Unchecked;
				}
			}			

        }

        #endregion

		/// <summary>		
		/// comboEdit_SelectionChanged		
		/// スプレッドコンボリストチェンジイベント		
		/// </summary>		
		/// <param name="sender"></param>		
		/// <param name="e"></param>		
		private void comboEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sp入金明細データ.ActiveCell.IsEditing)
			{
				sp入金明細データ.CommitCellEdit();
			}
		}

		private void checkEdit_Checked(object sender, RoutedEventArgs e)
		{
			sp入金明細データ.CommitCellEdit();
		}

		private void checkEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			sp入金明細データ.CommitCellEdit();
		}		


        #region スプレッド登録・修正
        private void sp入金明細データ_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
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
                string cname = e.CellPosition.ColumnName;
                string ctext = sp入金明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                ctext = ctext == null ? string.Empty : ctext;
				if (cname == CellName && ctext == CellText && (!(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType) && !(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)))
				{
					if (CloseFlg) { CloseFlg = false; }
					// セルの値が変化していなければ何もしない
					return;
				}	


                var row = gcsp.Rows[e.CellPosition.Row];
                object val = row.Cells[e.CellPosition.Column].Value;
                val = val == null ? "" : val;
                if (cname.Contains("年月日") == true)
                {
                    AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);
                    cname = cname.Replace("年月日", "日付");
                    DateTime dt;
                    if (DateTime.TryParse(row.Cells[e.CellPosition.Column].Text, out dt) == true)
                    {
                        val = dt;
                    }
                    else
                    {
                        this.ErrorMessage = "正しい日付を入力してください。";
                        return;
                    }
                }

                var colM = gcsp.Columns.Where(x => x.Name == "明細番号").FirstOrDefault();
                if (colM == null)
                {
                    throw new Exception("システムエラー");
                }
                var colL = gcsp.Columns.Where(x => x.Name == "明細行").FirstOrDefault();
                if (colL == null)
                {
                    throw new Exception("システムエラー");
                }

                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, row.Cells[colM.Index].Value, row.Cells[colL.Index].Value, e.CellPosition.ColumnName, val));


                入金合計 += AppCommon.DecimalParse(row.Cells[this.sp入金明細データ.Columns["入金金額"].Index].Value.ToString()) - Cell入金金額;

                //Button_Click_1(null , null);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "入力内容が不正です。";
            }
        }
        #endregion

        #region 検索ボタンフォーカス
        private void KensakuBtnFocus(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnKensaku);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }
        #endregion

        //#region MainWindow_Closed
        //private void MainWindow_Closed(object sender, EventArgs e)
        //{
        //    if (usercfg != null)
        //    {
        //        if (frmcfg == null) { frmcfg = new ConfigDLY15010(); }
        //        //画面が閉じられた時、データを保持する
        //        frmcfg.Top = this.Top;
        //        frmcfg.Left = this.Left;
        //        frmcfg.Height = this.Height;
        //        frmcfg.Width = this.Width;
        //        frmcfg.表示順 = this.表示順;
        //        frmcfg.表示順方向 = this.表示順方向;
        //        frmcfg.区分1 = this.c日付区分.SelectedIndex;
        //        frmcfg.集計期間From = this.適用開始日From;
        //        frmcfg.集計期間To = this.適用開始日To;
        //        usercfg.SetConfigValue(frmcfg);
        //        this.sp入金明細データ = null;
        //        frmcfg.spConfig = AppCommon.SaveSpConfig(this.sp入金明細データ);

        //        usercfg.SetConfigValue(frmcfg);
        //    }

            
        //}
        //#endregion

		//private void SortItemCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	DataReSort();
		//}

        private void sp入金明細データ_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete && sp入金明細データ.EditElement == null)
			{
				e.Handled = true;
			}
			if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
			{
				e.Handled = true;
			}
        }

		private void sp入金明細データ_CellBeginEdit(object sender, SpreadCellBeginEditEventArgs e)
		{
			EditFlg = true;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (sp入金明細データ.ActiveCell != null && sp入金明細データ.ActiveCell.IsEditing)
			{
				CloseFlg = true;
				sp入金明細データ.CommitCellEdit();
				if (CloseFlg) { e.Cancel = true; }
				return;
			}		

		}

		/// <summary>	
		/// sp売上明細データ_EditElementShowing	
		/// スプレッドコンボイベント関連付け	
		/// デザイン画面でイベント追加	
		/// </summary>	
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void sp入金明細データ_EditElementShowing(object sender, EditElementShowingEventArgs e)
		{
			if (e.EditElement is GrapeCity.Windows.SpreadGrid.Editors.GcComboBox)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = e.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged += comboEdit_SelectionChanged;
				}
			}

			if (e.EditElement is GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = e.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
				if (gcchk != null)
				{
					gcchk.Checked += checkEdit_Checked;
					gcchk.Unchecked += checkEdit_Unchecked;
				}
			}
		}

		private void sp入金明細データ_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
		{
			if (sp入金明細データ.Columns[1].Name == null)
			{
				return;
			}
			if (sp入金明細データ.Rows.Count() > 0)
			{
				Summary();
			}
		}				


    }
}