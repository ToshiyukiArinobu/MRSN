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
    /// <summary>
    /// 運転日報入力
    /// </summary>
    public partial class DLY02015 : RibbonWindowViewBase
    {
        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            Dispatcher.PushFrame(frame);
        }

        public object ExitFrames(object frames)
        {
            ((DispatcherFrame)frames).Continue = false;
            return null;
        }

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
        public class ConfigDLY02015 : FormConfigBase
        {
            public byte[] spUriageConfig = null;
			public byte[] spKeihiConfig = null;
			public byte[] spRirekiConfig20160419 = null;
			public int? 表示順指定 = null;
			public int? 担当者指定 = null;
			public int? 得意先指定 = null;
			public int 番号通知区分 = 1;
			public int 最終伝票表示区分 = 0;
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY02015 frmcfg = null;

        #endregion

        // SPREAD初期状態保存用
        private byte[] spUriageConfig = null;
		private byte[] spKeihiConfig = null;
		private byte[] spRirekiConfig = null;

		#region Const
		private const string TRN_RIREKI = "DLY02015_TRN_RIREKI";
        private const string GET_CNTL = "M87_CNTL";
        private const string GetMaxNo = "DLY01010_MAXNO";
        private const string GetMeisaiNo = "DLY02015_GETNO";
        private const string PutAllData = "DLY02015_PUTALL";
        private const string DeleteAllData = "DLY02015_DELALL";
        private const string GetTanka = "DLY02015_TANKA";
        private const string GetTanka2 = "DLY02015_TANKA2";
        private const string GetNippou = "DLY02015_1";
        //private const string GetKeihiLog = "DLY02015_3";
        //private const string GetKeihiDef = "DLY02015_DEFAULT_K";

        private const string GetCARDATA = "M05_CAR_UC";
        private const string GetCARDATA2 = "M05_CAR_UC2";
        private const string GetHINDATA = "M09_HIN_UC";
        private const string GetTOKDATA = "M_M01_TOK";
        private const string GetYOSDATA = "M_M01_YOS";
        private const string GetMaxMeisaiNo = "DLY02015_GetMaxMeisaiNo";
        #endregion

		public class DLY02015_RIREKI : INotifyPropertyChanged
		{
			private DateTime _請求日付 { get; set; }
			private string _得意先名 { get; set; }
			private string _発地名 { get; set; }
			private string _着地名 { get; set; }
			private string _商品名 { get; set; }
			private decimal _数量 { get; set; }
			private decimal _重量 { get; set; }
			private string _車番 { get; set; }
			private string _乗務員 { get; set; }
			private string _支払先名 { get; set; }
			private int _売上金額 { get; set; }
			private int _通行料 { get; set; }
			private int _支払金額 { get; set; }
			private int _支払通行料 { get; set; }
			private int _明細番号 { get; set; }
			private int _行 { get; set; }

			public DateTime 請求日付 { get { return _請求日付; } set { _請求日付 = value; NotifyPropertyChanged(); } }
			public string 得意先名 { get { return _得意先名; } set { _得意先名 = value; NotifyPropertyChanged(); } }
			public string 発地名 { get { return _発地名; } set { _発地名 = value; NotifyPropertyChanged(); } }
			public string 着地名 { get { return _着地名; } set { _着地名 = value; NotifyPropertyChanged(); } }
			public string 商品名 { get { return _商品名; } set { _商品名 = value; NotifyPropertyChanged(); } }
			public decimal 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
			public decimal 重量 { get { return _重量; } set { _重量 = value; NotifyPropertyChanged(); } }
			public string 車番 { get { return _車番; } set { _車番 = value; NotifyPropertyChanged(); } }
			public string 乗務員 { get { return _乗務員; } set { _乗務員 = value; NotifyPropertyChanged(); } }
			public string 支払先名 { get { return _支払先名; } set { _支払先名 = value; NotifyPropertyChanged(); } }
			public int 売上金額 { get { return _売上金額; } set { _売上金額 = value; NotifyPropertyChanged(); } }
			public int 通行料 { get { return _通行料; } set { _通行料 = value; NotifyPropertyChanged(); } }
			public int 支払金額 { get { return _支払金額; } set { _支払金額 = value; NotifyPropertyChanged(); } }
			public int 支払通行料 { get { return _支払通行料; } set { _支払通行料 = value; NotifyPropertyChanged(); } }
			public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
			public int 行 { get { return _行; } set { _行 = value; NotifyPropertyChanged(); } }

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

        public class TRN : INotifyPropertyChanged
        {
            public int _明細番号;
            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int _明細行;
            public int 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public DateTime? _登録日時;
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? _更新日時;
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public int _明細区分;
            public int 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }
            public int _入力区分;
            public int 入力区分 { get { return _入力区分; } set { _入力区分 = value; NotifyPropertyChanged(); } }
            public DateTime? _請求日付;
            public DateTime? 請求日付 { get { return _請求日付; } set { _請求日付 = value; NotifyPropertyChanged(); } }
            public DateTime? _支払日付;
            public DateTime? 支払日付 { get { return _支払日付; } set { _支払日付 = value; NotifyPropertyChanged(); } }
            public DateTime? _配送日付;
            public DateTime? 配送日付 { get { return _配送日付; } set { _配送日付 = value; NotifyPropertyChanged(); } }
            public decimal? _配送時間;
            public decimal? 配送時間 { get { return _配送時間; } set { _配送時間 = value; NotifyPropertyChanged(); } }
            public int? _得意先ID;
            public int? 得意先ID { get { return _得意先ID; } set { _得意先ID = value; NotifyPropertyChanged(); } }
            public int? _請求内訳ID;
            public int? 請求内訳ID { get { return _請求内訳ID; } set { _請求内訳ID = value == 0 ? null : value; NotifyPropertyChanged(); } }
            public int? _車輌ID;
            public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public int? _車種ID;
            public int? 車種ID { get { return _車種ID; } set { _車種ID = value; NotifyPropertyChanged(); } }
            public int? _支払先ID;
            public int? 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
            public int? _乗務員ID;
            public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
            public int? _自社部門ID;
            public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value == 0 ? null : value; NotifyPropertyChanged(); } }
            public string _車輌番号;
            public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
            public string _支払先名２次;
            public string 支払先名２次 { get { return _支払先名２次; } set { _支払先名２次 = value; NotifyPropertyChanged(); } }
            public string _実運送乗務員;
            public string 実運送乗務員 { get { return _実運送乗務員; } set { _実運送乗務員 = value; NotifyPropertyChanged(); } }
            public string _乗務員連絡先;
            public string 乗務員連絡先 { get { return _乗務員連絡先; } set { _乗務員連絡先 = value; NotifyPropertyChanged(); } }
            public int? _請求運賃計算区分ID;
            public int? 請求運賃計算区分ID { get { return _請求運賃計算区分ID; } set { _請求運賃計算区分ID = value; NotifyPropertyChanged(); } }
            public int? _支払運賃計算区分ID;
            public int? 支払運賃計算区分ID { get { return _支払運賃計算区分ID; } set { _支払運賃計算区分ID = value; NotifyPropertyChanged(); } }
            public decimal? _数量;
            public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            public string _単位;
            public string 単位 { get { return _単位; } set { _単位 = value; NotifyPropertyChanged(); } }
            public decimal? _重量;
            public decimal? 重量 { get { return _重量; } set { _重量 = value; NotifyPropertyChanged(); } }
            public int? _走行ＫＭ;
            public int? 走行ＫＭ { get { return _走行ＫＭ; } set { _走行ＫＭ = value; NotifyPropertyChanged(); } }
            public int? _実車ＫＭ;
            public int? 実車ＫＭ { get { return _実車ＫＭ; } set { _実車ＫＭ = value; NotifyPropertyChanged(); } }
            public decimal? _待機時間;
            public decimal? 待機時間 { get { return _待機時間; } set { _待機時間 = value; NotifyPropertyChanged(); } }
            public decimal? _売上単価;
            public decimal? 売上単価 { get { return _売上単価; } set { _売上単価 = value; NotifyPropertyChanged(); } }
            public int? _売上金額;
            public int? 売上金額 { get { return _売上金額; } set { _売上金額 = value; NotifyPropertyChanged(); } }
            public int? _通行料;
            public int? 通行料 { get { return _通行料; } set { _通行料 = value; NotifyPropertyChanged(); } }
            public int? _請求割増１;
            public int? 請求割増１ { get { return _請求割増１; } set { _請求割増１ = value; NotifyPropertyChanged(); } }
            public int? _請求割増２;
            public int? 請求割増２ { get { return _請求割増２; } set { _請求割増２ = value; NotifyPropertyChanged(); } }
            public int? _請求消費税;
            public int? 請求消費税 { get { return _請求消費税; } set { _請求消費税 = value; NotifyPropertyChanged(); } }
            public decimal? _支払単価;
            public decimal? 支払単価 { get { return _支払単価; } set { _支払単価 = value; NotifyPropertyChanged(); } }
            public int? _支払金額;
            public int? 支払金額 { get { return _支払金額; } set { _支払金額 = value; NotifyPropertyChanged(); } }
            public int? _支払通行料;
            public int? 支払通行料 { get { return _支払通行料; } set { _支払通行料 = value; NotifyPropertyChanged(); } }
            public int? _支払割増１;
            public int? 支払割増１ { get { return _支払割増１; } set { _支払割増１ = value; NotifyPropertyChanged(); } }
            public int? _支払割増２;
            public int? 支払割増２ { get { return _支払割増２; } set { _支払割増２ = value; NotifyPropertyChanged(); } }
            public int? _支払消費税;
            public int? 支払消費税 { get { return _支払消費税; } set { _支払消費税 = value; NotifyPropertyChanged(); } }
            public int? _水揚金額;
            public int? 水揚金額 { get { return _水揚金額; } set { _水揚金額 = value; NotifyPropertyChanged(); } }
            public int? _社内区分;
            public int? 社内区分 { get { return _社内区分; } set { _社内区分 = value; NotifyPropertyChanged(); } }
            public int? _請求税区分;
            public int? 請求税区分 { get { return _請求税区分; } set { _請求税区分 = value; NotifyPropertyChanged(); } }
            public int? _支払税区分;
            public int? 支払税区分 { get { return _支払税区分; } set { _支払税区分 = value; NotifyPropertyChanged(); } }
            public int? _売上未定区分;
            public int? 売上未定区分 { get { return _売上未定区分; } set { _売上未定区分 = value; NotifyPropertyChanged(); } }
            public int? _支払未定区分;
            public int? 支払未定区分 { get { return _支払未定区分; } set { _支払未定区分 = value; NotifyPropertyChanged(); } }
            public int? _商品ID;
            public int? 商品ID { get { return _商品ID; } set { _商品ID = value; NotifyPropertyChanged(); } }
            public string _商品名;
            public string 商品名 { get { return _商品名; } set { _商品名 = value; NotifyPropertyChanged(); } }
            public int? _発地ID;
            public int? 発地ID { get { return _発地ID; } set { _発地ID = value; NotifyPropertyChanged(); } }
            public string _発地名;
            public string 発地名 { get { return _発地名; } set { _発地名 = value; NotifyPropertyChanged(); } }
            public int? _着地ID;
            public int? 着地ID { get { return _着地ID; } set { _着地ID = value; NotifyPropertyChanged(); } }
            public string _着地名;
            public string 着地名 { get { return _着地名; } set { _着地名 = value; NotifyPropertyChanged(); } }
            public int? _請求摘要ID;
            public int? 請求摘要ID { get { return _請求摘要ID; } set { _請求摘要ID = value; NotifyPropertyChanged(); } }
            public string _請求摘要;
            public string 請求摘要 { get { return _請求摘要; } set { _請求摘要 = value; NotifyPropertyChanged(); } }
            public int? _社内備考ID;
            public int? 社内備考ID { get { return _社内備考ID; } set { _社内備考ID = value; NotifyPropertyChanged(); } }
            public string _社内備考;
            public string 社内備考 { get { return _社内備考; } set { _社内備考 = value; NotifyPropertyChanged(); } }
            public int? _入力者ID;
            public int? 入力者ID { get { return _入力者ID; } set { _入力者ID = value; NotifyPropertyChanged(); } }
            public string _得意先名;
            public string 得意先名 { get { return _得意先名; } set { _得意先名 = value; NotifyPropertyChanged(); } }
            public string _支払先名;
            public string 支払先名 { get { return _支払先名; } set { _支払先名 = value; NotifyPropertyChanged(); } }
            public string _乗務員名;
            public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
            public string _経費名称;
            public string 経費名称 { get { return _経費名称; } set { _経費名称 = value; NotifyPropertyChanged(); } }
            public string _請求内訳名;
            public string 請求内訳名 { get { return _請求内訳名; } set { _請求内訳名 = value; NotifyPropertyChanged(); } }
            public int? _確認名称区分;
            public int? 確認名称区分 { get { return _確認名称区分; } set { _確認名称区分 = value; NotifyPropertyChanged(); } }
            public string _備考;
            public string 備考 { get { return _備考; } set { _備考 = value; NotifyPropertyChanged(); } }
            public DateTime? _労務日;
            public DateTime? 労務日 { get { return _労務日; } set { _労務日 = value; NotifyPropertyChanged(); } }
            public int? _請求内訳管理区分;
            public int? 請求内訳管理区分 { get { return _請求内訳管理区分; } set { _請求内訳管理区分 = value; NotifyPropertyChanged(); } }

            public TRN()
            {
                this.請求運賃計算区分ID = 0;
                this.支払運賃計算区分ID = 0;
                this.社内区分 = 0;
                this.請求税区分 = 0;
                this.支払税区分 = 0;
                this.売上未定区分 = 0;
                this.支払未定区分 = 0;
                this.確認名称区分 = 0;

            }

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


        // SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
        string _originalText = null;

		public int? 初期明細番号 = null;
		public int? 初期行番号 = null;
		public bool IsUpdated = false;
		public int? 金額計算端数処理区分 = null;
		public int 未定区分 = 0;
		public DateTime 初期日付 = DateTime.Now;

        public int 担当者ID;

        #region BindingMember

        private Visibility _類似番号入力表示 = Visibility.Collapsed;
        public Visibility 類似番号入力表示
        {
            get { return _類似番号入力表示; }
            set { _類似番号入力表示 = value; NotifyPropertyChanged(); }
        }

		private string _RefferNumber;
		public string RefferNumber
		{
			get { return _RefferNumber; }
			set { _RefferNumber = value; NotifyPropertyChanged(); }
		}

		private string _RefferGyou;
		public string RefferGyou
		{
			get { return _RefferGyou; }
			set { _RefferGyou = value; NotifyPropertyChanged(); }
		}

		private DataTable _dUriageData = null;
        private DataTable _dLogData = null;
        private DataTable _dKeihiData = null;
        [BindableAttribute(true)]
        public DataTable DUriageData
        {
            get
            {
                return this._dUriageData;
            }
            set
            {
                this._dUriageData = value;
                if (value == null)
                {
                    this._dInputData = null;
                }
                else
                {
                    _dInputData = value.Clone();
                    this.運転日報データ = _dInputData.NewRow();
                    _dInputData.Rows.Add(this.運転日報データ);
                }
                NotifyPropertyChanged();
            }
        }

        public DataTable DLogData
        {
            get
            {
                return this._dLogData;
            }
            set
            {
                this._dLogData = value;
                NotifyPropertyChanged();
            }
        }

        private TRN _cInputTrn = new TRN();
        public TRN CInputTrn
        {
            get { return this._cInputTrn; }
            set
            {
                _cInputTrn = value;
                NotifyPropertyChanged();
            }
        }


        private DataTable _dInputData = new DataTable();
        DataRow _運転日報データ = null;
        public DataRow 運転日報データ
        {
            get { return this._運転日報データ; }
            set
            {
                this._運転日報データ = value;
                NotifyPropertyChanged();
                if (value == null)
                {
                    CInputTrn = new TRN();
                }
                else
                {
                    List<TRN> list = new List<TRN>();
                    list = (List<TRN>)AppCommon.ConvertFromDataTable(list.GetType(), value);
                    if (list.Count > 0)
                    {
                        CInputTrn = list[0];
                    }
                    CInputTrn.請求運賃計算区分ID = CInputTrn.請求運賃計算区分ID == null ? 0 : CInputTrn.請求運賃計算区分ID;
                    CInputTrn.支払運賃計算区分ID = CInputTrn.支払運賃計算区分ID == null ? 0 : CInputTrn.支払運賃計算区分ID;
                    CInputTrn.社内区分 = CInputTrn.社内区分 == null ? 0 : CInputTrn.社内区分;
                    CInputTrn.請求税区分 = CInputTrn.請求税区分 == null ? 0 : CInputTrn.請求税区分;
                    CInputTrn.支払税区分 = CInputTrn.支払税区分 == null ? 0 : CInputTrn.支払税区分;
                    CInputTrn.売上未定区分 = CInputTrn.売上未定区分 == null ? 0 : CInputTrn.売上未定区分;
                    CInputTrn.支払未定区分 = CInputTrn.支払未定区分 == null ? 0 : CInputTrn.支払未定区分;
                    CInputTrn.確認名称区分 = CInputTrn.確認名称区分 == null ? 0 : CInputTrn.確認名称区分;

                }
            }
        }

        public DataTable DKeihiData
        {
            get
            {
                return this._dKeihiData;
            }
            set
            {
                this._dKeihiData = value;
                NotifyPropertyChanged();
            }
        }

		private string _detailsNumber = string.Empty;
		[BindableAttribute(true)]
		public string DetailsNumber
		{
			get
			{
				return this._detailsNumber;
			}
			set
			{
				this._detailsNumber = value;
				NotifyPropertyChanged();
			}
		}

		private string _detailsGyou = string.Empty;
		[BindableAttribute(true)]
		public string DetailsGyou
		{
			get
			{
				return this._detailsGyou;
			}
			set
			{
				this._detailsGyou = value;
				NotifyPropertyChanged();
			}
		}

        private string changedColumns = string.Empty;
        public string ChangedColumns
        {
            get
            {
                return this.changedColumns;
            }
            set
            {
                this.changedColumns = value;
                NotifyPropertyChanged();
            }
        }

		private List<DLY02015_RIREKI> _売上明細データ = null;
		public List<DLY02015_RIREKI> 売上明細データ
		{
			get
			{
				return this._売上明細データ;
			}
			set
			{
				this._売上明細データ = value;
				NotifyPropertyChanged();
			}
		}

        #endregion

        private int? _登録件数 = null;
        public int? 登録件数
        {
            get { return _登録件数; }
            set { _登録件数 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _運行開始日 = null;
        public DateTime? 運行開始日
        {
            get { return _運行開始日; }
            set { _運行開始日 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _運行終了日 = null;
        public DateTime? 運行終了日
        {
            get { return _運行終了日; }
            set { _運行終了日 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _勤務日FROM = null;
        public DateTime? 勤務日FROM
        {
            get { return _勤務日FROM; }
            set { _勤務日FROM = value; NotifyPropertyChanged(); }
        }
        private DateTime? _勤務日TO = null;
        public DateTime? 勤務日TO
        {
            get { return _勤務日TO; }
            set { _勤務日TO = value; NotifyPropertyChanged(); }
        }
        private DateTime? _労務日 = null;
        public DateTime? 労務日
        {
            get { return _労務日; }
            set { _労務日 = value; NotifyPropertyChanged(); }
        }

		private int? _表示順 = 0;
		public int? 表示順
		{
			get { return _表示順; }
			set { _表示順 = value; NotifyPropertyChanged(); }
		}
		private int? _担当者ID指定 = null;
		public int? 担当者ID指定
		{
			get { return _担当者ID指定; }
			set { _担当者ID指定 = value; NotifyPropertyChanged(); }
		}
		private string _担当者名指定 = "";
		public string 担当者名指定
		{
			get { return _担当者名指定; }
			set { _担当者名指定 = value; NotifyPropertyChanged(); }
		}
		private int? _得意先ID指定 = null;
		public int? 得意先ID指定
		{
			get { return _得意先ID指定; }
			set { _得意先ID指定 = value; NotifyPropertyChanged(); }
		}
		private string _得意先名指定 = "";
		public string 得意先名指定
		{
			get { return _得意先名指定; }
			set { _得意先名指定 = value; NotifyPropertyChanged(); }
		}

		private int _番号通知区分 = 1;
		public int 番号通知区分
		{
			get { return _番号通知区分; }
			set { _番号通知区分 = value; NotifyPropertyChanged(); }
		}
		private int _最終伝票表示区分 = 0;
		public int 最終伝票表示区分
		{
			get { return _最終伝票表示区分; }
			set { _最終伝票表示区分 = value; NotifyPropertyChanged(); }
		}
		
        private Visibility _日付表示モード = Visibility.Visible;
        public Visibility 日付表示モード
        {
            get { return this._日付表示モード; }
            set { this._日付表示モード = value; NotifyPropertyChanged(); }
        }

        bool _内訳Enabled = false;
        public bool 内訳Enabled
        {
            get { return this._内訳Enabled; }
            set
            {

                UIElement element = Keyboard.FocusedElement as UIElement;

                this._内訳Enabled = value; NotifyPropertyChanged();
                if (_内訳Enabled == true)
                {
                    Se_SeikyuuUtiwakeNm.IsRequired = true;
                }
                else
                {
                    var twintxt = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(element as Control);

                    Se_SeikyuuUtiwakeNm.IsRequired = false;
                    Se_SeikyuuUtiwakeNm.SetValidationMessage1(string.Empty);
                    Se_SeikyuuUtiwakeNm.SetValidationMessage2(string.Empty);

                    //中村一時的な修正
                    if (twintxt == null)
                    {
                        return;
                    }
                    if (twintxt.Name == "Se_SeikyuuUtiwakeNm")
                    {
                        FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            }
        }

        string _内訳検索用得意先ID = string.Empty;
        public string 内訳検索用得意先ID
        {
            get { return this._内訳検索用得意先ID; }
            set { this._内訳検索用得意先ID = value; NotifyPropertyChanged(); }
        }

        #region 入力用ワーク（バインド用）
        private DateTime? _登録日時 = null;
        public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
        private DateTime? _請求日付 = null;
        public DateTime? 請求日付 { get { return _請求日付; } set { _請求日付 = value; NotifyPropertyChanged(); } }
        private decimal? _配送時間 = null;
        public decimal? 配送時間 { get { return _配送時間; } set { _配送時間 = value; NotifyPropertyChanged(); } }
        private int? _得意先ID = null;
        public int? 得意先ID { get { return _得意先ID; } set { _得意先ID = value; NotifyPropertyChanged(); } }
        private int? _請求内訳ID = null;
        public int? 請求内訳ID { get { return _請求内訳ID; } set { _請求内訳ID = value; NotifyPropertyChanged(); } }
        private int? _発地ID = null;
        public int? 発地ID { get { return _発地ID; } set { _発地ID = value; NotifyPropertyChanged(); } }
        private string _発地名 = null;
        public string 発地名 { get { return _発地名; } set { _発地名 = value; NotifyPropertyChanged(); } }
        private int? _着地ID = null;
        public int? 着地ID { get { return _着地ID; } set { _着地ID = value; NotifyPropertyChanged(); } }
        private string _着地名 = null;
        public string 着地名 { get { return _着地名; } set { _着地名 = value; NotifyPropertyChanged(); } }
        private int? _商品ID = null;
        public int? 商品ID { get { return _商品ID; } set { _商品ID = value; NotifyPropertyChanged(); } }
        private string _商品名 = null;
        public string 商品名 { get { return _商品名; } set { _商品名 = value; NotifyPropertyChanged(); } }

        private int? _車輌ID = null;
        public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
        private string _車輌番号 = null;
        public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
        private int? _車種ID = null;
        public int? 車種ID { get { return _車種ID; } set { _車種ID = value; NotifyPropertyChanged(); } }
        private int? _乗務員ID = null;
        public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
        private int? _請求摘要ID = null;
        public int? 請求摘要ID { get { return _請求摘要ID; } set { _請求摘要ID = value; NotifyPropertyChanged(); } }
        private string _請求摘要 = null;
        public string 請求摘要 { get { return _請求摘要; } set { _請求摘要 = value; NotifyPropertyChanged(); } }
        private int? _社内備考ID = null;
        public int? 社内備考ID { get { return _社内備考ID; } set { _社内備考ID = value; NotifyPropertyChanged(); } }
        private string _社内備考 = null;
        public string 社内備考 { get { return _社内備考; } set { _社内備考 = value; NotifyPropertyChanged(); } }

        private decimal? _数量 = null;
        public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
        private string _単位 = null;
        public string 単位 { get { return _単位; } set { _単位 = value; NotifyPropertyChanged(); } }
        private decimal? _重量 = null;
        public decimal? 重量 { get { return _重量; } set { _重量 = value; NotifyPropertyChanged(); } }
        private int? _走行ＫＭ = null;
        public int? 走行ＫＭ { get { return _走行ＫＭ; } set { _走行ＫＭ = value; NotifyPropertyChanged(); } }
        private int? _実車ＫＭ = null;
        public int? 実車ＫＭ { get { return _実車ＫＭ; } set { _実車ＫＭ = value; NotifyPropertyChanged(); } }
        private decimal? _待機時間 = null;
        public decimal? 待機時間 { get { return _待機時間; } set { _待機時間 = value; NotifyPropertyChanged(); } }

        private int? _請求運賃計算区分ID = null;
        public int? 請求運賃計算区分ID { get { return _請求運賃計算区分ID; } set { _請求運賃計算区分ID = value; NotifyPropertyChanged(); } }
        private decimal? _売上単価 = null;
        public decimal? 売上単価 { get { return _売上単価; } set { _売上単価 = value; NotifyPropertyChanged(); } }
        private int? _売上金額 = null;
        public int? 売上金額 { get { return _売上金額; } set { _売上金額 = value; NotifyPropertyChanged(); } }
        private int? _通行料 = null;
        public int? 通行料 { get { return _通行料; } set { _通行料 = value; NotifyPropertyChanged(); } }
        private int? _請求割増１ = null;
        public int? 請求割増１ { get { return _請求割増１; } set { _請求割増１ = value; NotifyPropertyChanged(); } }
        private int? _請求割増２ = null;
        public int? 請求割増２ { get { return _請求割増２; } set { _請求割増２ = value; NotifyPropertyChanged(); } }

        private int? _社内区分 = null;
        public int? 社内区分 { get { return _社内区分; } set { _社内区分 = value; NotifyPropertyChanged(); } }
        private int? _売上未定区分 = null;
        public int? 売上未定区分 { get { return _売上未定区分; } set { _売上未定区分 = value; NotifyPropertyChanged(); } }
        private int? _請求税区分 = null;
        public int? 請求税区分 { get { return _請求税区分; } set { _請求税区分 = value; NotifyPropertyChanged(); } }

        public int? _確認名称区分;
        public int? 確認名称区分 { get { return _確認名称区分; } set { _確認名称区分 = value; NotifyPropertyChanged(); } }

        private int? _自社部門ID = null;
        public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value; NotifyPropertyChanged(); } }


        private string _乗務員名 = null;
        public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
        private string _経費名称 = null;
        public string 経費名称 { get { return _経費名称; } set { _経費名称 = value; NotifyPropertyChanged(); } }

        private int? _請求路線計算年度 = null;
        public int? 請求路線計算年度 { get { return _請求路線計算年度; } set { _請求路線計算年度 = value; NotifyPropertyChanged(); } }
        private int? _支払路線計算年度 = null;
        public int? 支払路線計算年度 { get { return _支払路線計算年度; } set { _支払路線計算年度 = value; NotifyPropertyChanged(); } }

        #endregion

        bool IsRfferMode = false;
        bool IsGridWorking = false;

        #region 明細クリック時のアクション定義

        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd経費削除 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd経費削除(GcSpreadGrid gcSpreadGrid)
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
                    this._gcSpreadGrid.CancelRowEdit();
                    this._gcSpreadGrid.Rows.Remove(cellCommandParameter.CellPosition.Row);
                }
            }
        }

        #endregion


        void SpreadResetSelection(GcSpreadGrid spgrid)
        {
            var opmode = spgrid.OperationMode;
            spgrid.ResetSelection();
            spgrid.OperationMode = OperationMode.Normal;
            spgrid.ActiveCellPosition = CellPosition.Empty;
            spgrid.OperationMode = opmode;
        }

        enum DataGetMode
        {
            first,
            last,
            previous,
            next,
            number,
        }
        DataGetMode datagetmode;

        /// <summary>
        /// 運転日報入力
        /// </summary>
        public DLY02015()
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

			this.spRireki.Rows.Clear();
			Grid_RIREKI.Visibility = Visibility.Collapsed;

			this.spRireki.RowCount = 0;
			this.spRirekiConfig = AppCommon.SaveSpConfig(this.spRireki);

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
            frmcfg = (ConfigDLY02015)ucfg.GetConfigValue(typeof(ConfigDLY02015));

            担当者ID = ccfg.ユーザID;

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY02015();
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
			if (frmcfg.spRirekiConfig20160419 != null)
			{
				AppCommon.LoadSpConfig(this.spRireki, frmcfg.spRirekiConfig20160419, "売上明細データ");
			}

			//担当者ID指定 = frmcfg.担当者指定;
			//得意先ID指定 = frmcfg.得意先指定;
			表示順 = frmcfg.表示順指定;
			番号通知区分 = frmcfg.番号通知区分;
			最終伝票表示区分 = frmcfg.最終伝票表示区分;
			#endregion


            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { typeof(MST04010), typeof(SCH04010) });
            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { typeof(MST10010), typeof(SCH10010) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST12010), typeof(SCH12010) });
            base.MasterMaintenanceWindowList.Add("M01_TOK_SHIHARAI_SCH", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { typeof(MST06010), typeof(SCH06010) });
            base.MasterMaintenanceWindowList.Add("M06_SYA", new List<Type> { typeof(MST05010), typeof(SCH05010) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST07010), typeof(SCH07010) });
            base.MasterMaintenanceWindowList.Add("M08_TIK_UC", new List<Type> { typeof(MST03010), typeof(SCH03010) });
            base.MasterMaintenanceWindowList.Add("M10_UHK_UC", new List<Type> { typeof(MST02010), typeof(SCH02020) });
			base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCH08010) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });


            GetComboBoxItems();

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CNTL, 1, 0));

			//DoEvents();

            ScreenClear();
            ChangeKeyItemChangeable(true);
            Txt登録件数.Focusable = false;
            SetFocusToTopControl();

            if (初期明細番号 != null)
            {
				DetailsNumber = 初期明細番号.ToString();
				DetailsGyou = 初期行番号.ToString();
				類似番号入力表示 = System.Windows.Visibility.Collapsed;
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                GetMeisaiData();
				return;
			};
			if (最終伝票表示区分 == 1)
			{
				datagetmode = DataGetMode.last;
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0, 3));
			}
        }

        private void spKeihi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.F1)
            {
                e.Handled = true;
            }
        }

        void spUriage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// コンボボックスのアイテムをDBから取得
        /// </summary>
        private void GetComboBoxItems()
        {
            AppCommon.SetutpComboboxList(this.cmb計算区分);
            AppCommon.SetutpComboboxList(this.cmb支払計算区分);
            AppCommon.SetutpComboboxList(this.cmb税区分);
            AppCommon.SetutpComboboxList(this.cmb社内区分);
            AppCommon.SetutpComboboxList(this.cmb未定区分);
            AppCommon.SetutpComboboxList(this.cmb支払税区分);
            AppCommon.SetutpComboboxList(this.cmb支払未定区分);
            AppCommon.SetutpComboboxList(this.cmb確認名称);
        }


        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                #region
                this.ErrorMessage = string.Empty;
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    case GET_CNTL:
                        this.Se_Warimasi1.Label_Context = AppCommon.GetWarimasiName1(tbl);
                        this.Se_Warimasi2.Label_Context = AppCommon.GetWarimasiName2(tbl);
						金額計算端数処理区分 = (int)tbl.Rows[0]["金額計算端数区分"];
						未定区分 = (int)tbl.Rows[0]["未定区分"];

                        break;
                    case GetMaxNo:
                        if (data is int)
                        {
                            int maxno = (int)data;
                            if (maxno < 0)
                            {
                                MessageBox.Show(string.Format("明細番号を取得できません。"));
                                return;
                            }
                            this.DetailsNumber = maxno.ToString();
							this.DetailsGyou = "1";
                            類似番号入力表示 = System.Windows.Visibility.Visible;
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            GetMeisaiData();
                        }
                        break;
                    case GetMeisaiNo:
                        if (data == null)
                        {
                            return;
                        }
                        DataTable dt = (DataTable)data;
					    
						int no = dt.Rows.Count;
						if (no < 1)
						{
							return;
						}
						if((int)dt.Rows[0]["明細番号"] == 0)
                        {
                            // データが無かった場合
                            switch (datagetmode)
                            {
                                case DataGetMode.first:
                                case DataGetMode.last:
                                    this.ErrorMessage = "データがありません。";
                                    break;
                                case DataGetMode.previous:
                                    this.ErrorMessage = "先頭のデータです。";
                                    break;
                                case DataGetMode.next:
                                    this.ErrorMessage = "最後のデータです。";
                                    break;
                                case DataGetMode.number:
                                    break;
                            }
                            return;
                        }
                        if (no.ToString() != this.DetailsNumber)
                        {
							this.DetailsNumber = dt.Rows[0]["明細番号"].ToString();
							this.DetailsGyou = dt.Rows[0]["明細行"].ToString();
							類似番号入力表示 = System.Windows.Visibility.Collapsed;
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            GetMeisaiData();
                        }
                        break;
                    case GetNippou:

						if (tbl == null)
						{
                            return;
                        }

                        if (tbl.Rows.Count == 0)
                        {
                            if (this.MaintenanceMode != AppConst.MAINTENANCEMODE_ADD)
                            {
                                this.He_MeisaiBangou.Focus();
                                this.ErrorMessage = "該当する明細番号はありません。";
                                return;
                            }
                            if (IsRfferMode)
                            {
                                this.He_RuijiMeisaiBangou.Focus();
                                this.ErrorMessage = "該当する明細番号はありません。";
                                return;
                            }
                        }
                        
                        DUriageData = tbl.Copy();
                        DataTable Ditigyoume = null;
                        
                        int 入力者ID;
                        if (DUriageData.Rows.Count > 0)
                        {
                            if (int.TryParse(DUriageData.Rows[0]["入力者ID"].ToString(), out 入力者ID))
                            {
                                担当者ID = 入力者ID;
                            }
                        }

                        if (tbl.Rows.Count != 0)
                        {
                            DataRow[] rows = DUriageData.Select("明細行  = '1' ");
                            Array.ForEach<DataRow>(rows, row => DUriageData.Rows.Remove(row));

                            rows.Initialize();
                            rows = tbl.Select("明細行 = '1'");
                            Ditigyoume = tbl.Clone();
                            Ditigyoume.ImportRow(rows[0]);
                            //Ditigyoume.Rows.Add(rows[0]);
                            Ditigyoume.AcceptChanges();
                        }
                        else
                        {
                            Ditigyoume = tbl;
                            DUriageData = tbl;
                        }


                        //if (Ditigyoume == null)
                        //{
                        //    this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                        //    return;
                        //}

						ClearUriageItems();
						if (DUriageData.Rows.Count > 0)
						{

							int gyo = (int)DUriageData.Rows[DUriageData.Rows.Count - 1]["明細行"];
							//this.LineNumber = gyo + 1;


							_dInputData.Rows.Clear();
							DoEvents();
							_dInputData.ImportRow(DUriageData.Rows[0]);
							this.運転日報データ = _dInputData.Rows[0];
							this.内訳検索用得意先ID = this.CInputTrn.得意先ID.ToString();
							if (this.CInputTrn.請求内訳管理区分 == null)
							{
								this.内訳Enabled = false;
							}
							else
							{
								this.内訳Enabled = ((int)this.CInputTrn.請求内訳管理区分 == 1) ? true : false;
							}


							ChangeKeyItemChangeable(false);

							DataRow row = Ditigyoume.Rows[0];

							Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
							Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
							Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
							He_SyaryouBangou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
							Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
							Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;

							Se_TantouBumon.Text1 = null;
							Se_TokuiSakiNm.Text1 = null;
							He_SyashuNm.Text1 = null;
							txt乗務員.Text1 = null;

							this.請求日付 = row.IsNull("請求日付") ? (DateTime?)null : (DateTime?)row["請求日付"];
							this.配送時間 = row.IsNull("配送時間") ? (decimal?)null : (decimal?)row["配送時間"];
							this.自社部門ID = row.IsNull("自社部門ID") ? (int?)null : (int?)row["自社部門ID"];
							this.得意先ID = row.IsNull("得意先ID") ? (int?)null : (int?)row["得意先ID"];
							this.請求内訳ID = row.IsNull("請求内訳ID") ? (int?)null : (int?)row["請求内訳ID"];
							this.発地ID = row.IsNull("発地ID") ? (int?)null : (int?)row["発地ID"];
							this.発地名 = row.IsNull("発地名") ? (string)null : (string)row["発地名"];
							this.着地ID = row.IsNull("着地ID") ? (int?)null : (int?)row["着地ID"];
							this.着地名 = row.IsNull("着地名") ? (string)null : (string)row["着地名"];
							this.商品ID = row.IsNull("商品ID") ? (int?)null : (int?)row["商品ID"];
							this.商品名 = row.IsNull("商品名") ? (string)null : (string)row["商品名"];

							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.請求摘要ID = row.IsNull("請求摘要ID") ? (int?)null : (int?)row["請求摘要ID"];
							this.請求摘要 = row.IsNull("請求摘要") ? (string)null : (string)row["請求摘要"];
							this.社内備考ID = row.IsNull("社内備考ID") ? (int?)null : (int?)row["社内備考ID"];
							this.社内備考 = row.IsNull("社内備考") ? (string)null : (string)row["社内備考"];

							this.数量 = row.IsNull("数量") ? (decimal?)null : (decimal?)row["数量"];
							this.単位 = row.IsNull("単位") ? (string)null : (string)row["単位"];
							this.重量 = row.IsNull("重量") ? (decimal?)null : (decimal?)row["重量"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.待機時間 = row.IsNull("待機時間") ? (decimal?)null : (decimal?)row["待機時間"];
							this.請求運賃計算区分ID = row.IsNull("請求運賃計算区分ID") ? (int?)null : (int?)row["請求運賃計算区分ID"];
							this.売上単価 = row.IsNull("売上単価") ? (decimal?)null : (decimal?)row["売上単価"];
							this.売上金額 = row.IsNull("売上金額") ? (int?)null : (int?)row["売上金額"];
							this.通行料 = row.IsNull("通行料") ? (int?)null : (int?)row["通行料"];
							this.請求割増１ = row.IsNull("請求割増１") ? (int?)null : (int?)row["請求割増１"];
							this.請求割増２ = row.IsNull("請求割増２") ? (int?)null : (int?)row["請求割増２"];

							this.社内区分 = row.IsNull("社内区分") ? (int?)null : (int?)row["社内区分"];
							this.売上未定区分 = row.IsNull("売上未定区分") ? (int?)null : (int?)row["売上未定区分"];
							this.請求税区分 = row.IsNull("請求税区分") ? (int?)null : (int?)row["請求税区分"];
							this.確認名称区分 = row.IsNull("確認名称区分") ? (int?)null : (int?)row["確認名称区分"];

							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.労務日 = row.IsNull("労務日") ? (DateTime?)null : (DateTime?)row["労務日"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];

							this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];

							CInputTrn.支払日付 = row.IsNull("支払日付") ? (DateTime?)null : (DateTime?)row["支払日付"];
							CInputTrn.配送日付 = row.IsNull("配送日付") ? (DateTime?)null : (DateTime?)row["配送日付"];
							CInputTrn.支払先ID = row.IsNull("支払先ID") ? (int?)null : (int?)row["支払先ID"];
							CInputTrn.支払先名２次 = row.IsNull("支払先名２次") ? (string)null : (string)row["支払先名２次"];
							CInputTrn.実運送乗務員 = row.IsNull("実運送乗務員") ? (string)null : (string)row["実運送乗務員"];
							CInputTrn.乗務員連絡先 = row.IsNull("乗務員連絡先") ? (string)null : (string)row["乗務員連絡先"];
							CInputTrn.支払運賃計算区分ID = row.IsNull("支払運賃計算区分ID") ? (int?)null : (int?)row["支払運賃計算区分ID"];
							CInputTrn.支払単価 = row.IsNull("支払単価") ? (decimal?)null : (decimal?)row["支払単価"];
							CInputTrn.支払金額 = row.IsNull("支払金額") ? (int?)null : (int?)row["支払金額"];
							CInputTrn.支払通行料 = row.IsNull("支払通行料") ? (int?)null : (int?)row["支払通行料"];
							CInputTrn.支払税区分 = row.IsNull("支払税区分") ? (int?)null : (int?)row["支払税区分"];
							CInputTrn.支払未定区分 = row.IsNull("支払未定区分") ? (int?)null : (int?)row["支払未定区分"];

							AppCommon.DoEvents();

							this.請求日付 = row.IsNull("請求日付") ? (DateTime?)null : (DateTime?)row["請求日付"];
							this.配送時間 = row.IsNull("配送時間") ? (decimal?)null : (decimal?)row["配送時間"];
							this.自社部門ID = row.IsNull("自社部門ID") ? (int?)null : (int?)row["自社部門ID"];
							this.得意先ID = row.IsNull("得意先ID") ? (int?)null : (int?)row["得意先ID"];
							this.請求内訳ID = row.IsNull("請求内訳ID") ? (int?)null : (int?)row["請求内訳ID"];
							this.発地ID = row.IsNull("発地ID") ? (int?)null : (int?)row["発地ID"];
							this.発地名 = row.IsNull("発地名") ? (string)null : (string)row["発地名"];
							this.着地ID = row.IsNull("着地ID") ? (int?)null : (int?)row["着地ID"];
							this.着地名 = row.IsNull("着地名") ? (string)null : (string)row["着地名"];
							this.商品ID = row.IsNull("商品ID") ? (int?)null : (int?)row["商品ID"];
							this.商品名 = row.IsNull("商品名") ? (string)null : (string)row["商品名"];

							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.請求摘要ID = row.IsNull("請求摘要ID") ? (int?)null : (int?)row["請求摘要ID"];
							this.請求摘要 = row.IsNull("請求摘要") ? (string)null : (string)row["請求摘要"];
							this.社内備考ID = row.IsNull("社内備考ID") ? (int?)null : (int?)row["社内備考ID"];
							this.社内備考 = row.IsNull("社内備考") ? (string)null : (string)row["社内備考"];

							this.数量 = row.IsNull("数量") ? (decimal?)null : (decimal?)row["数量"];
							this.単位 = row.IsNull("単位") ? (string)null : (string)row["単位"];
							this.重量 = row.IsNull("重量") ? (decimal?)null : (decimal?)row["重量"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.待機時間 = row.IsNull("待機時間") ? (decimal?)null : (decimal?)row["待機時間"];
							this.請求運賃計算区分ID = row.IsNull("請求運賃計算区分ID") ? (int?)null : (int?)row["請求運賃計算区分ID"];
							this.売上単価 = row.IsNull("売上単価") ? (decimal?)null : (decimal?)row["売上単価"];
							this.売上金額 = row.IsNull("売上金額") ? (int?)null : (int?)row["売上金額"];
							this.通行料 = row.IsNull("通行料") ? (int?)null : (int?)row["通行料"];
							this.請求割増１ = row.IsNull("請求割増１") ? (int?)null : (int?)row["請求割増１"];
							this.請求割増２ = row.IsNull("請求割増２") ? (int?)null : (int?)row["請求割増２"];

							this.社内区分 = row.IsNull("社内区分") ? (int?)null : (int?)row["社内区分"];
							this.売上未定区分 = row.IsNull("売上未定区分") ? (int?)null : (int?)row["売上未定区分"];
							this.請求税区分 = row.IsNull("請求税区分") ? (int?)null : (int?)row["請求税区分"];
							this.確認名称区分 = row.IsNull("確認名称区分") ? (int?)null : (int?)row["確認名称区分"];

							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.労務日 = row.IsNull("労務日") ? (DateTime?)null : (DateTime?)row["労務日"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];

							this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];

							CInputTrn.支払日付 = row.IsNull("支払日付") ? (DateTime?)null : (DateTime?)row["支払日付"];
							CInputTrn.配送日付 = row.IsNull("配送日付") ? (DateTime?)null : (DateTime?)row["配送日付"];
							CInputTrn.支払先ID = row.IsNull("支払先ID") ? (int?)null : (int?)row["支払先ID"];
							CInputTrn.支払先名２次 = row.IsNull("支払先名２次") ? (string)null : (string)row["支払先名２次"];
							CInputTrn.実運送乗務員 = row.IsNull("実運送乗務員") ? (string)null : (string)row["実運送乗務員"];
							CInputTrn.乗務員連絡先 = row.IsNull("乗務員連絡先") ? (string)null : (string)row["乗務員連絡先"];
							CInputTrn.支払運賃計算区分ID = row.IsNull("支払運賃計算区分ID") ? (int?)null : (int?)row["支払運賃計算区分ID"];
							CInputTrn.支払単価 = row.IsNull("支払単価") ? (decimal?)null : (decimal?)row["支払単価"];
							CInputTrn.支払金額 = row.IsNull("支払金額") ? (int?)null : (int?)row["支払金額"];
							CInputTrn.支払通行料 = row.IsNull("支払通行料") ? (int?)null : (int?)row["支払通行料"];
							CInputTrn.支払税区分 = row.IsNull("支払税区分") ? (int?)null : (int?)row["支払税区分"];
							CInputTrn.支払未定区分 = row.IsNull("支払未定区分") ? (int?)null : (int?)row["支払未定区分"];


							類似番号入力表示 = System.Windows.Visibility.Collapsed;

							Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
							Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
							Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
							He_SyaryouBangou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
							Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
							Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.On;

							//DoEvents();
							//return;


						}

                        if (Ditigyoume != null && Ditigyoume.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                            {
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            }
                            DataRow row = Ditigyoume.Rows[0];

                            Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
                            Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
                            Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
                            He_SyaryouBangou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
                            Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
                            Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;

                            Se_TantouBumon.Text1 = null;
                            Se_TokuiSakiNm.Text1 = null;
                            He_SyashuNm.Text1 = null;
                            txt乗務員.Text1 = null;

                            this.請求日付 = row.IsNull("請求日付") ? (DateTime?)null : (DateTime?)row["請求日付"];
							初期日付 = 請求日付 ?? DateTime.Now;
							this.配送時間 = row.IsNull("配送時間") ? (decimal?)null : (decimal?)row["配送時間"];
                            this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
                            this.得意先ID = row.IsNull("得意先ID") ? (int?)null : (int?)row["得意先ID"];
							DoEvents();
                            this.請求内訳ID = row.IsNull("請求内訳ID") ? (int?)null : (int?)row["請求内訳ID"];
                            this.発地ID = row.IsNull("発地ID") ? (int?)null : (int?)row["発地ID"];
                            this.発地名 = row.IsNull("発地名") ? (string)null : (string)row["発地名"];
                            this.着地ID = row.IsNull("着地ID") ? (int?)null : (int?)row["着地ID"];
                            this.着地名 = row.IsNull("着地名") ? (string)null : (string)row["着地名"];
                            this.商品ID = row.IsNull("商品ID") ? (int?)null : (int?)row["商品ID"];
                            this.商品名 = row.IsNull("商品名") ? (string)null : (string)row["商品名"];

                            this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
                            this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
                            this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
                            this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
                            this.請求摘要ID = row.IsNull("請求摘要ID") ? (int?)null : (int?)row["請求摘要ID"];
                            this.請求摘要 = row.IsNull("請求摘要") ? (string)null : (string)row["請求摘要"];
                            this.社内備考ID = row.IsNull("社内備考ID") ? (int?)null : (int?)row["社内備考ID"];
                                this.社内備考 = row.IsNull("社内備考") ? (string)null : (string)row["社内備考"];

                            this.数量 = row.IsNull("数量") ? (decimal?)null : (decimal?)row["数量"];
                            this.単位 = row.IsNull("単位") ? (string)null : (string)row["単位"];
                            this.重量 = row.IsNull("重量") ? (decimal?)null : (decimal?)row["重量"];
                            this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
                            this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
                            this.待機時間 = row.IsNull("待機時間") ? (decimal?)null : (decimal?)row["待機時間"];
                            this.請求運賃計算区分ID = row.IsNull("請求運賃計算区分ID") ? (int?)null : (int?)row["請求運賃計算区分ID"];
                            this.売上単価 = row.IsNull("売上単価") ? (decimal?)null : (decimal?)row["売上単価"];
                            this.売上金額 = row.IsNull("売上金額") ? (int?)null : (int?)row["売上金額"];
                            this.通行料 = row.IsNull("通行料") ? (int?)null : (int?)row["通行料"];
                            this.請求割増１ = row.IsNull("請求割増１") ? (int?)null : (int?)row["請求割増１"];
                            this.請求割増２ = row.IsNull("請求割増２") ? (int?)null : (int?)row["請求割増２"];

                            this.社内区分 = row.IsNull("社内区分") ? (int?)null : (int?)row["社内区分"];
                            this.売上未定区分 = row.IsNull("売上未定区分") ? (int?)null : (int?)row["売上未定区分"];
                            this.請求税区分 = row.IsNull("請求税区分") ? (int?)null : (int?)row["請求税区分"];
                            this.確認名称区分 = row.IsNull("確認名称区分") ? (int?)null : (int?)row["確認名称区分"];

                            this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
                            this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
                            this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
                            this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
                            this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
                            this.労務日 = row.IsNull("労務日") ? (DateTime?)null : (DateTime?)row["労務日"];
                            this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
                            this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
                            this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];


							CInputTrn.支払日付 = row.IsNull("支払日付") ? (DateTime?)null : (DateTime?)row["支払日付"];
							CInputTrn.配送日付 = row.IsNull("配送日付") ? (DateTime?)null : (DateTime?)row["配送日付"];
							CInputTrn.支払先ID = row.IsNull("支払先ID") ? (int?)null : (int?)row["支払先ID"];
							CInputTrn.支払先名２次 = row.IsNull("支払先名２次") ? (string)null : (string)row["支払先名２次"];
							CInputTrn.実運送乗務員 = row.IsNull("実運送乗務員") ? (string)null : (string)row["実運送乗務員"];
							CInputTrn.乗務員連絡先 = row.IsNull("乗務員連絡先") ? (string)null : (string)row["乗務員連絡先"];
							CInputTrn.支払運賃計算区分ID = row.IsNull("支払運賃計算区分ID") ? (int?)null : (int?)row["支払運賃計算区分ID"];
							CInputTrn.支払単価 = row.IsNull("支払単価") ? (decimal?)null : (decimal?)row["支払単価"];
							CInputTrn.支払金額 = row.IsNull("支払金額") ? (int?)null : (int?)row["支払金額"];
							CInputTrn.支払通行料 = row.IsNull("支払通行料") ? (int?)null : (int?)row["支払通行料"];
							CInputTrn.支払税区分 = row.IsNull("支払税区分") ? (int?)null : (int?)row["支払税区分"];
							CInputTrn.支払未定区分 = row.IsNull("支払未定区分") ? (int?)null : (int?)row["支払未定区分"];


							AppCommon.DoEvents();

							this.請求日付 = row.IsNull("請求日付") ? (DateTime?)null : (DateTime?)row["請求日付"];
							初期日付 = 請求日付 ?? DateTime.Now;
							this.配送時間 = row.IsNull("配送時間") ? (decimal?)null : (decimal?)row["配送時間"];
							this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
							this.得意先ID = row.IsNull("得意先ID") ? (int?)null : (int?)row["得意先ID"];
							DoEvents();
							this.請求内訳ID = row.IsNull("請求内訳ID") ? (int?)null : (int?)row["請求内訳ID"];
							this.発地ID = row.IsNull("発地ID") ? (int?)null : (int?)row["発地ID"];
							this.発地名 = row.IsNull("発地名") ? (string)null : (string)row["発地名"];
							this.着地ID = row.IsNull("着地ID") ? (int?)null : (int?)row["着地ID"];
							this.着地名 = row.IsNull("着地名") ? (string)null : (string)row["着地名"];
							this.商品ID = row.IsNull("商品ID") ? (int?)null : (int?)row["商品ID"];
							this.商品名 = row.IsNull("商品名") ? (string)null : (string)row["商品名"];

							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.請求摘要ID = row.IsNull("請求摘要ID") ? (int?)null : (int?)row["請求摘要ID"];
							this.請求摘要 = row.IsNull("請求摘要") ? (string)null : (string)row["請求摘要"];
							this.社内備考ID = row.IsNull("社内備考ID") ? (int?)null : (int?)row["社内備考ID"];
							this.社内備考 = row.IsNull("社内備考") ? (string)null : (string)row["社内備考"];

							this.数量 = row.IsNull("数量") ? (decimal?)null : (decimal?)row["数量"];
							this.単位 = row.IsNull("単位") ? (string)null : (string)row["単位"];
							this.重量 = row.IsNull("重量") ? (decimal?)null : (decimal?)row["重量"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.待機時間 = row.IsNull("待機時間") ? (decimal?)null : (decimal?)row["待機時間"];
							this.請求運賃計算区分ID = row.IsNull("請求運賃計算区分ID") ? (int?)null : (int?)row["請求運賃計算区分ID"];
							this.売上単価 = row.IsNull("売上単価") ? (decimal?)null : (decimal?)row["売上単価"];
							this.売上金額 = row.IsNull("売上金額") ? (int?)null : (int?)row["売上金額"];
							this.通行料 = row.IsNull("通行料") ? (int?)null : (int?)row["通行料"];
							this.請求割増１ = row.IsNull("請求割増１") ? (int?)null : (int?)row["請求割増１"];
							this.請求割増２ = row.IsNull("請求割増２") ? (int?)null : (int?)row["請求割増２"];

							this.社内区分 = row.IsNull("社内区分") ? (int?)null : (int?)row["社内区分"];
							this.売上未定区分 = row.IsNull("売上未定区分") ? (int?)null : (int?)row["売上未定区分"];
							this.請求税区分 = row.IsNull("請求税区分") ? (int?)null : (int?)row["請求税区分"];
							this.確認名称区分 = row.IsNull("確認名称区分") ? (int?)null : (int?)row["確認名称区分"];

							this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
							this.自社部門ID = (int?)row["自社部門ID"] == 0 ? (int?)null : (int?)row["自社部門ID"];
							this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
							this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
							this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
							this.労務日 = row.IsNull("労務日") ? (DateTime?)null : (DateTime?)row["労務日"];
							this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
							this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
							this.登録日時 = row.IsNull("登録日時") ? (DateTime?)null : (DateTime?)row["登録日時"];


							CInputTrn.支払日付 = row.IsNull("支払日付") ? (DateTime?)null : (DateTime?)row["支払日付"];
							CInputTrn.配送日付 = row.IsNull("配送日付") ? (DateTime?)null : (DateTime?)row["配送日付"];
							CInputTrn.支払先ID = row.IsNull("支払先ID") ? (int?)null : (int?)row["支払先ID"];
							CInputTrn.支払先名２次 = row.IsNull("支払先名２次") ? (string)null : (string)row["支払先名２次"];
							CInputTrn.実運送乗務員 = row.IsNull("実運送乗務員") ? (string)null : (string)row["実運送乗務員"];
							CInputTrn.乗務員連絡先 = row.IsNull("乗務員連絡先") ? (string)null : (string)row["乗務員連絡先"];
							CInputTrn.支払運賃計算区分ID = row.IsNull("支払運賃計算区分ID") ? (int?)null : (int?)row["支払運賃計算区分ID"];
							CInputTrn.支払単価 = row.IsNull("支払単価") ? (decimal?)null : (decimal?)row["支払単価"];
							CInputTrn.支払金額 = row.IsNull("支払金額") ? (int?)null : (int?)row["支払金額"];
							CInputTrn.支払通行料 = row.IsNull("支払通行料") ? (int?)null : (int?)row["支払通行料"];
							CInputTrn.支払税区分 = row.IsNull("支払税区分") ? (int?)null : (int?)row["支払税区分"];
							CInputTrn.支払未定区分 = row.IsNull("支払未定区分") ? (int?)null : (int?)row["支払未定区分"];

                            類似番号入力表示 = System.Windows.Visibility.Collapsed;

                            Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
                            Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
                            Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
                            He_SyaryouBangou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
                            Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
                            Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.On;

							//DoEvents();

                        }
                        else
                        {
                            //

                            //DUriageData.Rows.Add(DUriageData.NewRow());
                            類似番号入力表示 = System.Windows.Visibility.Visible;
                        }
                        ChangeKeyItemChangeable(false);
                        SetFocusToTopControl();
                        break;
                    case GetCARDATA:
                        CatchupCarData(tbl);
                        break;
                    case GetCARDATA2:
                        CatchupCarData2(tbl);
                        break;
                    case GetHINDATA:
                        CatchupHinData(tbl);
                        break;
                    case GetTOKDATA:
                        CatchupTokData(tbl);
                        break;
                    case GetYOSDATA:
                        CatchupYosData(tbl);
                        break;

                    case GetTanka:
						if (tbl != null)
                        {
                            int kbn = (int)tbl.Rows[0]["Kubun"];
                            decimal tanka = (decimal)tbl.Rows[0]["Tanka"];
                            decimal kingaku = (decimal)tbl.Rows[0]["Kingaku"];
                            if (kbn == 0)
                            {
                                if (tanka >= 0)
                                {
                                    売上単価 = tanka;
                                }
                                if (kingaku >= 0)
                                {
                                    売上金額 = (int)kingaku;
                                }
                            }
                            else
                            {
                                if (tanka >= 0)
                                {
                                    CInputTrn.支払単価 = tanka;
                                }
                                if (kingaku >= 0)
                                {
                                    CInputTrn.支払金額 = (int)kingaku;
                                }
                            }
                        }
                        break;

					case PutAllData:
						IsUpdated = true;
						if (番号通知区分 == 1)
						{
							MessageBox.Show(string.Format("明細番号 {0} で登録しました。", (int)data));
						};
                        ScreenClear();
                        break;
					case DeleteAllData:
						IsUpdated = true;
                        ScreenClear();
                        break;

                    case GetMaxMeisaiNo:
                        登録件数 = (int)data;
                        break;

					case TRN_RIREKI:
						//RIREKI.ItemsSource = null;
						if (data == null)
						{
							spRireki.Rows.Clear();
							return;
						}
						売上明細データ = (List<DLY02015_RIREKI>)AppCommon.ConvertFromDataTable(typeof(List<DLY02015_RIREKI>), tbl);
						spRireki.ItemsSource = 売上明細データ;
					
						spRireki.Columns[0].Focusable = true;
						spRireki.Columns[0].Locked = true;

						break;
                }
                #endregion
            }
            catch
            {
                //MessageBox.Show("エラー");
            }
        }


        private void ScreenClear()
        {
            this.IsRfferMode = false;
            this.RefferNumber = string.Empty;
			this.RefferGyou = string.Empty;

            this.MaintenanceMode = null;
            this.DLogData = null;
            this.DKeihiData = null;
            this.運転日報データ = null;

            this.登録日時 = null;
            this.請求日付 = null;
            this.配送時間 = null;
            this.得意先ID = null;
            this.請求内訳ID = null;
            this.発地ID = null;
            this.発地名 = null;
            this.着地ID = null;
            this.着地名 = null;
            this.商品ID = null;
            this.商品名 = null;
            this.車輌ID = null;
            this.車輌番号 = null;
            this.車種ID = null;
            this.乗務員ID = null;
            this.請求摘要ID = null;
            this.請求摘要 = null;
            this.社内備考ID = null;
            this.社内備考 = null;
            this.数量 = null;
            this.単位 = null;
            this.重量 = null;
            this.走行ＫＭ = null;
            this.実車ＫＭ = null;
            this.待機時間 = null;
            this.請求運賃計算区分ID = null;
            this.売上単価 = null;
            this.売上金額 = null;
            this.通行料 = null;
            this.請求割増１ = null;
            this.請求割増２ = null;
            this.社内区分 = 0;
			this.売上未定区分 = 未定区分;
            this.請求税区分 = 0;
            this.確認名称区分 = 0;
            this.自社部門ID = null;

            //this.日付表示モード = System.Windows.Visibility.Hidden;

            this.類似番号入力表示 = System.Windows.Visibility.Collapsed;
			this.DetailsNumber = null;
			this.DetailsGyou = null;

            ClearUriageItems();
            ChangeKeyItemChangeable(true);
            ResetAllValidation();


            SetFocusToTopControl();

            //現在の登録件数を表示
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMaxMeisaiNo));
        }

        /// <summary>
        /// F1 マスタ検索
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
                if (twintxt != null)
                {
                    if (twintxt.Name == "Se_TokuiSakiNm")
                    {
                        this.内訳Enabled = true;
                    }
                }
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                var elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);
                if (spgrid != null)
                {
                    if (spgrid.ActiveColumn.Name == "S_経費項目ID")
                    {
                        Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                        MST09010 mnt = new MST09010();
                        mnt.ShowDialog(this);

                    }
                    else if (spgrid.ActiveColumn.Name == "S_支払先ID")
                    {
                        Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                        MST01010 mnt = new MST01010();
                        mnt.ShowDialog(this);

                    }
                    else if (spgrid.ActiveColumn.Name == "S_摘要ID")
                    {
                        Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                        MST08010 mnt = new MST08010();
                        mnt.ShowDialog(this);

                    }
                }
                else
                {
                    ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
                }
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテナンス画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

		/// <summary>
		/// 経費行追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF3Key(object sender, KeyEventArgs e)
		{
			if (this.MaintenanceMode == null)
			{
				return;
			}
			this.DKeihiData.Rows.Add(this.DKeihiData.NewRow());
		}

		/// <summary>
		/// 売上履歴
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF4Key(object sender, KeyEventArgs e)
		{
			得意先ID指定 = 得意先ID;
			if (Grid_RIREKI.Visibility == Visibility.Collapsed)
			{
				Grid_RIREKI.Visibility = Visibility.Visible;
				CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
				base.SendRequest(com);
				if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
				{
					spRireki.Focus();
				}
				else
				{
					Jyunjyo.Focus();
				}
			}
			else
			{
				Grid_RIREKI.Visibility = Visibility.Collapsed;
			}
		}
		/// <summary>
		/// 行コピー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF5Key(object sender, KeyEventArgs e)
		{
			if (this.MaintenanceMode == null)
			{
				return;
			}

			CInputTrn.支払日付 = 請求日付;
			CInputTrn.配送日付 = 請求日付;
			CInputTrn.発地ID = 発地ID;
			CInputTrn.発地名 = 発地名;
			CInputTrn.着地ID = 着地ID;
			CInputTrn.着地名 = 着地名;
			CInputTrn.社内備考ID = 社内備考ID;
			CInputTrn.社内備考 = 社内備考;
			CInputTrn.車輌ID = 車輌ID;
			CInputTrn.車輌番号 = 車輌番号;
			CInputTrn.車種ID = 車種ID;
			CInputTrn.乗務員ID = 乗務員ID;
			//CInputTrn.乗務員名 = 

		}

        /// <summary>
        /// 行登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// 配車依頼表印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            try
            {
                int init;
                DateTime dejit;
                DLY02020 frm = new DLY02020();
                if (int.TryParse(Si_SiharaiSaki.Text1, out init))
                {
                    frm.i傭車先ID = init;
                }
                frm.i車種名 = He_SyashuNm.Text2;
				frm.i積荷 = Se_SyouhinNm.Text2;
                if (DateTime.TryParse(Se_SeikyuHiduke.Text, out dejit))
                {
                    frm.i日付 = dejit;
                }
                if (int.TryParse(Se_HattiNm.Text1, out init))
                {
                    frm.i積地ID = init;
                }
                frm.i積地名 = Se_HattiNm.Text2;
                if (int.TryParse(Se_CyakuNm.Text1, out init))
                {
                    frm.i卸地ID = init;
                }
                frm.i卸地名 = Se_CyakuNm.Text2;
                frm.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (MaintenanceMode == null)
            {
                return;
            }
            Se_HaitatuJikan.Focus();
            Se_SeikyuHiduke.Focus();
            Update();
        }

        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
            {
                return;
            }
            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
            {
                return;
            }

            ScreenClear();
        }

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
            {
                this.Close();
            }
            else
            {
				if (DataUpdateVisible != Visibility.Hidden)
				{
					var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
					if (yesno == MessageBoxResult.No)
					{
						return;
					}
				}
                this.Close();
            }

        }


        /// <summary>
        /// F12 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
            {
                return;
            }
            var yesno = MessageBox.Show("伝票を削除しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
            {
                return;
            }

            Delete();
        }
		
        private void He_MeisaiBangou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
			//if (e.Key == Key.Enter)
			//{
			//	//e.Handled = true;
			//	GetMeisaiData();
			//}
        }
		private void He_MeisaiGyou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //e.Handled = true;
                GetMeisaiData();
            }
        }

		private void He_Reffer_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			//if (e.Key == Key.Enter)
			//{
			//	//e.Handled = true;
			//	GetRefMeisaiData();
			//}
		}

		private void He_RuijiMeisaiGyou_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				//e.Handled = true;
				GetRefMeisaiData();
			}
		}

        void GetMeisaiData()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.DetailsNumber))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMaxNo, null, null));
                }
                else
                {
					int pno = AppCommon.IntParse(this.DetailsNumber);
					int pgyou = AppCommon.IntParse(this.DetailsGyou);
					CommunicationObject[] com ={
										   new CommunicationObject(MessageType.RequestData, GetNippou, pno, pgyou),
									   };
					//base.SendRequest(com);
					DoEvents();
					base.SendRequest(com);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 類似明細番号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetRefMeisaiData()
        {
            if (string.IsNullOrWhiteSpace(this.RefferNumber))
            {
                return;
            }
            else
            {
                IsRfferMode = true;
				int pno = AppCommon.IntParse(this.RefferNumber);
				int pgyou = AppCommon.IntParse(this.RefferGyou);
				CommunicationObject[] com ={
										   new CommunicationObject(MessageType.RequestData, GetNippou, pno, pgyou),
									   };
				//base.SendRequest(com);
				DoEvents();
				base.SendRequest(com);


            }
        }

        /// <summary>
        /// データクラスからDataRowに値を転送する。
        /// データクラスのプロパティ名とカラム名は一致することが前提。
        /// </summary>
        /// <param name="props"></param>
        /// <param name="data"></param>
        /// <param name="row"></param>
        private void GetValues(object data, DataRow row)
        {
            var props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //メンバを取得する
            foreach (PropertyInfo pi in props)
            {
                var col = pi.GetValue(data);
                if (col == null)
                {
                    row[pi.Name] = DBNull.Value;
                }
                else
                {
                    row[pi.Name] = pi.GetValue(data);
                }
            }
        }



        public void ClearUriageItems()
        {

            // 請求項目

            Si_SiharaiHiduke.Text = string.Empty;
            Si_HaisouHiduke.Text = string.Empty;
            cmb支払未定区分.SelectedIndex = 0;
            Si_SiharaiSaki.Text1 = string.Empty;
            Si_SiharaiSakiJyoumuin.Text = string.Empty;
            Si_JyoumuinRenrakusaki.Text = string.Empty;
            Si_SiharaisakiNm2.Text = string.Empty;
            cmb支払計算区分.SelectedIndex = 0;
            cmb支払税区分.SelectedIndex = 0;
            Si_SiharaiTanka.Text = string.Empty;
            Si_SiharaiKingaku.Text = string.Empty;
			Si_SiharaiTuukouRyou.Text = string.Empty;




            //Se_SeikyuHiduke.Text = string.Empty;
            //Se_HaitatuJikan.Text = string.Empty;
            //Se_TantouBumon.Text1 = string.Empty;
            //Se_TantouBumon.Text2 = string.Empty;
            //Se_TokuiSakiNm.Text1 = string.Empty;
            //Se_TokuiSakiNm.Text2 = string.Empty;
            //Se_SyouhinNm.Text1 = string.Empty;
            //Se_SeikyuuUtiwakeNm.Text1 = string.Empty;
            //Se_SeikyuuUtiwakeNm.Text2 = string.Empty;
            //Se_SeikyuuTekiyou.Text1 = string.Empty;
            //Se_SeikyuuTekiyou.Text2 = string.Empty;
            //Se_HattiNm.Text1 = string.Empty;
            //Se_HattiNm.Text2 = string.Empty;
            //Se_Bikou.Text1 = string.Empty;
            //Se_Bikou.Text2 = string.Empty;
            //Se_CyakuNm.Text1 = string.Empty;
            //Se_CyakuNm.Text2 = string.Empty;
            //Se_Suuryou.Text = string.Empty;
            //Se_Tani.Text = string.Empty;
            //Se_Jyuuryou.Text = string.Empty;
            //Se_SoukouKm.Text = string.Empty;
            //Se_JissyaKm.Text = string.Empty;
            //Se_UriageTanka.Text = string.Empty;
            //Se_UriageKingaku.Text = string.Empty;
            //Se_TuukouRyou.Text = string.Empty;
            //Se_Warimasi1.Text = string.Empty;
            //Se_Warimasi2.Text = string.Empty;
            //Se_TaikiJikan.Text = string.Empty;
            //this.cmb計算区分.SelectedIndex = 0;
            //this.cmb社内区分.SelectedIndex = 0;
            //this.cmb税区分.SelectedIndex = 0;
            //this.cmb未定区分.SelectedIndex = 0;

            // 支払項目
            //Si_SiharaiHiduke.Text = string.Empty;
            //Si_SiharaiSaki.Text1 = string.Empty;
            //Si_SiharaisakiNm2.Text = string.Empty;
            //Si_SiharaiSakiJyoumuin.Text = string.Empty;
            //Si_JyoumuinRenrakusaki.Text = string.Empty;
            //Si_SiharaiTanka.Text = string.Empty;
            //Si_SiharaiKingaku.Text = string.Empty;
            //Si_SiharaiTuukouRyou.Text = string.Empty;
            //this.cmb支払計算区分.SelectedIndex = 0;
            //this.cmb支払税区分.SelectedIndex = 0;
            //this.cmb支払未定区分.SelectedIndex = 0;

			this.CInputTrn = new TRN();
            this.ResetAllValidation();

        }

        void Update()
        {

            DateTime d日付;
            if (!DateTime.TryParse(Se_SeikyuHiduke.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }


            var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (yesno == MessageBoxResult.No)
            {
                return;
            }

			DataTable TrnData = DUriageData.Copy();
            DataRow Trnrow = TrnData.NewRow();
            Trnrow["明細番号"] = this.DetailsNumber;
			Trnrow["明細行"] = this.DetailsGyou;
            Trnrow["請求日付"] = this.請求日付 == null ? DBNull.Value : (object)this.請求日付;
			Trnrow["支払日付"] = CInputTrn.支払日付 == null ? DBNull.Value : (object)CInputTrn.支払日付;
			Trnrow["配送日付"] = CInputTrn.配送日付 == null ? DBNull.Value : (object)CInputTrn.配送日付;
            Trnrow["配送時間"] = this.配送時間 == null ? DBNull.Value : (object)this.配送時間;
            Trnrow["得意先ID"] = 得意先ID;
            Trnrow["請求内訳ID"] = this.請求内訳ID == null ? DBNull.Value : (object)this.請求内訳ID;
            Trnrow["車輌ID"] = this.車輌ID == null ? DBNull.Value : (object)this.車輌ID;
            Trnrow["車種ID"] = this.車種ID == null ? DBNull.Value : (object)this.車種ID;
            Trnrow["乗務員ID"] = this.乗務員ID == null ? DBNull.Value : (object)this.乗務員ID;
            Trnrow["自社部門ID"] = this.自社部門ID == null ? DBNull.Value : (object)this.自社部門ID;
            Trnrow["車輌番号"] = this.車輌番号 == null ? DBNull.Value : (object)this.車輌番号;
            Trnrow["請求運賃計算区分ID"] = this.請求運賃計算区分ID == null ? DBNull.Value : (object)this.請求運賃計算区分ID;
            Trnrow["数量"] = this.数量 == null ? DBNull.Value : (object)this.数量;
            Trnrow["単位"] = this.単位 == null ? DBNull.Value : (object)this.単位;
            Trnrow["重量"] = this.重量 == null ? DBNull.Value : (object)this.重量;
            Trnrow["走行ＫＭ"] = this.走行ＫＭ == null ? DBNull.Value : (object)this.走行ＫＭ;
            Trnrow["実車ＫＭ"] = this.実車ＫＭ == null ? DBNull.Value : (object)this.実車ＫＭ;
            Trnrow["待機時間"] = this.待機時間 == null ? DBNull.Value : (object)this.待機時間;
            Trnrow["売上単価"] = this.売上単価 == null ? DBNull.Value : (object)this.売上単価;
            Trnrow["売上金額"] = this.売上金額 == null ? DBNull.Value : (object)this.売上金額;
            Trnrow["通行料"] = this.通行料 == null ? DBNull.Value : (object)this.通行料;
            Trnrow["請求割増１"] = this.請求割増１ == null ? DBNull.Value : (object)this.請求割増１;
            Trnrow["請求割増２"] = this.請求割増２ == null ? DBNull.Value : (object)this.請求割増２;
            Trnrow["社内区分"] = this.社内区分 == null ? DBNull.Value : (object)this.社内区分;
            Trnrow["請求税区分"] = this.請求税区分 == null ? DBNull.Value : (object)this.請求税区分;
            Trnrow["売上未定区分"] = this.売上未定区分 == null ? DBNull.Value : (object)this.売上未定区分;
            Trnrow["商品ID"] = this.商品ID == null ? DBNull.Value : (object)this.商品ID;
            Trnrow["商品名"] = this.商品名 == null ? DBNull.Value : (object)this.商品名;
            Trnrow["発地ID"] = this.発地ID == null ? DBNull.Value : (object)this.発地ID;
            Trnrow["発地名"] = this.発地名 == null ? DBNull.Value : (object)this.発地名;
            Trnrow["着地ID"] = this.着地ID == null ? DBNull.Value : (object)this.着地ID;
            Trnrow["着地名"] = this.着地名 == null ? DBNull.Value : (object)this.着地名;
            Trnrow["請求摘要ID"] = this.請求摘要ID == null ? DBNull.Value : (object)this.請求摘要ID;
            Trnrow["請求摘要"] = this.請求摘要 == null ? DBNull.Value : (object)this.請求摘要;
            Trnrow["社内備考ID"] = this.社内備考ID == null ? DBNull.Value : (object)this.社内備考ID;
            Trnrow["社内備考"] = this.社内備考 == null ? DBNull.Value : (object)this.社内備考;
            Trnrow["入力者ID"] = this.担当者ID == null ? DBNull.Value : (object)this.担当者ID;
            Trnrow["明細区分"] = 1;
            Trnrow["入力区分"] = 3;
            Trnrow["登録日時"] = this.登録日時 == null ? DBNull.Value : (object)this.登録日時;

			Trnrow["支払先ID"] = CInputTrn.支払先ID == null ? DBNull.Value : (object)CInputTrn.支払先ID;
			Trnrow["支払先名２次"] = CInputTrn.支払先名２次 == null ? DBNull.Value : (object)CInputTrn.支払先名２次;
			Trnrow["実運送乗務員"] = CInputTrn.実運送乗務員 == null ? DBNull.Value : (object)CInputTrn.実運送乗務員;
			Trnrow["乗務員連絡先"] = CInputTrn.乗務員連絡先 == null ? DBNull.Value : (object)CInputTrn.乗務員連絡先;
			Trnrow["支払運賃計算区分ID"] = CInputTrn.支払運賃計算区分ID == null ? DBNull.Value : (object)CInputTrn.支払運賃計算区分ID;
			Trnrow["支払単価"] = CInputTrn.支払単価 == null ? DBNull.Value : (object)CInputTrn.支払単価;
			Trnrow["支払金額"] = CInputTrn.支払金額 == null ? DBNull.Value : (object)CInputTrn.支払金額;
			Trnrow["支払通行料"] = CInputTrn.支払通行料 == null ? DBNull.Value : (object)CInputTrn.支払通行料;
			Trnrow["支払税区分"] = CInputTrn.支払税区分 == null ? DBNull.Value : (object)CInputTrn.支払税区分;
			Trnrow["支払未定区分"] = CInputTrn.支払未定区分 == null ? DBNull.Value : (object)CInputTrn.支払未定区分;

            TrnData.Rows.Add(Trnrow);
            Trnrow.AcceptChanges();


			int pno = AppCommon.IntParse(this.DetailsNumber);
			int pgyou = AppCommon.IntParse(this.DetailsGyou);
			if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                pno = -1;
                担当者ID = ccfg.ユーザID;
            }


            CommunicationObject com = new CommunicationObject(MessageType.RequestData, PutAllData, pno, pgyou, TrnData, 担当者ID , this.確認名称区分 );
			base.SendRequest(com);
			初期日付 = 請求日付 ?? DateTime.Now;
        }


        void Delete()
        {
			int pno = AppCommon.IntParse(this.DetailsNumber);
			int pgyou = AppCommon.IntParse(this.DetailsGyou);
			CommunicationObject com = new CommunicationObject(MessageType.RequestData, DeleteAllData, pno, pgyou);
            base.SendRequest(com);
        }

        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                (sender as Button).IsEnabled = true;
            }
        }

        private void FirstIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.first;
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0, 0));
        }

        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.previous;
            if (string.IsNullOrWhiteSpace(this.DetailsNumber))
            {
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0, 0));
            }
            else
            {
				int no;
				int gyou;
				if (int.TryParse(this.DetailsNumber, out no))
                {
					gyou = AppCommon.IntParse(this.DetailsGyou.ToString());
					base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, no, gyou, 1));
                }
            }
        }

        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.next;
            if (string.IsNullOrWhiteSpace(this.DetailsNumber))
            {
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0, 0));
            }
            else
            {
                int no;
				int gyou;
                if (int.TryParse(this.DetailsNumber, out no))
                {
					gyou = AppCommon.IntParse(this.DetailsGyou.ToString());
					base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, no, gyou, 2));
                }
            }
        }

        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.last;
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0, 3));
        }

        #region Window_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
		{
			this.spRireki.InputBindings.Clear();

            if (frmcfg == null) { frmcfg = new ConfigDLY02015(); }
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            this.DLogData = null;
            this.DKeihiData = null;
			this.売上明細データ = null;
			frmcfg.spRirekiConfig20160419 = AppCommon.SaveSpConfig(this.spRireki);
			frmcfg.表示順指定 = 表示順;
			frmcfg.担当者指定 = 担当者ID指定;
			frmcfg.得意先指定 = 得意先ID指定;
			frmcfg.番号通知区分 = 番号通知区分;
			frmcfg.最終伝票表示区分 = 最終伝票表示区分;

            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        /// <summary>
        /// スプレッド定義情報リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnResert_Click(object sender, RoutedEventArgs e)
        {
			AppCommon.LoadSpConfig(this.spRireki, this.spRirekiConfig, "売上明細データ");
			ScreenClear();
        }



        private void 労務日_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }

        private void 請求日_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }

        private void 支払日付_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }

        private void 勤務日FROM_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }

        private void 勤務日TO_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }


        private void 日付_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text))
            {
                ctl.Text = 初期日付.ToString(ctl.Mask);
            }
        }

        private void 支払日付_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text) == true && 請求日付 != null)
            {
                ctl.Text = string.Format("{0:yyyy/MM/dd}", 請求日付);
            }
        }

        private void 配送日付_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text) == true && 請求日付 != null)
            {
                ctl.Text = string.Format("{0:yyyy/MM/dd}", 請求日付);
            }
        }

        /// <summary>
        /// 車両ID入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CARID_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                int cid;
                if (int.TryParse(ctl.Text1, out cid) != true)
                {
                    return;
                }
                SendRequest(new CommunicationObject(MessageType.RequestData, GetCARDATA, cid));
            }
        }

        /// <summary>
        /// 車両ﾏｽﾀ取得時
        /// </summary>
        /// <param name="tbl"></param>
        void CatchupCarData(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }
            //if (string.IsNullOrWhiteSpace(txt乗務員.Text1))
            //{
            //    this.乗務員ID = (int?)tbl.Rows[0]["乗務員ID"];
            //}
            //if (string.IsNullOrWhiteSpace(He_SyashuNm.Text1))
            //{
            //    this.車種ID = (int?)tbl.Rows[0]["車種ID"];
			//}
			if (tbl.Rows[0]["乗務員ID"].ToString() != "0" && (this.乗務員ID == null || this.乗務員ID == 0))
			{
				this.乗務員ID = (int?)tbl.Rows[0]["乗務員ID"];
			}
			if (tbl.Rows[0]["車種ID"].ToString() != "0" && (this.車種ID == null || this.車種ID == 0))
			{
				this.車種ID = (int?)tbl.Rows[0]["車種ID"];
			}
        }

        /// <summary>
        /// 車両ﾏｽﾀ取得時
        /// </summary>
        /// <param name="tbl"></param>
        void CatchupCarData2(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }
            //if (string.IsNullOrWhiteSpace(Si_JyoumuinNm.Text1))
            //{
            //    CInputTrn.乗務員ID = (int?)tbl.Rows[0]["乗務員ID"];
            //}
            //if (string.IsNullOrWhiteSpace(Si_SyashuNm.Text1))
            //{
            //    CInputTrn.車種ID = (int?)tbl.Rows[0]["車種ID"];
            //}
            CInputTrn.乗務員ID = (int?)tbl.Rows[0]["乗務員ID"];
            CInputTrn.車種ID = (int?)tbl.Rows[0]["車種ID"];
        }

        void CatchupHinData(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }
            this.Se_Tani.Text = this.単位 = tbl.Rows[0].IsNull("単位") ? "" : (string)tbl.Rows[0]["単位"];
        }

        private void 得意先ID_TextChanged(object sender, RoutedEventArgs e)
        {
            //if (IsGridWorking)
            //    return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
            if (ctl == null)
            {
                return;
            }
            this.内訳検索用得意先ID = ctl.Text1;
            this.CInputTrn.請求内訳ID = null;
            this.CInputTrn.請求内訳管理区分 = null;
            this.CInputTrn.請求内訳名 = string.Empty;
            請求内訳ID = null;
            int cid;
            if (int.TryParse(ctl.Text1, out cid) != true)
            {
                return;
            }
            SendRequest(new CommunicationObject(MessageType.RequestData, GetTOKDATA, cid, 0));
        }

        void CatchupTokData(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }
            this.内訳Enabled = ((int)tbl.Rows[0]["請求内訳管理区分"] == 1) ? true : false;
            //if (this.運転日報データ != null)
            {
                this.CInputTrn.請求内訳管理区分 = (int?)tbl.Rows[0]["請求内訳管理区分"];
                if (this.CInputTrn.請求内訳管理区分 == null)
                {
                    this.内訳Enabled = false;
                }
                else
                {
                    this.内訳Enabled = ((int)this.CInputTrn.請求内訳管理区分 == 1) ? true : false;
                }
                this.CInputTrn.請求運賃計算区分ID = (int?)tbl.Rows[0]["請求運賃計算区分ID"];
                if (tbl.Rows[0].IsNull("請求運賃計算区分ID") != true)
                {
                    this.cmb計算区分.SelectedValue = (int)this.CInputTrn.請求運賃計算区分ID;
                }

                請求路線計算年度 = (int?)tbl.Rows[0]["Ｔ路線計算年度"];
            }
        }

        void CatchupYosData(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }

            this.CInputTrn.支払運賃計算区分ID = (int?)tbl.Rows[0]["支払運賃計算区分ID"];
            if (tbl.Rows[0].IsNull("支払運賃計算区分ID") != true)
            {
                this.cmb支払計算区分.SelectedValue = (int)this.CInputTrn.支払運賃計算区分ID;
            }

            支払路線計算年度 = (int?)tbl.Rows[0]["Ｓ路線計算年度"];
        }


        private void Price計算_請求()
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            //if (運転日報データ == null)
            //	return;

            if (this.cmb計算区分.SelectedValue == null)
            {
                return;
            }
            int result;
            int p車種ID = 0;
            int p計算区分 = (int)this.cmb計算区分.SelectedValue;
            int p請求支払区分 = 0;	// 請求
            int p得意先ID = (string.IsNullOrWhiteSpace(this.Se_TokuiSakiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_TokuiSakiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p発地ID = (string.IsNullOrWhiteSpace(this.Se_HattiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_HattiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p着地ID = (string.IsNullOrWhiteSpace(this.Se_CyakuNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_CyakuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p商品ID = (string.IsNullOrWhiteSpace(this.Se_SyouhinNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_SyouhinNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            if (this.He_SyashuNm != null)
            {
                if (int.TryParse(He_SyashuNm.Text1, out result) == true)
                {
                    p車種ID = (string.IsNullOrWhiteSpace(this.He_SyashuNm.Text1)) ? 0 : AppCommon.IntParse(this.He_SyashuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
                }
            }
            int p走行ＫＭ = (string.IsNullOrWhiteSpace(this.Se_SoukouKm.Text)) ? 0 : AppCommon.IntParse(this.Se_SoukouKm.Text, System.Globalization.NumberStyles.AllowThousands);
            decimal p数量 = (string.IsNullOrWhiteSpace(this.Se_Suuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Suuryou.Text);
            decimal p重量 = (string.IsNullOrWhiteSpace(this.Se_Jyuuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Jyuuryou.Text);
            decimal 単価 = (string.IsNullOrWhiteSpace(this.Se_UriageTanka.Text)) ? 0 : AppCommon.DecimalParse(this.Se_UriageTanka.Text);
            int 金額 = -1;

            /*
             p計算区分	表示名
                0		手入力
                1		数量計算
                2		重量計算
                3		運賃タリフ
                4		地区単価
                5		車種運賃
                6		個建単価
             */
            switch (p計算区分)
            {
                case 0:
                    // 手入力は何もしない
					break;
				case 1:
					// 数量 ｘ 単価
					switch (金額計算端数処理区分)
					{
					case 0:
						金額 = (int)Math.Floor((p数量 * 単価));
						break;
					case 1:
						金額 = (int)Math.Ceiling((p数量 * 単価));
						break;
					case 2:
						金額 = (int)Math.Round((p数量 * 単価), 0, MidpointRounding.AwayFromZero);
						break;
					}
					売上金額 = 金額;
					//Se_UriageKingaku.Text = 金額.ToString(Se_UriageKingaku.Mask);
					break;
				case 2:
					// 重量 ｘ 単価
					switch (金額計算端数処理区分)
					{
					case 0:
						金額 = (int)Math.Floor((p重量 * 単価));
						break;
					case 1:
						金額 = (int)Math.Ceiling((p重量 * 単価));
						break;
					case 2:
						金額 = (int)Math.Round((p重量 * 単価), 0, MidpointRounding.AwayFromZero);
						break;
					}
					売上金額 = 金額;
					//Se_UriageKingaku.Text = 金額.ToString(Se_UriageKingaku.Mask);
					break;
				case 3:
					if (p得意先ID != 0 && p重量 != 0 && p走行ＫＭ != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 4:
					if (p得意先ID != 0 && p商品ID != 0)//&& p重量 != 0 && p数量 != 0
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 5:
					if (p得意先ID != 0 && p車種ID != 0) // && p重量 != 0 && p数量 != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 6:
					if (p得意先ID != 0 && p数量 != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
            }
        }

        private void Price計算_支払()
        {
			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            //if (運転日報データ == null)
            //	return;

            
			
			if (cmb支払計算区分.SelectedValue == null)
            {
                return;
            }
            int result;
            
			int p車種ID = 0;
            int p計算区分 = (int)this.cmb支払計算区分.SelectedValue;
            int p請求支払区分 = 1;	// 支払
            int p得意先ID = (string.IsNullOrWhiteSpace(this.Si_SiharaiSaki.Text1)) ? 0 : AppCommon.IntParse(this.Si_SiharaiSaki.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p発地ID = (string.IsNullOrWhiteSpace(this.Se_HattiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_HattiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p着地ID = (string.IsNullOrWhiteSpace(this.Se_CyakuNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_CyakuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            int p商品ID = (string.IsNullOrWhiteSpace(this.Se_SyouhinNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_SyouhinNm.Text1, System.Globalization.NumberStyles.AllowThousands);
            if (He_SyashuNm.Text1 != null)
            {
				if (int.TryParse(He_SyashuNm.Text1, out result) == true)
                {
					p車種ID = (string.IsNullOrWhiteSpace(this.He_SyashuNm.Text1)) ? 0 : AppCommon.IntParse(this.He_SyashuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
                }
            }
            int p走行ＫＭ = (string.IsNullOrWhiteSpace(this.Se_SoukouKm.Text)) ? 0 : AppCommon.IntParse(this.Se_SoukouKm.Text, System.Globalization.NumberStyles.AllowThousands);
            decimal p数量 = (string.IsNullOrWhiteSpace(this.Se_Suuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Suuryou.Text);
            decimal p重量 = (string.IsNullOrWhiteSpace(this.Se_Jyuuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Jyuuryou.Text);
            decimal 単価 = (string.IsNullOrWhiteSpace(this.Si_SiharaiTanka.Text)) ? 0 : AppCommon.DecimalParse(this.Si_SiharaiTanka.Text);
            int 金額 = -1;

            /*
             p計算区分	表示名
                0		手入力
                1		数量計算
                2		重量計算
                3		運賃タリフ
                4		地区単価
                5		車種運賃
                6		個建単価
             */
            switch (p計算区分)
            {
                case 0:
                    // 手入力は何もしない
					break;
				case 1:
					// 数量 ｘ 単価
					switch (金額計算端数処理区分)
					{
					case 0:
						金額 = (int)Math.Floor((p数量 * 単価));
						break;
					case 1:
						金額 = (int)Math.Ceiling((p数量 * 単価));
						break;
					case 2:
						金額 = (int)Math.Round((p数量 * 単価), 0, MidpointRounding.AwayFromZero);
						break;
					}
					CInputTrn.支払金額 = 金額;
					//Se_UriageKingaku.Text = 金額.ToString(Se_UriageKingaku.Mask);
					break;
				case 2:
					// 重量 ｘ 単価
					switch (金額計算端数処理区分)
					{
					case 0:
						金額 = (int)Math.Floor((p重量 * 単価));
						break;
					case 1:
						金額 = (int)Math.Ceiling((p重量 * 単価));
						break;
					case 2:
						金額 = (int)Math.Round((p重量 * 単価), 0, MidpointRounding.AwayFromZero);
						break;
					}
					CInputTrn.支払金額 = 金額;
					//Se_UriageKingaku.Text = 金額.ToString(Se_UriageKingaku.Mask);
					break;
				case 3:
					if (p得意先ID != 0 && p重量 != 0 && p走行ＫＭ != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 4:
					if (p得意先ID != 0 && p商品ID != 0)//&& p重量 != 0 && p数量 != 0
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 5:
					if (p得意先ID != 0 && p車種ID != 0) // && p重量 != 0 && p数量 != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
				case 6:
					if (p得意先ID != 0 && p数量 != 0)
					{
						SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
					}
					break;
            }
        }

        private void 請求運賃計算区分_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmb計算区分.SelectedIndex < 0)
            {
                return;
            }
            if (this.cmb計算区分.SelectedIndex == 0)
            {
                this.Se_UriageKingaku.cIsReadOnly = false;
                return;
            }
            this.Se_UriageKingaku.cIsReadOnly = true;
            Price計算_請求();
        }

        private void 支払運賃計算区分_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmb支払計算区分.SelectedIndex < 0)
            {
                return;
            }
            if (this.cmb支払計算区分.SelectedIndex == 0)
            {
                this.Si_SiharaiKingaku.cIsReadOnly = false;
                return;
            }
            this.Si_SiharaiKingaku.cIsReadOnly = true;
            Price計算_支払();
        }

        private void 売上単価_LostFocus(object sender, RoutedEventArgs e)
        {
			if (Se_UriageTanka.Text == "")
			{
				this.売上単価 = 0;
			}
            Price計算_請求();
        }

        private void 発地ID_LostFocus(object sender, RoutedEventArgs e)
        {
            Price計算_請求();
        }

        private void 着地ID_LostFocus(object sender, RoutedEventArgs e)
        {
            Price計算_請求();
        }

        private void 数量_LostFocus(object sender, RoutedEventArgs e)
        {
			if (Se_Suuryou.Text == "")
			{
				this.数量 = 0;
			}
            Price計算_請求();
        }

        private void 重量_LostFocus(object sender, RoutedEventArgs e)
        {
			if (Se_Jyuuryou.Text == "")
			{
				this.重量 = 0;
			}

            Price計算_請求();
        }

        private void 走行ＫＭ_LostFocus(object sender, RoutedEventArgs e)
        {
            Price計算_請求();
        }

        private void 商品_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
            if (ctl == null)
            {
                return;
            }
			if (ctl.IsModified1 != true && ctl.IsModified != true)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(ctl.Text1) == true)
            {
                単位 = "";
            }
            else
            {
                int cid;
                if (int.TryParse(ctl.Text1, out cid) != true)
                {
                    return;
                }
                SendRequest(new CommunicationObject(MessageType.RequestData, GetHINDATA, cid, 0));
            }
            Price計算_請求();
        }

        private void 車種_LostFocus(object sender, RoutedEventArgs e)
        {
            Price計算_請求();
        }



        private void メーター数_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                return;

            //if (string.IsNullOrWhiteSpace(txtMeterFROM.Text) || string.IsNullOrWhiteSpace(txtMeterTO.Text))
            //{
            //	return;
            //}
            //if (string.IsNullOrWhiteSpace(Se_SoukouKm.Text) != true)
            //{
            //	return;
            //}
            //int mtrFr = AppCommon.IntParse(txtMeterFROM.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands);
            //int mtrTo = AppCommon.IntParse(txtMeterTO.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands);
            //if (mtrFr > mtrTo)
            //{
            //	return;
            //}
            //Se_SoukouKm.Text = (mtrTo - mtrFr).ToString();

        }


        private void 支払運賃計算(object sender, RoutedEventArgs e)
        {
            Price計算_支払();
        }


        private decimal SubtructDecimalTime(decimal from, decimal to)
        {
            decimal ret = from;

            int h1 = (int)Math.Truncate(from);
            int m1 = (int)((from - h1) * 100);
            int h2 = (int)Math.Truncate(to);
            int m2 = (int)((to - h2) * 100);
            TimeSpan ts1 = new TimeSpan(h1, m1, 0);
            TimeSpan ts2 = new TimeSpan(h2, m2, 0);
            TimeSpan ts = ts1 - ts2;

            ret = (decimal)ts.Hours + (decimal)(ts.Minutes) / 100;

            return ret;
        }


        private void spUriage_ColumnCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangedAction.Move)
            {
                if (e.OldStartingIndex < 3)
                {
                }
            }
        }

        private void 自社部門_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                //e.Handled = true;
                //this.Se_SeikyuHiduke.Focus();
            }
        }

        private void spread_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            if (e.EditAction != SpreadEditAction.Commit) return;

            if (e.CellPosition.ColumnName.Contains("数量") == true)
            {
            }
        }

        private void CARID_TextChanged2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                int cid;
                if (int.TryParse(ctl.Text1, out cid) != true)
                {
                    return;
                }
                SendRequest(new CommunicationObject(MessageType.RequestData, GetCARDATA2, cid));
            }
        }

        private void Se_TuukouRyou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
                Si_SiharaiHiduke.Focus();
            }
        }

        private void Si_SiharaiTuukouRyou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
				e.Handled = true;
				Update();
            }
            
        }

        private void Si_SiharaiSaki_cText1Changed(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Si_SiharaiSaki.Text1))
            {
                Si_SiharaiKingaku.Label_Context = "社内金額";
                Si_SiharaiTuukouRyou.Label_Context = "社内通行料";
            }
            else
            {
                Si_SiharaiKingaku.Label_Context = "支払金額";
                Si_SiharaiTuukouRyou.Label_Context = "支払通行料";
            }


            //if (IsGridWorking)
            //    return;

            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
            if (ctl == null)
            {
                return;
            }
            int cid;
            if (int.TryParse(ctl.Text1, out cid) != true)
            {
                return;
            }
            SendRequest(new CommunicationObject(MessageType.RequestData, GetYOSDATA, cid, 0));

        }




		private void Jyunjyo_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab)
			{
				if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) == KeyStates.Down || (Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) == KeyStates.Down)
				{
					e.Handled = true;
				}
			}
			if (e.Key == Key.Enter)
			{
				TantoShitei.SetFocus();
			}
		}

		private void TantoShitei_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab)
			{

			}
			if (e.Key == Key.Enter)
			{
				CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
				base.SendRequest(com);
			}
		}

		private void TokuShitei_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			//if (e.Key == Key.Tab)
			//{
			//	if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) != KeyStates.Down && (Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) != KeyStates.Down)
			//	{
			//		e.Handled = true;
			//	}
			//}

			if (e.Key == Key.Enter)
			{
				CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
				base.SendRequest(com);
				//e.Handled = true;
			}

		}

		private void Jyunjyo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
			base.SendRequest(com);

		}



		private void spRireki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

			GrapeCity.Windows.SpreadGrid.HitTestInfo hinfo = spRireki.HitTest(e.GetPosition(spRireki));
			if (hinfo is GrapeCity.Windows.SpreadGrid.CellHitTestInfo)
			{
				GrapeCity.Windows.SpreadGrid.CellHitTestInfo cellinfo = (GrapeCity.Windows.SpreadGrid.CellHitTestInfo)hinfo;
				if (cellinfo.Area == GrapeCity.Windows.SpreadGrid.SpreadArea.Cells)
				{
					var a = spRireki.Cells[cellinfo.RowIndex, "明細番号"].Value.ToString();
					var b = spRireki.Cells[cellinfo.RowIndex, "行"].Value.ToString();
					if (MaintenanceMode == null)
					{
						DetailsNumber = a;
						DetailsGyou = b;
						He_MeisaiGyou.Focus();
					}
					if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
					{
						RefferNumber = a;
						RefferGyou = b;
						He_RuijiMeisaiGyou.Focus();
					}

					Grid_RIREKI.Visibility = Visibility.Collapsed;
				}

			}

		}

		private void spRireki_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (spRireki.ActiveRow.Index == -1)
				{
					return;
				}
				else
				{
					var a = spRireki.Cells[spRireki.ActiveRow.Index, "明細番号"].Value.ToString();
					var b = spRireki.Cells[spRireki.ActiveRow.Index, "行"].Value.ToString();
					if (MaintenanceMode == null)
					{
						DetailsNumber = a;
						DetailsGyou = b;
						He_MeisaiBangou.Focus();
					}
					if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
					{
						RefferNumber = a;
						RefferGyou = b;
						He_RuijiMeisaiBangou.Focus();
					}

					Grid_RIREKI.Visibility = Visibility.Collapsed;
				}
			}

		}

		private void cmb支払未定区分_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (乗務員ID > 0)
				{
					Si_SiharaiKingaku.Focus();
				}
			}
		}



    }
}
