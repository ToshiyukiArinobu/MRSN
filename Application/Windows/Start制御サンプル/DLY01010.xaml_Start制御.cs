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
using GrapeCity.Windows.SpreadGrid.Editors;
using System.Reflection;
using System.Windows.Threading;



namespace KyoeiSystem.Application.Windows.Views
{


	/// <summary>
	/// 運転日報入力
	/// </summary>
	public partial class DLY01010 : RibbonWindowViewBase
	{

		public class DLY01010_RIREKI : INotifyPropertyChanged
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

        public class T03_KTRN_Member : INotifyPropertyChanged
        {
			private int _明細番号 { get; set; }
			private int _明細行 { get; set; }
			private DateTime? _登録日時 { get; set; }
			private DateTime? _更新日時 { get; set; }
			private int _明細区分 { get; set; }
			private int _入力区分 { get; set; }
			private DateTime? _経費発生日 { get; set; }
			private int? _車輌ID { get; set; }
			private string _車輌番号 { get; set; }
			private int? _乗務員ID { get; set; }
			private int? _支払先ID { get; set; }
			private int? _自社部門ID { get; set; }
			private int? _経費項目ID { get; set; }
			private string _経費補助名称 { get; set; }
			private decimal _単価 { get; set; }
			private decimal? _内軽油税分 { get; set; }
			private decimal? _数量 { get; set; }
			private int? _金額 { get; set; }
			private int? _収支区分 { get; set; }
			private int? _摘要ID { get; set; }
			private string _摘要名 { get; set; }
			private int? _入力者ID { get; set; }
			private string _S_経費項目ID { get; set; }
			private string _S_支払先ID { get; set; }
			private string _S_摘要ID { get; set; }
			private string _S_単価 { get; set; }
			private string _S_内軽油税分 { get; set; }
			private string _S_数量 { get; set; }
			private string _S_金額 { get; set; }
			private string _経費発生年月日 { get; set; }
			private string _経費項目名 { get; set; }
			private string _支払先名 { get; set; }
			private decimal _軽油取引税率 { get; set; }



            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public DateTime? 登録日時 { get { return _登録日時; } set { _登録日時 = value; NotifyPropertyChanged(); } }
            public DateTime? 更新日時 { get { return _更新日時; } set { _更新日時 = value; NotifyPropertyChanged(); } }
            public int 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }
            public int 入力区分 { get { return _入力区分; } set { _入力区分 = value; NotifyPropertyChanged(); } }
            public DateTime? 経費発生日 { get { return _経費発生日; } set { _経費発生日 = value; NotifyPropertyChanged(); } }
            public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
            public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
            public int? 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
            public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value; NotifyPropertyChanged(); } }
            public int? 経費項目ID { get { return _経費項目ID; } set { _経費項目ID = value; NotifyPropertyChanged(); } }
            public string 経費補助名称 { get { return _経費補助名称; } set { _経費補助名称 = value; NotifyPropertyChanged(); } }
            public decimal 単価 { get { return _単価; } set { _単価 = value; NotifyPropertyChanged(); } }
            public decimal? 内軽油税分 { get { return _内軽油税分; } set { _内軽油税分 = value; NotifyPropertyChanged(); } }
            public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            public int? 金額 { get { return _金額; } set { _金額 = value; NotifyPropertyChanged(); } }
            public int? 収支区分 { get { return _収支区分; } set { _収支区分 = value; NotifyPropertyChanged(); } }
            public int? 摘要ID { get { return _摘要ID; } set { _摘要ID = value; NotifyPropertyChanged(); } }
            public string 摘要名 { get { return _摘要名; } set { _摘要名 = value; NotifyPropertyChanged(); } }
            public int? 入力者ID { get { return _入力者ID; } set { _入力者ID = value; NotifyPropertyChanged(); } }
            public string S_経費項目ID { get { return _S_経費項目ID; } set { _S_経費項目ID = value; NotifyPropertyChanged(); } }
            public string S_支払先ID { get { return _S_支払先ID; } set { _S_支払先ID = value; NotifyPropertyChanged(); } }
            public string S_摘要ID { get { return _S_摘要ID; } set { _S_摘要ID = value; NotifyPropertyChanged(); } }
            public string S_単価 { get { return _S_単価; } set { _S_単価 = value; NotifyPropertyChanged(); } }
            public string S_内軽油税分 { get { return _S_内軽油税分; } set { _S_内軽油税分 = value; NotifyPropertyChanged(); } }
            public string S_数量 { get { return _S_数量; } set { _S_数量 = value; NotifyPropertyChanged(); } }
            public string S_金額 { get { return _S_金額; } set { _S_金額 = value; NotifyPropertyChanged(); } }
            public string 経費発生年月日 { get { return _経費発生年月日; } set { _経費発生年月日 = value; NotifyPropertyChanged(); } }
            public string 経費項目名 { get { return _経費項目名; } set { _経費項目名 = value; NotifyPropertyChanged(); } }
            public string 支払先名 { get { return _支払先名; } set { _支払先名 = value; NotifyPropertyChanged(); } }
            public decimal 軽油取引税率 { get { return _軽油取引税率; } set { _軽油取引税率 = value; NotifyPropertyChanged(); } }

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



		#region SPREADクリック時に入力項目を展開する際にイベント完了を待つ機能用
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
		public class ConfigDLY01010 : FormConfigBase
		{
			public int? 表示順指定 = 0;
			public int? 担当者指定 = null;
			public int? 得意先指定 = null;
			public byte[] spUriageConfig = null;
			public byte[] spKeihiConfig = null;
			public byte[] spRirekiConfig20160419 = null;
		}

		/// ※ 必ず public で定義する。
		public ConfigDLY01010 frmcfg = null;

		#endregion

		// SPREAD初期状態保存用
		private byte[] spUriageConfig = null;
		private byte[] spKeihiConfig = null;
		private byte[] spRirekiConfig = null;

		#region Const
		private const string TRN_RIREKI = "DLY01010_TRN_RIREKI";

		private const string GET_CNTL = "M87_CNTL";
		private const string GetMaxNo = "DLY01010_MAXNO";
		private const string GetMeisaiNo = "DLY01010_GETNO";

		private const string GetNippou = "DLY01010_1";
		private const string GetDrvLog = "DLY01010_2";
		//private const string GetKeihiLog = "DLY01010_3";
		//private const string GetKeihiDef = "DLY01010_DEFAULT_K";
		private const string GetKeihiName = "DLY01010_KEIHI_NAME";
		private const string GetShrName = "DLY01010_SHR_NAME";
		private const string GetTekiyoName = "DLY01010_TEKIYO_NAME";
		private const string GetKeiyuZeiritu = "DLY01010_KEIYU_ZEI";
		private const string GetCARDATA = "M05_CAR_UC";
		private const string GetHINDATA = "M09_HIN_UC";
		private const string GetTOKDATA = "M_M01_TOK";
		private const string GetYOSDATA = "M_M01_YOS";
		private const string PutAllData = "DLY01010_PUTALL";
		private const string DeleteAllData = "DLY01010_DELALL";
		private const string GetTanka = "DLY01010_TANKA";
        private const string GetMaxMeisaiNo = "DLY01010_GetMaxMeisaiNo";
		#endregion

		private bool shiharai_focus_kbn = true;

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
            public int? _確認名称区分;
            public int? 確認名称区分 { get { return _確認名称区分; } set { _確認名称区分 = value; NotifyPropertyChanged(); } }
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

			public DateTime? _実運行日開始;
			public DateTime? 実運行日開始 { get { return _実運行日開始; } set { _実運行日開始 = value; NotifyPropertyChanged(); } }
			public DateTime? _実運行日終了;
			public DateTime? 実運行日終了 { get { return _実運行日終了; } set { _実運行日終了 = value; NotifyPropertyChanged(); } }
			public decimal? _出庫時間;
			public decimal? 出庫時間 { get { return _出庫時間; } set { _出庫時間 = value; NotifyPropertyChanged(); } }
			public decimal? _帰庫時間;
			public decimal? 帰庫時間 { get { return _帰庫時間; } set { _帰庫時間 = value; NotifyPropertyChanged(); } }
			public int _出勤区分ID;
			public int 出勤区分ID { get { return _出勤区分ID; } set { _出勤区分ID = value; NotifyPropertyChanged(); } }
			public decimal? _拘束時間;
			public decimal? 拘束時間 { get { return _拘束時間; } set { _拘束時間 = value; NotifyPropertyChanged(); } }
			public decimal? _運転時間;
			public decimal? 運転時間 { get { return _運転時間; } set { _運転時間 = value; NotifyPropertyChanged(); } }
			public decimal? _高速時間;
			public decimal? 高速時間 { get { return _高速時間; } set { _高速時間 = value; NotifyPropertyChanged(); } }
			public decimal? _作業時間;
			public decimal? 作業時間 { get { return _作業時間; } set { _作業時間 = value; NotifyPropertyChanged(); } }
			public decimal? _休憩時間;
			public decimal? 休憩時間 { get { return _休憩時間; } set { _休憩時間 = value; NotifyPropertyChanged(); } }
			public decimal? _残業時間;
			public decimal? 残業時間 { get { return _残業時間; } set { _残業時間 = value; NotifyPropertyChanged(); } }
			public decimal? _深夜時間;
			public decimal? 深夜時間 { get { return _深夜時間; } set { _深夜時間 = value; NotifyPropertyChanged(); } }
			public decimal? _輸送屯数;
			public decimal? 輸送屯数 { get { return _輸送屯数; } set { _輸送屯数 = value; NotifyPropertyChanged(); } }
			public int? _出庫ＫＭ;
			public int? 出庫ＫＭ { get { return _出庫ＫＭ; } set { _出庫ＫＭ = value; NotifyPropertyChanged(); } }
			public int? _帰庫ＫＭ;
			public int? 帰庫ＫＭ { get { return _帰庫ＫＭ; } set { _帰庫ＫＭ = value; NotifyPropertyChanged(); } }
			public string _備考;
			public string 備考 { get { return _備考; } set { _備考 = value; NotifyPropertyChanged(); } }
			public DateTime? _勤務開始日;
			public DateTime? 勤務開始日 { get { return _勤務開始日; } set { _勤務開始日 = value; NotifyPropertyChanged(); } }
			public DateTime? _勤務終了日;
			public DateTime? 勤務終了日 { get { return _勤務終了日; } set { _勤務終了日 = value; NotifyPropertyChanged(); } }
			public DateTime? _労務日;
			public DateTime? 労務日 { get { return _労務日; } set { _労務日 = value; NotifyPropertyChanged(); } }
			public int? _請求内訳管理区分;
			public int? 請求内訳管理区分 { get { return _請求内訳管理区分; } set { _請求内訳管理区分 = value; NotifyPropertyChanged(); } }

			public TRN()
			{
				this.請求運賃計算区分ID = 0;
				this.支払運賃計算区分ID = 0;
				this.社内区分 = 0;
                this.確認名称区分 = 0;
				this.請求税区分 = 0;
				this.支払税区分 = 0;
				this.売上未定区分 = 0;
				this.支払未定区分 = 0;
				this.出勤区分ID = 0;

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
		public int 担当者ID = 0;
		public int? 金額計算端数処理区分 = null;

		#region BindingMember

		private Visibility _類似番号入力表示 = Visibility.Hidden;
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

		private DataTable _dUriageData = null;
		private DataTable _dLogData = null;
        private List<T03_KTRN_Member> _dKeihiData = null;
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
					this.運転日報データ = null;
					//this.運転日報データ = _dInputData.NewRow();
					//_dInputData.Rows.Add(this.運転日報データ);
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
                    CInputTrn.確認名称区分 = CInputTrn.確認名称区分 == null ? 0 : CInputTrn.確認名称区分;
					CInputTrn.請求税区分 = CInputTrn.請求税区分 == null ? 0 : CInputTrn.請求税区分;
					CInputTrn.支払税区分 = CInputTrn.支払税区分 == null ? 0 : CInputTrn.支払税区分;
					CInputTrn.売上未定区分 = CInputTrn.売上未定区分 == null ? 0 : CInputTrn.売上未定区分;
					CInputTrn.支払未定区分 = CInputTrn.支払未定区分 == null ? 0 : CInputTrn.支払未定区分;
					CInputTrn.出勤区分ID = CInputTrn.出勤区分ID == null ? 0 : CInputTrn.出勤区分ID;

				}
			}
		}

        public List<T03_KTRN_Member> DKeihiData
		{

            get
            {
                return this._dKeihiData;
            }
            set
            {
                this._dKeihiData = value;
                this.spKeihiGrid.ItemsSource = value;
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

		private int? _ineNumber = null;
		public int? LineNumber
		{
			get
			{
				return _ineNumber;
			}
			set
			{
				this._ineNumber = value;
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

		private List<DLY01010_RIREKI> _売上明細データ = null;
		public List<DLY01010_RIREKI> 売上明細データ
		{
			get
			{
				return this._売上明細データ;
			}
			set
			{
				this._売上明細データ = value;
				//this.spRireki.ItemsSource = value;
				NotifyPropertyChanged();
			}
		}

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

		private int? _表示順 = null;
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
			set {

				UIElement element = Keyboard.FocusedElement as UIElement;

				this._内訳Enabled = value; NotifyPropertyChanged();
				if (_内訳Enabled == true)
				{
					Se_SeikyuuUtiwakeNm.IsRequired = true;
				} else 
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
		private int? _車輌ID = null;
		public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
		private int? _車種ID = null;
		public int? 車種ID { get { return _車種ID; } set { _車種ID = value; NotifyPropertyChanged(); } }
		private int? _乗務員ID = null;
		public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
		private int? _自社部門ID = null;
		public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value; NotifyPropertyChanged(); } }
		private string _車輌番号 = null;
		public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
		private int? _出勤区分ID = null;
		public int? 出勤区分ID { get { return _出勤区分ID; } set { _出勤区分ID = value; NotifyPropertyChanged(); } }
		private decimal? _出庫時間 = null;
		public decimal? 出庫時間 { get { return _出庫時間; } set { _出庫時間 = value; NotifyPropertyChanged(); } }
		private decimal? _帰庫時間 = null;
		public decimal? 帰庫時間 { get { return _帰庫時間; } set { _帰庫時間 = value; NotifyPropertyChanged(); } }

		private string _単位 = null;
		public string 単位 { get { return _単位; } set { _単位 = value; NotifyPropertyChanged(); } }

		private int? _走行ＫＭ = null;
		public int? 走行ＫＭ { get { return _走行ＫＭ; } set { _走行ＫＭ = value; NotifyPropertyChanged(); } }
		private int? _実車ＫＭ = null;
		public int? 実車ＫＭ { get { return _実車ＫＭ; } set { _実車ＫＭ = value; NotifyPropertyChanged(); } }

		private decimal? _拘束時間;
		public decimal? 拘束時間 { get { return _拘束時間; } set { _拘束時間 = value; NotifyPropertyChanged(); } }
		private decimal? _運転時間;
		public decimal? 運転時間 { get { return _運転時間; } set { _運転時間 = value; NotifyPropertyChanged(); } }
		private decimal? _高速時間;
		public decimal? 高速時間 { get { return _高速時間; } set { _高速時間 = value; NotifyPropertyChanged(); } }
		private decimal? _作業時間;
		public decimal? 作業時間 { get { return _作業時間; } set { _作業時間 = value; NotifyPropertyChanged(); } }
		private decimal? _待機時間;
		public decimal? 待機時間 { get { return _待機時間; } set { _待機時間 = value; NotifyPropertyChanged(); } }
		private decimal? _休憩時間;
		public decimal? 休憩時間 { get { return _休憩時間; } set { _休憩時間 = value; NotifyPropertyChanged(); } }
		private decimal? _残業時間;
		public decimal? 残業時間 { get { return _残業時間; } set { _残業時間 = value; NotifyPropertyChanged(); } }
		private decimal? _深夜時間;
		public decimal? 深夜時間 { get { return _深夜時間; } set { _深夜時間 = value; NotifyPropertyChanged(); } }
		private decimal? _輸送屯数;
		public decimal? 輸送屯数 { get { return _輸送屯数; } set { _輸送屯数 = value; NotifyPropertyChanged(); } }

		private string _乗務員名 = null;
		public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
		private string _経費名称 = null;
		public string 経費名称 { get { return _経費名称; } set { _経費名称 = value; NotifyPropertyChanged(); } }
		private int? _メーター開始 = null;
		public int? メーター開始 { get { return _メーター開始; } set { _メーター開始 = value; NotifyPropertyChanged(); メーター計算(); } }
		private int? _メーター終了 = null;
		public int? メーター終了 { get { return _メーター終了; } set { _メーター終了 = value; NotifyPropertyChanged(); メーター計算(); } }
		void メーター計算()
		{
			if (メーター開始 == null || メーター終了 == null) { 走行ＫＭ = 0; return; }
			if (メーター開始 <= メーター終了) { 走行ＫＭ = メーター終了 - メーター開始; } else { 走行ＫＭ = 0; }
		}

		private int? _請求路線計算年度 = null;
		public int? 請求路線計算年度 { get { return _請求路線計算年度; } set { _請求路線計算年度 = value; NotifyPropertyChanged(); } }
		private int? _支払路線計算年度 = null;
		public int? 支払路線計算年度 { get { return _支払路線計算年度; } set { _支払路線計算年度 = value; NotifyPropertyChanged(); } }

		#endregion

		#endregion

		bool IsRfferMode = false;
		bool IsGridWorking = false;
		bool IsTranProc = false;

		#region 明細クリック時のアクション定義
		/// <summary>
		/// 明細クリック時のアクション定義
		/// </summary>
		public class cmd売上削除 : ICommand
		{
			private GcSpreadGrid _gcSpreadGrid;
			public cmd売上削除(GcSpreadGrid gcSpreadGrid)
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
				try
				{
					this._gcSpreadGrid.IsEnabled = false;
					CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
					if (cellCommandParameter.Area == SpreadArea.Cells
						&& cellCommandParameter.CellPosition.Row >= 0
						&& cellCommandParameter.CellPosition.Row < this._gcSpreadGrid.Rows.Count
						)
					{
						this._gcSpreadGrid.SelectedItems.Clear();
						this._gcSpreadGrid.Rows.Remove(cellCommandParameter.CellPosition.Row);
						var wnd = GetWindow(this._gcSpreadGrid) as DLY01010;
						if (wnd != null)
						{
							wnd.ClearUriageItems();
						}
					}
				}
				catch (Exception ex)
				{
				}
				finally
				{
					this._gcSpreadGrid.IsEnabled = true;
				}
			}
		}

		/// <summary>
		/// 明細クリック時のアクション定義
		/// </summary>
		public class cmd売上複写 : ICommand
		{
			private GcSpreadGrid _gcSpreadGrid;
			public cmd売上複写(GcSpreadGrid gcSpreadGrid)
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
					this._gcSpreadGrid.Select(new CellRange(cellCommandParameter.CellPosition.Row,1), SelectionType.New);
					var wnd = GetWindow(this._gcSpreadGrid) as DLY01010;
					if (wnd == null)
					{
						return;
					}
					wnd.CopyUriageRow(cellCommandParameter.CellPosition.Row, true);
				}
			}
		}

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
				if (cellCommandParameter.Area == SpreadArea.Cells
					&& cellCommandParameter.CellPosition.Row >= 0
					&& cellCommandParameter.CellPosition.Row < this._gcSpreadGrid.Rows.Count
					)
				{
					this._gcSpreadGrid.CancelRowEdit();
					this._gcSpreadGrid.Rows.Remove(cellCommandParameter.CellPosition.Row);
				}
			}
		}

		#endregion

		public void CopyUriageRow(int rowno, bool newflag)
		{
			IsGridWorking = true;

			appLog.Debug("CopyUriageRow: ActiveRow:{0}, Cell:{1}", spUriage.ActiveCellPosition.Row, spUriage.ActiveCellPosition.Column);

			this.Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			this.Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			this.Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			this.Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			this.Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			this.Se_SeikyuuUtiwakeNm.DataAccessMode = Framework.Windows.Controls.OnOff.Off;
			int gyo = (int)this.spUriage.Rows[rowno].Cells[2].Value;
			int lineno = gyo;
			DataRow cprow = null;
			foreach (DataRow row in this.DUriageData.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
					continue;
				int rgyo = (int)row["明細行"];
				if (rgyo == gyo)
				{
					cprow = row;
				}
				if (lineno < rgyo)
				{
					lineno = rgyo;
				}
			}
			_dInputData.Rows.Clear();
			_dInputData.ImportRow(cprow);
			this.運転日報データ = _dInputData.Rows[0];
			this.内訳検索用得意先ID = this.CInputTrn.得意先ID.ToString();
			if (this.CInputTrn.請求内訳管理区分==null)
			{
				this.内訳Enabled = false;
			}
			else
			{
				this.内訳Enabled = ((int)this.CInputTrn.請求内訳管理区分 == 1) ? true : false;
			}
			if (newflag)
			{
				this.LineNumber = lineno + 1;
			}
			else
			{
				this.LineNumber = gyo;
			}
			this.Se_SeikyuHiduke.Focus();

			SpreadResetSelection(this.spUriage);

			this.Se_HattiNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			this.Se_CyakuNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			this.Se_SyouhinNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			this.Se_SeikyuuTekiyou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			this.Se_Bikou.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			this.Se_SeikyuuUtiwakeNm.DataAccessMode = Framework.Windows.Controls.OnOff.On;
			IsGridWorking = false;
		}

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
		public DLY01010()
		{
			InitializeComponent();
			this.DataContext = this;    
		}

        CommonConfig ccfg = null;

		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.spRireki.Rows.Clear();

			Grid_RIREKI.Visibility = Visibility.Collapsed;

			this.spUriage.RowCount = 0;
			this.spKeihiGrid.RowCount = 0;
			this.spRireki.RowCount = 0;
			this.spUriageConfig = AppCommon.SaveSpConfig(this.spUriage);
			this.spKeihiConfig = AppCommon.SaveSpConfig(this.spKeihiGrid);
			this.spRirekiConfig = AppCommon.SaveSpConfig(this.spRireki);

			#region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
			frmcfg = (ConfigDLY01010)ucfg.GetConfigValue(typeof(ConfigDLY01010));
            担当者ID = ccfg.ユーザID;
			if (frmcfg == null)
			{
				frmcfg = new ConfigDLY01010();
				ucfg.SetConfigValue(frmcfg);
				frmcfg.spUriageConfig = this.spUriageConfig;
				frmcfg.spKeihiConfig = this.spKeihiConfig;
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
			if (frmcfg.spUriageConfig != null)
			{
				AppCommon.LoadSpConfig(this.spUriage, frmcfg.spUriageConfig);
			}
			if (frmcfg.spKeihiConfig != null)
			{
				AppCommon.LoadSpConfig(this.spKeihiGrid, frmcfg.spKeihiConfig);
			}
			if (frmcfg.spRirekiConfig20160419 != null)
			{
				AppCommon.LoadSpConfig(this.spRireki, frmcfg.spRirekiConfig20160419);
			}
			担当者ID指定 = frmcfg.担当者指定;
			得意先ID指定 = frmcfg.得意先指定;
			表示順 = frmcfg.表示順指定;
			#endregion

			this.spUriage.RowCount = 0;
			this.spKeihiGrid.RowCount = 0;
			this.spUriage.PreviewKeyDown += spUriage_PreviewKeyDown;
			this.spKeihiGrid.PreviewKeyDown += spKeihi_PreviewKeyDown;
			foreach (var cell in this.spKeihiGrid.Cells.Columns)
			{
				var v = cell.CellValidator;
			}

			ButtonCellType btn1 = this.spUriage.Columns[0].CellType as ButtonCellType;
			btn1.Command = new cmd売上削除(spUriage);
			ButtonCellType btn2 = this.spUriage.Columns[1].CellType as ButtonCellType;
			btn2.Command = new cmd売上複写(spUriage);

			ButtonCellType btn3 = this.spKeihiGrid.Columns[0].CellType as ButtonCellType;
			btn3.Command = new cmd経費削除(spKeihiGrid);

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
			base.MasterMaintenanceWindowList.Add("M72_TNT_SCH", new List<Type> { typeof(MST13010), typeof(SCH13010) });
			

			GetComboBoxItems();

			base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CNTL, 1, 0));


			ScreenClear();
			ChangeKeyItemChangeable(true);
            Txt登録件数.Focusable = false;
			SetFocusToTopControl();

			if (初期明細番号 != null)
			{
				DetailsNumber = 初期明細番号.ToString();
				this.LineNumber = 1;
				類似番号入力表示 = System.Windows.Visibility.Hidden;
				this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
				GetMeisaiData();
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
			AppCommon.SetutpComboboxListToCell(this.spUriage, "請求運賃計算区分ID", "日次", "運転日報入力", "計算区分", false);
			AppCommon.SetutpComboboxListToCell(this.spUriage, "売上未定区分", "日次", "運転日報入力", "未定区分", false);
			AppCommon.SetutpComboboxListToCell(this.spUriage, "社内区分", "日次", "運転日報入力", "社内区分", false);
            AppCommon.SetutpComboboxListToCell(this.spUriage, "確認名称区分", "マスタ", "基礎情報設定", "確認名称", false);
			AppCommon.SetutpComboboxListToCell(this.spUriage, "請求税区分", "日次", "運転日報入力", "税区分", false);
			AppCommon.SetutpComboboxListToCell(this.spUriage, "支払税区分", "日次", "運転日報入力", "税区分", false);
			AppCommon.SetutpComboboxList(this.cmb計算区分);
			AppCommon.SetutpComboboxList(this.cmb支払計算区分);
			AppCommon.SetutpComboboxList(this.cmb出勤区分);
			AppCommon.SetutpComboboxList(this.cmb税区分);
            AppCommon.SetutpComboboxList(this.cmb社内区分);
            AppCommon.SetutpComboboxList(this.cmb確認名称);
			AppCommon.SetutpComboboxList(this.cmb未定区分);
			AppCommon.SetutpComboboxList(this.cmb支払税区分);
			AppCommon.SetutpComboboxList(this.cmb支払未定区分);
		}


		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message"></param>
		public override void OnReceivedResponseData(CommunicationObject message)
		{
			this.ErrorMessage = string.Empty;
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
			case GET_CNTL:
				AppCommon.SetupSpreadHeaderWarimasiName(this.spUriage, tbl);
				this.Se_Warimasi1.Label_Context = AppCommon.GetWarimasiName1(tbl);
				this.Se_Warimasi2.Label_Context = AppCommon.GetWarimasiName2(tbl);
				金額計算端数処理区分 = (int)tbl.Rows[0]["金額計算端数区分"];
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
					this.LineNumber = 1;
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
				int no = (int)data;
				if (no < 1)
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
					this.DetailsNumber = no.ToString();
					this.LineNumber = 1;
					類似番号入力表示 = System.Windows.Visibility.Hidden;
					this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
					GetMeisaiData();
				}
				break;
			case GetNippou:
				DataSet ds = (data is DataSet) ? (data as DataSet) : null;
				if (ds == null)
				{
					return;
				}
				if (ds.Tables["T02"].Rows.Count == 0)
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

				DUriageData = ds.Tables["T01"];
				DLogData = ds.Tables["T02"];
                int 入力者ID;
                if (DLogData.Rows.Count > 0)
                {
                    if (int.TryParse(DLogData.Rows[0]["入力者ID"].ToString(), out 入力者ID))
                    {
                        担当者ID = 入力者ID;
                    }
                }

				DKeihiData = null;
                DKeihiData = (List<T03_KTRN_Member>)AppCommon.ConvertFromDataTable(typeof(List<T03_KTRN_Member>), ds.Tables["KEIHI"]);

				this.spUriage.ItemsSource = this.DUriageData.DefaultView;
				//this.spKeihiGrid.ItemsSource = this.DKeihiData.DefaultView;
				if (DLogData == null)
				{
					this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
					return;
				}
				if (DUriageData.Rows.Count > 0)
				{
                    int gyo = (int)DUriageData.Rows[DUriageData.Rows.Count - 1]["明細行"];
                    this.LineNumber = gyo + 1;
                    int icnt = 0;
                    int cnt = 0;

                    if (初期行番号 != null)
                    {
                        foreach (DataRow row in this.DUriageData.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted)
                                continue;
                            if ((int)row["明細行"] == 初期行番号)
                            {
                                LineNumber = (int)row["明細行"];
                                cnt = icnt;


                                _dInputData.Rows.Clear();
                                DoEvents();
                                _dInputData.ImportRow(row);
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

                            }
                            icnt += 1;
                        }
                        CopyUriageRow(cnt, false);
                        DoEvents();
                        CopyUriageRow(cnt, false);
                        DoEvents();

                        初期行番号 = null;
                        if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
                        {
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        }

                        DataRow S_row = DLogData.Rows[0];
                        this.乗務員ID = S_row.IsNull("乗務員ID") ? (int?)null : (int?)S_row["乗務員ID"];
                        this.出庫時間 = S_row.IsNull("出庫時間") ? (decimal?)null : (decimal?)S_row["出庫時間"];
                        this.帰庫時間 = S_row.IsNull("帰庫時間") ? (decimal?)null : (decimal?)S_row["帰庫時間"];
                        this.出勤区分ID = S_row.IsNull("出勤区分ID") ? (int?)null : (int?)S_row["出勤区分ID"];
                        this.自社部門ID = S_row.IsNull("自社部門ID") ? (int?)null : (int?)S_row["自社部門ID"];
                        this.車輌ID = S_row.IsNull("車輌ID") ? (int?)null : (int?)S_row["車輌ID"];
                        this.車輌番号 = S_row.IsNull("車輌番号") ? (string)null : (string)S_row["車輌番号"];
                        this.車種ID = S_row.IsNull("車種ID") ? (int?)null : (int?)S_row["車種ID"];
                        this.メーター開始 = S_row.IsNull("出庫ＫＭ") ? (int?)null : (int?)S_row["出庫ＫＭ"];
                        this.メーター終了 = S_row.IsNull("帰庫ＫＭ") ? (int?)null : (int?)S_row["帰庫ＫＭ"];
                        this.運行開始日 = S_row.IsNull("実運行日開始") ? (DateTime?)null : (DateTime?)S_row["実運行日開始"];
                        this.運行終了日 = S_row.IsNull("実運行日終了") ? (DateTime?)null : (DateTime?)S_row["実運行日終了"];
                        this.勤務日FROM = S_row.IsNull("勤務開始日") ? (DateTime?)null : (DateTime?)S_row["勤務開始日"];
                        this.勤務日TO = S_row.IsNull("勤務終了日") ? (DateTime?)null : (DateTime?)S_row["勤務終了日"];
                        this.労務日 = S_row.IsNull("労務日") ? (DateTime?)null : (DateTime?)S_row["労務日"];
                        this.拘束時間 = S_row.IsNull("拘束時間") ? (decimal?)null : (decimal?)S_row["拘束時間"];
                        this.運転時間 = S_row.IsNull("運転時間") ? (decimal?)null : (decimal?)S_row["運転時間"];
                        this.高速時間 = S_row.IsNull("高速時間") ? (decimal?)null : (decimal?)S_row["高速時間"];
                        this.作業時間 = S_row.IsNull("作業時間") ? (decimal?)null : (decimal?)S_row["作業時間"];
                        this.待機時間 = S_row.IsNull("待機時間") ? (decimal?)null : (decimal?)S_row["待機時間"];
                        this.休憩時間 = S_row.IsNull("休憩時間") ? (decimal?)null : (decimal?)S_row["休憩時間"];
                        this.残業時間 = S_row.IsNull("残業時間") ? (decimal?)null : (decimal?)S_row["残業時間"];
                        this.深夜時間 = S_row.IsNull("深夜時間") ? (decimal?)null : (decimal?)S_row["深夜時間"];
                        this.輸送屯数 = S_row.IsNull("輸送屯数") ? (decimal?)null : (decimal?)S_row["輸送屯数"];
                        this.走行ＫＭ = S_row.IsNull("走行ＫＭ") ? (int?)null : (int?)S_row["走行ＫＭ"];
                        this.実車ＫＭ = S_row.IsNull("実車ＫＭ") ? (int?)null : (int?)S_row["実車ＫＭ"];
                        類似番号入力表示 = System.Windows.Visibility.Hidden;
                        DoEvents();
                        this.乗務員ID = S_row.IsNull("乗務員ID") ? (int?)null : (int?)S_row["乗務員ID"];
                        this.出庫時間 = S_row.IsNull("出庫時間") ? (decimal?)null : (decimal?)S_row["出庫時間"];
                        this.帰庫時間 = S_row.IsNull("帰庫時間") ? (decimal?)null : (decimal?)S_row["帰庫時間"];
                        this.出勤区分ID = S_row.IsNull("出勤区分ID") ? (int?)null : (int?)S_row["出勤区分ID"];
                        this.自社部門ID = S_row.IsNull("自社部門ID") ? (int?)null : (int?)S_row["自社部門ID"];
                        this.車輌ID = S_row.IsNull("車輌ID") ? (int?)null : (int?)S_row["車輌ID"];
                        this.車輌番号 = S_row.IsNull("車輌番号") ? (string)null : (string)S_row["車輌番号"];
                        this.車種ID = S_row.IsNull("車種ID") ? (int?)null : (int?)S_row["車種ID"];
                        this.メーター開始 = S_row.IsNull("出庫ＫＭ") ? (int?)null : (int?)S_row["出庫ＫＭ"];
                        this.メーター終了 = S_row.IsNull("帰庫ＫＭ") ? (int?)null : (int?)S_row["帰庫ＫＭ"];
                        this.運行開始日 = S_row.IsNull("実運行日開始") ? (DateTime?)null : (DateTime?)S_row["実運行日開始"];
                        this.運行終了日 = S_row.IsNull("実運行日終了") ? (DateTime?)null : (DateTime?)S_row["実運行日終了"];
                        this.勤務日FROM = S_row.IsNull("勤務開始日") ? (DateTime?)null : (DateTime?)S_row["勤務開始日"];
                        this.勤務日TO = S_row.IsNull("勤務終了日") ? (DateTime?)null : (DateTime?)S_row["勤務終了日"];
                        this.労務日 = S_row.IsNull("労務日") ? (DateTime?)null : (DateTime?)S_row["労務日"];
                        this.拘束時間 = S_row.IsNull("拘束時間") ? (decimal?)null : (decimal?)S_row["拘束時間"];
                        this.運転時間 = S_row.IsNull("運転時間") ? (decimal?)null : (decimal?)S_row["運転時間"];
                        this.高速時間 = S_row.IsNull("高速時間") ? (decimal?)null : (decimal?)S_row["高速時間"];
                        this.作業時間 = S_row.IsNull("作業時間") ? (decimal?)null : (decimal?)S_row["作業時間"];
                        this.待機時間 = S_row.IsNull("待機時間") ? (decimal?)null : (decimal?)S_row["待機時間"];
                        this.休憩時間 = S_row.IsNull("休憩時間") ? (decimal?)null : (decimal?)S_row["休憩時間"];
                        this.残業時間 = S_row.IsNull("残業時間") ? (decimal?)null : (decimal?)S_row["残業時間"];
                        this.深夜時間 = S_row.IsNull("深夜時間") ? (decimal?)null : (decimal?)S_row["深夜時間"];
                        this.輸送屯数 = S_row.IsNull("輸送屯数") ? (decimal?)null : (decimal?)S_row["輸送屯数"];
                        this.走行ＫＭ = S_row.IsNull("走行ＫＭ") ? (int?)null : (int?)S_row["走行ＫＭ"];
                        this.実車ＫＭ = S_row.IsNull("実車ＫＭ") ? (int?)null : (int?)S_row["実車ＫＭ"];
                        類似番号入力表示 = System.Windows.Visibility.Hidden;


                        // -------------------
                        IsTranProc = false;
                        ChangeKeyItemChangeable(false);
                        return;
                    }

				}
				else
				{
					this.LineNumber = 1;
				}
				if (DLogData.Rows.Count > 0)
				{
					IsTranProc = true;
					// -------------------

					if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
					{
						this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
					}
					DataRow row = DLogData.Rows[0];
					this.乗務員ID = row.IsNull("乗務員ID") ? (int?)null : (int?)row["乗務員ID"];
					this.出庫時間 = row.IsNull("出庫時間") ? (decimal?)null : (decimal?)row["出庫時間"];
					this.帰庫時間 = row.IsNull("帰庫時間") ? (decimal?)null : (decimal?)row["帰庫時間"];
					this.出勤区分ID = row.IsNull("出勤区分ID") ? (int?)null : (int?)row["出勤区分ID"];
					this.自社部門ID = row.IsNull("自社部門ID") ? (int?)null : (int?)row["自社部門ID"];
					this.車輌ID = row.IsNull("車輌ID") ? (int?)null : (int?)row["車輌ID"];
					this.車輌番号 = row.IsNull("車輌番号") ? (string)null : (string)row["車輌番号"];
					this.車種ID = row.IsNull("車種ID") ? (int?)null : (int?)row["車種ID"];
					this.メーター開始 = row.IsNull("出庫ＫＭ") ? (int?)null : (int?)row["出庫ＫＭ"];
					this.メーター終了 = row.IsNull("帰庫ＫＭ") ? (int?)null : (int?)row["帰庫ＫＭ"];
					this.運行開始日 = row.IsNull("実運行日開始") ? (DateTime?)null : (DateTime?)row["実運行日開始"];
					this.運行終了日 = row.IsNull("実運行日終了") ? (DateTime?)null : (DateTime?)row["実運行日終了"];
					this.勤務日FROM = row.IsNull("勤務開始日") ? (DateTime?)null : (DateTime?)row["勤務開始日"];
					this.勤務日TO = row.IsNull("勤務終了日") ? (DateTime?)null : (DateTime?)row["勤務終了日"];
					this.労務日 = row.IsNull("労務日") ? (DateTime?)null : (DateTime?)row["労務日"];
					this.拘束時間 = row.IsNull("拘束時間") ? (decimal?)null : (decimal?)row["拘束時間"];
					this.運転時間 = row.IsNull("運転時間") ? (decimal?)null : (decimal?)row["運転時間"];
					this.高速時間 = row.IsNull("高速時間") ? (decimal?)null : (decimal?)row["高速時間"];
					this.作業時間 = row.IsNull("作業時間") ? (decimal?)null : (decimal?)row["作業時間"];
					this.待機時間 = row.IsNull("待機時間") ? (decimal?)null : (decimal?)row["待機時間"];
					this.休憩時間 = row.IsNull("休憩時間") ? (decimal?)null : (decimal?)row["休憩時間"];
					this.残業時間 = row.IsNull("残業時間") ? (decimal?)null : (decimal?)row["残業時間"];
					this.深夜時間 = row.IsNull("深夜時間") ? (decimal?)null : (decimal?)row["深夜時間"];
					this.輸送屯数 = row.IsNull("輸送屯数") ? (decimal?)null : (decimal?)row["輸送屯数"];
					this.走行ＫＭ = row.IsNull("走行ＫＭ") ? (int?)null : (int?)row["走行ＫＭ"];
					this.実車ＫＭ = row.IsNull("実車ＫＭ") ? (int?)null : (int?)row["実車ＫＭ"];
					類似番号入力表示 = System.Windows.Visibility.Hidden;

					// -------------------
					IsTranProc = false;
				}
				else
				{
					//

					DLogData.Rows.Add(DLogData.NewRow());
					this.LineNumber = 1;
					cmb出勤区分.SelectedValue = 0;
					類似番号入力表示 = System.Windows.Visibility.Visible;
				}
				ChangeKeyItemChangeable(false);
				SetFocusToTopControl();
				ClearUriageItems();

				break;
			case GetCARDATA:
				CatchupCarData(tbl);
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

			case GetKeihiName:
				CatchupKeihiName(tbl);
				break;
			case GetShrName:
				CatchupSiharaisakiName(tbl);
				break;
			case GetTekiyoName:
				CatchupTekiyoName(tbl);
				break;
			case GetKeiyuZeiritu:
				CatchupKeiyuZeiritu(tbl);
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
							CInputTrn.売上単価 = tanka;
						}
						else
						{
							CInputTrn.売上単価 = 0;
						}
						if (kingaku >= 0)
						{
							CInputTrn.売上金額 = (int)kingaku;
						}
						else
						{
							CInputTrn.売上金額 = 0;
						}
					}
					else
					{
						if (tanka >= 0)
						{
							CInputTrn.支払単価 = tanka;
						}
						else
						{
							CInputTrn.支払単価 = 0;
						}
						if (kingaku >= 0)
						{
							CInputTrn.支払金額 = (int)kingaku;
						}
						else
						{
							CInputTrn.支払金額 = 0;
						}
					}
				}
				break;

            case GetMaxMeisaiNo:
                登録件数 = (int)data;
                break;

			case PutAllData:
				IsUpdated = true;
				MessageBox.Show(string.Format("明細番号 {0} で登録しました。", (int)data));
				ScreenClear();
				break;
			case DeleteAllData:
				IsUpdated = true;
				ScreenClear();
				break;

			case TRN_RIREKI:
				//RIREKI.ItemsSource = null;
				if (data == null)
				{
					spRireki.Rows.Clear();
					return;
				}
				売上明細データ  = (List<DLY01010_RIREKI>)AppCommon.ConvertFromDataTable(typeof(List<DLY01010_RIREKI>), tbl);
				spRireki.ItemsSource = 売上明細データ;

				//var a = (List<DLY01010_RIREKI>)AppCommon.ConvertFromDataTable(typeof(List<DLY01010_RIREKI>), tbl);
				//売上明細データ = a;
				//RIREKI.ItemsSource = a;
				break;
			}

		}

		private void Sum実車KM()
		{
			int km = 0;
			foreach (DataRow row in DUriageData.Rows)
			{
				km += row.IsNull("実車ＫＭ") ? 0 : (int)row["実車ＫＭ"];
			}
			if (km > 0)
			{
				Hi_JissyaKm.Text = km.ToString();
			}
		}

		private void ScreenClear()
		{
			this.IsRfferMode = false;
			this.RefferNumber = string.Empty;

			this.MaintenanceMode = null;
			this.DLogData = null;
			this.DKeihiData = null;
			this.運転日報データ = null;
			//this.spKeihiGrid.ItemsSource = null;
			this.spUriage.ItemsSource = null;
			this.乗務員ID = null;
			this.出庫時間 = null;
			this.帰庫時間 = null;
			this.出勤区分ID = null;
			this.自社部門ID = null;
			this.車輌ID = null;
			this.車輌番号 = null;
			this.車種ID = null;
			this.メーター開始 = null;
			this.メーター終了 = null;
			this.運行開始日 = null;
			this.運行終了日 = null;
			this.勤務日FROM = null;
			this.勤務日TO = null;
			this.労務日 = null;
			this.拘束時間 = null;
			this.運転時間 = null;
			this.高速時間 = null;
			this.作業時間 = null;
			this.待機時間 = null;
			this.休憩時間 = null;
			this.残業時間 = null;
			this.深夜時間 = null;
			this.走行ＫＭ = null;
			this.実車ＫＭ = null;
			this.輸送屯数 = null;
			this.LineNumber = null;

			this.類似番号入力表示 = System.Windows.Visibility.Hidden;
			this.DetailsNumber = null;

			ClearUriageItems();
			ChangeKeyItemChangeable(true);
			ResetAllValidation();

			SpreadResetSelection(this.spUriage);

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
				if (spgrid != null)
				{
					int actrow = spgrid.ActiveRowIndex;
					if (spgrid.ActiveColumn.Name == "S_経費項目ID")
					{
						Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
						dmy.LinkItem = "2";
						SCH09010 srch = new SCH09010();
						srch.TwinTextBox = dmy;
						
						if (srch.ShowDialog(this) == true)
						{
							spgrid.Cells[actrow, "S_経費項目ID"].Text = dmy.Text1;
							int kid = Convert.ToInt32(dmy.Text1);
							base.SendRequest(new CommunicationObject(MessageType.RequestData, GetKeihiName, kid, spgrid.ActiveRow.Index));
						}
						
					}
					else if (spgrid.ActiveColumn.Name == "S_支払先ID")
					{
						Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
						dmy.LinkItem = "3";
						SCH01010 srch = new SCH01010();
						srch.TwinTextBox = dmy;

						if (srch.ShowDialog(this) == true)
						{
							spgrid.Cells[actrow, "S_支払先ID"].Text = dmy.Text1;
							int sid = AppCommon.IntParse(dmy.Text1);
							base.SendRequest(new CommunicationObject(MessageType.RequestData, GetShrName, sid, spgrid.ActiveRow.Index));
						}

					}
					else if (spgrid.ActiveColumn.Name == "S_摘要ID")
					{
						Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
						SCH08010 srch = new SCH08010();
						srch.TwinTextBox = dmy;

						if (srch.ShowDialog(this) == true)
						{
							spgrid.Cells[actrow, "S_摘要ID"].Text = dmy.Text1;
							int sid = AppCommon.IntParse(dmy.Text1);
							base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTekiyoName, sid, spgrid.ActiveRow.Index));
						}

					}
				}
				else
				{
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
						AppCommon.CallMasterMainte("MST09010", this);

					}
					else if (spgrid.ActiveColumn.Name == "S_支払先ID")
					{
						AppCommon.CallMasterMainte("MST01010", this);

					}
					else if (spgrid.ActiveColumn.Name == "S_摘要ID")
					{
						AppCommon.CallMasterMainte("MST08010", this);

					}
				}
				else
				{
					AppCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
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

			spKeihiGrid.Rows.AddNew();


			//DataRow datarow = DKeihiData.NewRow();
			//int gyo = spKeihiGrid.RowCount;
			//gyo += 1;
			//datarow["明細行"] = gyo;
			//this.DKeihiData.Rows.Add(datarow);
			////this.DKeihiData.Rows.Add(this.DKeihiData.NewRow());
		}

		/// <summary>
		/// 売上履歴
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF4Key(object sender, KeyEventArgs e)
		{
			if (Grid_RIREKI.Visibility == Visibility.Collapsed)
			{
				得意先ID指定 = CInputTrn.得意先ID;
				Grid_RIREKI.Visibility = Visibility.Visible;
				CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
				base.SendRequest(com);
				Jyunjyo.Focus();
			}
			else
			{
				Grid_RIREKI.Visibility = Visibility.Collapsed;
			}
		}

        /// <summary>
        /// 行登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            DataAddButton_Click(null, null);
        }

		public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (MaintenanceMode == null)
            {
                return;
            }

            txt運行終了日.Focus();
            txt運行開始日.Focus();


			//if (運行開始日 > 運行終了日)
			//{
			//	MessageBox.Show("実運行日付を確認して下さい。");
			//	return;
			//}

			//if (勤務日FROM > 勤務日TO)
			//{
			//	MessageBox.Show("勤務日を確認して下さい。");
			//	return;
			//}

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
                var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.No)
                {
                    return;
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
			if (e.Key == Key.Enter)
			{
				//e.Handled = true;
				GetMeisaiData();
			}
		}

		private void He_Reffer_PreviewKeyDown(object sender, KeyEventArgs e)
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
					CommunicationObject[] com ={
										   new CommunicationObject(MessageType.RequestData, GetNippou, pno, null),
									   };
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
				CommunicationObject[] com ={
										   new CommunicationObject(MessageType.RequestData, GetNippou, pno, null),
									   };
				base.SendRequest(com);
			}
		}

		private void DataAddButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
			{
				return;
			}
			base.CheckAllValidation();
			if (string.IsNullOrWhiteSpace(this.Se_TokuiSakiNm.Text1) == true)
			{
				this.ErrorMessage = "得意先が指定されていません。";
				return;
			}
			if (内訳Enabled == true && (CInputTrn.請求内訳ID == 0 || CInputTrn.請求内訳ID == null))
			{
				this.ErrorMessage = "請求内訳が指定されていません。";
				return;
			}
			if (base.CheckAllValidation() != true)
			{
				return;
			}

			try
			{
				int gyo = 0;
				DataRow modRow = null;
				foreach (DataRow row in this.DUriageData.Rows)
				{
					if (row.RowState == DataRowState.Deleted)
						continue;

					gyo = (int)row["明細行"];
					if (gyo == this.LineNumber)
					{
						// SPREAD内ROWの更新
						modRow = row;
						break;
					}
				}
				if (modRow == null)
				{
					// SPREAD内ROWに新規追加
					modRow = this.DUriageData.NewRow();
					this.DUriageData.Rows.Add(modRow);
				}

				// 売上関連項目の入力内容バインド変数からSPREAD用のデータにコピーする
				GetValues(this.CInputTrn, modRow);

				modRow["明細番号"] = this.DetailsNumber;
				modRow["明細行"] = this.LineNumber;
				modRow["実運行日開始"] = this.運行開始日 == null ? DateTime.Today : this.運行開始日;
				modRow["実運行日終了"] = this.運行終了日 == null ? (this.運行開始日 == null ? DateTime.Today : this.運行開始日) : this.運行終了日;
				modRow["勤務開始日"] = this.勤務日FROM == null ? (this.運行開始日 == null ? DateTime.Today : this.運行開始日) : this.勤務日FROM;
				modRow["勤務終了日"] = this.勤務日TO == null ? (this.運行開始日 == null ? DateTime.Today : this.運行開始日) : this.勤務日TO;
				modRow["労務日"] = this.労務日 == null ? (this.運行開始日 == null ? DateTime.Today : this.運行開始日) : this.労務日;
				modRow.AcceptChanges();

				gyo = (int)this.DUriageData.Rows[this.DUriageData.Rows.Count - 1]["明細行"];
				this.LineNumber = gyo + 1;

				// 売上関連項目のクリア
				ClearUriageItems();
				Sum実車KM();

                SiharaiKanrenExpander.IsExpanded = false;

				this.Se_SeikyuHiduke.Focus();
			}
			catch (Exception ex)
			{
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


		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			ClearUriageItems();
			int gyo = 0;
			foreach (DataRow row in this.DUriageData.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
					continue;

				int rgyo = (int)row["明細行"];
				if (rgyo > gyo)
				{
					gyo = rgyo;
				}
			}
			this.LineNumber = gyo + 1;

			this.Se_SeikyuHiduke.Focus();
		}

		public void ClearUriageItems()
		{

			// 請求項目
			Se_SeikyuHiduke.Text = string.Empty;
			Se_HaitatuJikan.Text = string.Empty;
			Se_TantouBumon.Text1 = string.Empty;
			Se_TantouBumon.Text2 = string.Empty;
			Se_TokuiSakiNm.Text1 = string.Empty;
			Se_TokuiSakiNm.Text2 = string.Empty;
			Se_SyouhinNm.Text1 = string.Empty;
			Se_SeikyuuUtiwakeNm.Text1 = string.Empty;
			Se_SeikyuuUtiwakeNm.Text2 = string.Empty;
			Se_SeikyuuTekiyou.Text1 = string.Empty;
			Se_SeikyuuTekiyou.Text2 = string.Empty;
			Se_HattiNm.Text1 = string.Empty;
			Se_HattiNm.Text2 = string.Empty;
			Se_Bikou.Text1 = string.Empty;
			Se_Bikou.Text2 = string.Empty;
			Se_CyakuNm.Text1 = string.Empty;
			Se_CyakuNm.Text2 = string.Empty;
			Se_Suuryou.Text = string.Empty;
			Se_Tani.Text = string.Empty;
			Se_Jyuuryou.Text = string.Empty;
			Se_SoukouKm.Text = string.Empty;
			Se_JissyaKm.Text = string.Empty;
			Se_UriageTanka.Text = string.Empty;
			Se_UriageKingaku.Text = string.Empty;
			Se_TuukouRyou.Text = string.Empty;
			Se_Warimasi1.Text = string.Empty;
			Se_Warimasi2.Text = string.Empty;
			//Se_TaikiJikan.Text = string.Empty;
			this.cmb計算区分.SelectedIndex = 0;
			this.cmb社内区分.SelectedIndex = 0;
            this.cmb確認名称.SelectedIndex = 0;
			this.cmb税区分.SelectedIndex = 0;
			this.cmb未定区分.SelectedIndex = 0;

			// 支払項目
			Si_SiharaiHiduke.Text = string.Empty;
			Si_SiharaiSaki.Text1 = string.Empty;
			Si_SiharaisakiNm2.Text = string.Empty;
			Si_SiharaiSakiJyoumuin.Text = string.Empty;
			Si_JyoumuinRenrakusaki.Text = string.Empty;
			Si_SiharaiTanka.Text = string.Empty;
			Si_SiharaiKingaku.Text = string.Empty;
			Si_SiharaiTuukouRyou.Text = string.Empty;
			this.cmb支払計算区分.SelectedIndex = 0;
			this.cmb支払税区分.SelectedIndex = 0;
			this.cmb支払未定区分.SelectedIndex = 0;

			this.CInputTrn = new TRN();
			SpreadResetSelection(this.spUriage);
			this.ResetAllValidation();

		}

		void Update()
		{
			if (CheckDataModified() != true)
			{
			}

            txt運行開始日.CheckValidation();
            txt運行終了日.CheckValidation();
            txt勤務日FROM.CheckValidation();
            txt勤務日TO.CheckValidation();
            txt労務日.CheckValidation();

            DateTime d日付;
            if(!DateTime.TryParse(txt運行開始日.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }
            if (!DateTime.TryParse(txt運行終了日.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }
            if (!DateTime.TryParse(txt勤務日FROM.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }
            if (!DateTime.TryParse(txt勤務日TO.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }
            if (!DateTime.TryParse(txt労務日.Text, out d日付))
            {
                MessageBox.Show(string.Format("入力内容に誤りがあります。"));
                return;
            }


			foreach (DataRow row in this.DUriageData.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
				{
					continue;
				}
				row["乗務員ID"] = this.乗務員ID == null ? DBNull.Value : (object)this.乗務員ID;
				row["出庫時間"] = this.出庫時間 == null ? DBNull.Value : (object)this.出庫時間;
				row["帰庫時間"] = this.帰庫時間 == null ? DBNull.Value : (object)this.帰庫時間;
				row["車輌ID"] = this.車輌ID == null ? DBNull.Value : (object)this.車輌ID;
				row["車輌番号"] = this.車輌番号 == null ? DBNull.Value : (object)this.車輌番号;
				row["車種ID"] = this.車種ID == null ? DBNull.Value : (object)this.車種ID;
				row["出庫ＫＭ"] = this.メーター開始 == null ? DBNull.Value : (object)this.メーター開始;
				row["帰庫ＫＭ"] = this.メーター終了 == null ? DBNull.Value : (object)this.メーター終了;
				row["実運行日開始"] = this.運行開始日 == null ? DBNull.Value : (object)this.運行開始日;
				row["実運行日終了"] = this.運行終了日 == null ? DBNull.Value : (object)this.運行終了日;
				row["勤務開始日"] = this.勤務日FROM == null ? DBNull.Value : (object)this.勤務日FROM;
				row["勤務終了日"] = this.勤務日TO == null ? DBNull.Value : (object)this.勤務日TO;
				row["労務日"] = this.労務日 == null ? DBNull.Value : (object)this.労務日;
				row["拘束時間"] = this.拘束時間 == null ? DBNull.Value : (object)this.拘束時間;
				row["運転時間"] = this.運転時間 == null ? DBNull.Value : (object)this.運転時間;
				row["高速時間"] = this.高速時間 == null ? DBNull.Value : (object)this.高速時間;
				row["作業時間"] = this.作業時間 == null ? DBNull.Value : (object)this.作業時間;
				row["待機時間"] = this.待機時間 == null ? DBNull.Value : (object)this.待機時間;
				row["休憩時間"] = this.休憩時間 == null ? DBNull.Value : (object)this.休憩時間;
				row["残業時間"] = this.残業時間 == null ? DBNull.Value : (object)this.残業時間;
				row["深夜時間"] = this.深夜時間 == null ? DBNull.Value : (object)this.深夜時間;

				row["輸送屯数"] = this.輸送屯数 == null ? DBNull.Value : (object)this.輸送屯数;
				row.AcceptChanges();
			}
			foreach (DataRow row in this.DLogData.Rows)
			{
				if (row.RowState == DataRowState.Deleted)
				{
					continue;
				}
				row["乗務員ID"] = this.乗務員ID == null ? DBNull.Value : (object)this.乗務員ID;
				row["出庫時間"] = this.出庫時間 == null ? DBNull.Value : (object)this.出庫時間;
				row["帰庫時間"] = this.帰庫時間 == null ? DBNull.Value : (object)this.帰庫時間;
				row["出勤区分ID"] = this.出勤区分ID == null ? DBNull.Value : (object)this.出勤区分ID;
				row["自社部門ID"] = this.自社部門ID == null ? DBNull.Value : (object)this.自社部門ID;
				row["車輌ID"] = this.車輌ID == null ? DBNull.Value : (object)this.車輌ID;
				row["車輌番号"] = this.車輌番号 == null ? DBNull.Value : (object)this.車輌番号;
				row["車種ID"] = this.車種ID == null ? DBNull.Value : (object)this.車種ID;
				row["出庫ＫＭ"] = this.メーター開始 == null ? DBNull.Value : (object)this.メーター開始;
				row["帰庫ＫＭ"] = this.メーター終了 == null ? DBNull.Value : (object)this.メーター終了;
				row["実運行日開始"] = this.運行開始日 == null ? DBNull.Value : (object)this.運行開始日;
				row["実運行日終了"] = this.運行終了日 == null ? DBNull.Value : (object)this.運行終了日;
				row["勤務開始日"] = this.勤務日FROM == null ? DBNull.Value : (object)this.勤務日FROM;
				row["勤務終了日"] = this.勤務日TO == null ? DBNull.Value : (object)this.勤務日TO;
				row["労務日"] = this.労務日 == null ? DBNull.Value : (object)this.労務日;
				row["拘束時間"] = this.拘束時間 == null ? DBNull.Value : (object)this.拘束時間;
				row["運転時間"] = this.運転時間 == null ? DBNull.Value : (object)this.運転時間;
				row["高速時間"] = this.高速時間 == null ? DBNull.Value : (object)this.高速時間;
				row["作業時間"] = this.作業時間 == null ? DBNull.Value : (object)this.作業時間;
				row["待機時間"] = this.待機時間 == null ? DBNull.Value : (object)this.待機時間;
				row["休憩時間"] = this.休憩時間 == null ? DBNull.Value : (object)this.休憩時間;
				row["残業時間"] = this.残業時間 == null ? DBNull.Value : (object)this.残業時間;
				row["深夜時間"] = this.深夜時間 == null ? DBNull.Value : (object)this.深夜時間;
				row["走行ＫＭ"] = this.走行ＫＭ == null ? DBNull.Value : (object)this.走行ＫＭ;
				row["実車ＫＭ"] = this.実車ＫＭ == null ? DBNull.Value : (object)this.実車ＫＭ;
				row["輸送屯数"] = this.輸送屯数 == null ? DBNull.Value : (object)this.輸送屯数;
				row.AcceptChanges();
			}

			var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
			if (yesno == MessageBoxResult.No)
			{
				return;
			}

			int pno = AppCommon.IntParse(this.DetailsNumber);
			if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
			{
				pno = -1;
                担当者ID = ccfg.ユーザID;
			}
            int gyo = 0;
			int intid;
			decimal decid;
			foreach (T03_KTRN_Member row in DKeihiData)
            { 
                gyo += 1;
                row.明細行 = gyo;
                row.自社部門ID = 自社部門ID;
                row.車輌ID = 車輌ID;
                row.車輌番号 = this.車輌番号;
                //row.収支区分 = 0;
                row.乗務員ID = 乗務員ID;
                row.入力者ID = ccfg.ユーザID;
				int.TryParse(row.S_支払先ID, out intid);
				row.支払先ID = intid;
				decimal.TryParse(row.S_数量, out decid);
				row.数量 = decid;
				int.TryParse(row.S_経費項目ID, out intid);
				row.経費項目ID = intid;
				int.TryParse(row.S_金額, out intid);
				row.金額 = intid;
				decimal.TryParse(row.S_単価, out decid);
				row.単価 = decid;
				int.TryParse(row.S_摘要ID, out intid);
				row.摘要ID = intid;
            }
            DataTable 経費データ  = new DataTable();
            //リストをデータテーブルへ
            AppCommon.ConvertToDataTable(DKeihiData, 経費データ);

            CommunicationObject com = new CommunicationObject(MessageType.RequestData, PutAllData, pno, this.DUriageData, this.DLogData, 経費データ, 担当者ID);
			base.SendRequest(com);
		}

		private bool CheckDataModified()
		{
			// SPREADの入力中のフィールドをチェックし、入力を確定させる
			foreach (var spread in new GcSpreadGrid[] { this.spKeihiGrid })
			{
				AppCommon.FixSpreadActiveCell(spread);
			}

			return false;
		}

		void Delete()
		{
			int pno = AppCommon.IntParse(this.DetailsNumber);
			CommunicationObject com = new CommunicationObject(MessageType.RequestData, DeleteAllData, pno);
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
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0));
		}

		private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
		{
			datagetmode = DataGetMode.previous;
			if (string.IsNullOrWhiteSpace(this.DetailsNumber))
			{
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0));
			}
			else
			{
				int no;
				if (int.TryParse(this.DetailsNumber, out no))
				{
					base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, no, 1));
				}
			}
		}

		private void NextIdButton_Click(object sender, RoutedEventArgs e)
		{
			datagetmode = DataGetMode.next;
			if (string.IsNullOrWhiteSpace(this.DetailsNumber))
			{
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0));
			}
			else
			{
				int no;
				if (int.TryParse(this.DetailsNumber, out no))
				{
					base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, no, 2));
				}
			}
		}

		private void LastIdButoon_Click(object sender, RoutedEventArgs e)
		{
			datagetmode = DataGetMode.last;
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 3));
		}

		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
			frmcfg.Top = this.Top;
			frmcfg.Left = this.Left;
			frmcfg.Height = this.Height;
			frmcfg.Width = this.Width;
			this.DLogData = null;
			this.DKeihiData = null;
			frmcfg.spUriageConfig = AppCommon.SaveSpConfig(this.spUriage);
			frmcfg.spKeihiConfig = AppCommon.SaveSpConfig(this.spKeihiGrid);
			frmcfg.spRirekiConfig20160419 = AppCommon.SaveSpConfig(this.spRireki);
			frmcfg.表示順指定 = 表示順;
			frmcfg.担当者指定 = 担当者ID指定;
			frmcfg.得意先指定 = 得意先ID指定;


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
			AppCommon.LoadSpConfig(this.spUriage, this.spUriageConfig);
			AppCommon.LoadSpConfig(this.spKeihiGrid, this.spKeihiConfig);
			ScreenClear();
		}

		private void 開始日_LostFocus(object sender, RoutedEventArgs e)
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
			}
            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
            {
                return;
            }

            if (string.IsNullOrEmpty(txt勤務日FROM.Text))
            {
                txt勤務日FROM.Text = txt運行開始日.Text;
            }
            if (string.IsNullOrEmpty(txt労務日.Text))
            {
                txt労務日.Text = txt運行開始日.Text;
            }

			拘束時間計算();
		}

		private void 終了日_LostFocus(object sender, RoutedEventArgs e)
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
			}

			if (ctl.IsModified != true)
			{
				if (運行開始日 > 運行終了日)
				{
					MessageBox.Show("実運行日付を確認して下さい。");
				}
				return;
			}

			txt勤務日TO.Text = txt運行終了日.Text;

			if (運行開始日 > 運行終了日)
			{
				MessageBox.Show("実運行日付を確認して下さい。");
			}

			拘束時間計算();
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
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
				ctl.Text = DateTime.Today.ToString(ctl.Mask);
			}

			if (勤務日FROM > 勤務日TO)
			{
				MessageBox.Show("勤務日を確認して下さい。");
			}

		}


		private void 日付_GotFocus(object sender, RoutedEventArgs e)
		{
			shiharai_focus_kbn = false;
			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
				return;

			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(ctl.Text) == true && 運行開始日 != null)
			{
				ctl.Text = string.Format("{0:yyyy/MM/dd}", 運行開始日);
			}
		}

		/// <summary>
		/// 車両ID入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        //private void CARID_TextChanged(object sender, RoutedEventArgs e)
        //{
        //    // TRNデータ取得直後の表示のときは処理しない
        //    if (IsTranProc)
        //        return;

        //    var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
        //    if (ctl == null)
        //    {
        //        return;
        //    }
        //    int cid;
        //    if (int.TryParse(ctl.Text1, out cid) != true)
        //    {
        //        return;
        //    }
        //    this.メーター開始 = null;
        //    SendRequest(new CommunicationObject(MessageType.RequestData, GetCARDATA, cid));
        //}

        private void CARID_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                // TRNデータ取得直後の表示のときは処理しない
                if (IsTranProc)
                    return;

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
                //this.メーター開始 = null;
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
			//if (this.メーター開始 == null || this.メーター開始 == 0)
			//{
			//    this.メーター開始 = (int)tbl.Rows[0]["現在メーター"];
			//}
			//if (string.IsNullOrWhiteSpace(txt乗務員.Text1))
			//{
			//    this.乗務員ID = (int?)tbl.Rows[0]["乗務員ID"];
			//}
			//if (string.IsNullOrWhiteSpace(He_SyashuNm.Text1))
			//{
			//    this.車種ID = (int?)tbl.Rows[0]["車種ID"];請求内訳管理区分
			//}

			if (tbl.Rows[0]["乗務員ID"].ToString() != "0" && (this.乗務員ID == null || this.乗務員ID == 0))
			{
				this.乗務員ID = tbl.Rows[0].IsNull("乗務員ID") ? null : (int?)tbl.Rows[0]["乗務員ID"];
			}
			if (tbl.Rows[0]["車種ID"].ToString() != "0" && (this.車種ID == null || this.車種ID == 0))
			{
				this.車種ID = tbl.Rows[0].IsNull("車種ID") ? null : (int?)tbl.Rows[0]["車種ID"];
			}
			if ((this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ) || (メーター開始 == null || メーター開始 == 0))
			{
				this.メーター開始 = null;
				this.メーター開始 = (int)tbl.Rows[0]["現在メーター"];
			}
		}


		void CatchupHinData(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
			this.Se_Tani.Text = this.単位 = tbl.Rows[0].IsNull("単位") ? "" : (string)tbl.Rows[0]["単位"];
		}

		private void 得意先ID_Text1Changed(object sender, RoutedEventArgs e)
		{
			if (IsGridWorking)
				return;

			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
			if (ctl == null)
			{
				return;
			}
			this.内訳検索用得意先ID = ctl.Text1;
			this.CInputTrn.請求内訳ID = null;
			this.CInputTrn.請求内訳管理区分 = null;
			this.CInputTrn.請求内訳名 = string.Empty;
			int cid;
			if (int.TryParse(ctl.Text1, out cid) != true)
			{
				return;
			}
			SendRequest(new CommunicationObject(MessageType.RequestData, GetTOKDATA, cid, 0));
		}

		private void 支払先ID_LostFocus(object sender, RoutedEventArgs e)
		{
			if (IsGridWorking)
				return;

			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
			if (ctl == null)
			{
				return;
			}
			if (ctl.IsModified1 != true)
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

		void CatchupTokData(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
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

        void CatchupKeihiName(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
			int row = (int)tbl.Rows[0]["row"];
			string name = tbl.Rows[0].IsNull("経費項目名") ? string.Empty : (string)tbl.Rows[0]["経費項目名"];
			spKeihiGrid.Cells[row, "経費項目名"].Text = name;

			if ((int)tbl.Rows[0]["経費項目ID"] == 0)
			{
				MessageBox.Show("経費項目マスタにデータがありません。");
			}
		}

		void CatchupSiharaisakiName(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
			int row = (int)tbl.Rows[0]["row"];
			string name = tbl.Rows[0].IsNull("支払先名") ? string.Empty : (string)tbl.Rows[0]["支払先名"];
			spKeihiGrid.Cells[row, "支払先名"].Text = name;
			decimal rate = (decimal)tbl.Rows[0]["燃料単価"];
			spKeihiGrid.Cells[row, "S_単価"].Value = rate;

			軽油税計算(row);
			経費金額計算(row);

			if ((int)tbl.Rows[0]["支払先ID"] == 0)
			{
				MessageBox.Show("取引先マスタにデータがありません。");
			}
		}

		void CatchupTekiyoName(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
			int row = (int)tbl.Rows[0]["row"];
			string name = tbl.Rows[0].IsNull("摘要名") ? string.Empty : (string)tbl.Rows[0]["摘要名"];
			spKeihiGrid.Cells[row, "摘要名"].Text = name;

			if ((int)tbl.Rows[0]["摘要ID"] == 0)
			{
				MessageBox.Show("摘要マスタにデータがありません。");
			}
		}

		void CatchupKeiyuZeiritu(DataTable tbl)
		{
			if (tbl == null || tbl.Rows.Count == 0)
			{
				return;
			}
			int row = (int)tbl.Rows[0]["row"];
			decimal rate = tbl.Rows[0].IsNull("軽油取引税率") ? 0 : (decimal)tbl.Rows[0]["軽油取引税率"];
			spKeihiGrid.Cells[row, "軽油取引税率"].Value = rate;

			軽油税計算(row);
			経費金額計算(row);
		}

		private void 軽油税計算(int row)
		{
			int kid = 0;
			int.TryParse(spKeihiGrid.Cells[row, "S_経費項目ID"].Text, out kid);
			if (kid == AppConst.KEIHI_CODE_KEIYUZEI)
			{
				decimal rate = 0;
				decimal suu = 0;
				decimal.TryParse(spKeihiGrid.Cells[row, "軽油取引税率"].Text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out rate);
				decimal.TryParse(spKeihiGrid.Cells[row, "S_数量"].Text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out suu);
				decimal zei = Math.Round((suu * rate), 2);
				spKeihiGrid.Cells[row, "内軽油税分"].Value = zei;
			}
			else
			{
				spKeihiGrid.Cells[row, "内軽油税分"].Value = 0;
			}
		}

		private void spread_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
			var grid = sender as GcSpreadGrid;
			if (grid == null) return;
			if (grid.RowCount == 0) return;
			if (e.EditAction != SpreadEditAction.Commit) return;

			if (e.CellPosition.ColumnName.Contains("年月日") == true)
			{
				AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);
				spKeihiGrid.Cells[e.CellPosition.Row, "軽油取引税率"].Value = 0;
				spKeihiGrid.Cells[e.CellPosition.Row, "内軽油税分"].Text = string.Empty;
				int kid = 0;
				int.TryParse(spKeihiGrid.Cells[e.CellPosition.Row, "S_経費項目ID"].Text, out kid);
				if (kid == AppConst.KEIHI_CODE_KEIYUZEI)
				{
					var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
					DateTime date;
					if (DateTime.TryParse(text, out date) == true)
					{
						int? sID = null;
						int wk;
						if (int.TryParse(spKeihiGrid.Cells[e.CellPosition.Row, "S_支払先ID"].Text, out wk))
						{
							sID = wk;
						}
						base.SendRequest(new CommunicationObject(MessageType.RequestData, GetKeiyuZeiritu, date, e.CellPosition.Row, sID));
					}
				}

			}
			else if (e.CellPosition.ColumnName == "S_経費項目ID")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) return;

				int kid = AppCommon.IntParse(text);
				grid.Cells[e.CellPosition.Row, "経費項目ID"].Value = kid;
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetKeihiName, kid, e.CellPosition.Row));
			}
			else if (e.CellPosition.ColumnName == "S_支払先ID")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) return;

				int sid = AppCommon.IntParse(text);
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetShrName, sid, e.CellPosition.Row));
			}
			else if (e.CellPosition.ColumnName == "S_摘要ID")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) return;

				int sid = AppCommon.IntParse(text);
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTekiyoName, sid, e.CellPosition.Row));
			}
			else if (e.CellPosition.ColumnName == "S_金額")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) return;

				decimal wk = 0;
				decimal.TryParse(text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out wk);
				grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = wk.ToString("N0");
			}
			else if (e.CellPosition.ColumnName == "S_数量")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) { spKeihiGrid.Cells[e.CellPosition.Row, "S_金額"].Text = string.Empty; return; }

				decimal wk = 0;
				decimal.TryParse(text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out wk);
				grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = wk.ToString("N01");

				軽油税計算(e.CellPosition.Row);
				経費金額計算(e.CellPosition.Row);
			}
			else if (e.CellPosition.ColumnName == "S_単価")
			{
				var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
				if (string.IsNullOrWhiteSpace(text) == true) { spKeihiGrid.Cells[e.CellPosition.Row, "S_金額"].Text = string.Empty; return; }

				decimal wk = 0;
				decimal.TryParse(text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out wk);
				grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = wk.ToString("N02");

				経費金額計算(e.CellPosition.Row);
			}
		}

		private void 経費金額計算(int rowno)
		{
			var s_tanka = spKeihiGrid.Cells[rowno, "S_単価"].Text;
			var s_suryo = spKeihiGrid.Cells[rowno, "S_数量"].Text;
			decimal wk = 0;
			decimal tanka = decimal.TryParse(s_tanka, out wk) ? wk : 0;
			decimal suryo = decimal.TryParse(s_suryo, out wk) ? wk : 0;
			wk = tanka * suryo;
			spKeihiGrid.Cells[rowno, "S_金額"].Text = wk.ToString("N0");
		}

		private void spKeihiGrid_CellEnter(object sender, SpreadCellEnterEventArgs e)
		{
			var grid = sender as GcSpreadGrid;
			if (grid == null) return;
			if (grid.RowCount == 0) return;
			this._originalText = grid.Cells[e.Row, e.Column].Text;

			if (this.spKeihiGrid.Cells.Columns[e.Column].Header == "経費発生年月日")
			{
				if (string.IsNullOrWhiteSpace(this.spKeihiGrid.Cells[e.Row, e.Column].Text) == true)
				{
					try
					{
						if (this.運行開始日 != null)
						{
							this.spKeihiGrid.Cells[e.Row, e.Column].Text = ((DateTime)this.運行開始日).ToString("yyyy/MM/dd");
							var text = this.spKeihiGrid.Cells[e.Row, e.Column].Text;
							DateTime date;
							if (DateTime.TryParse(text, out date) == true)
							{
								int? sID = null;
								int wk;
								if (int.TryParse(spKeihiGrid.Cells[e.Row, "S_支払先ID"].Text, out wk))
								{
									sID = wk;
								}
								base.SendRequest(new CommunicationObject(MessageType.RequestData, GetKeiyuZeiritu, date, e.Row, sID));
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		private void Se_TaikiJikan_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter)
			{
				return;
			}
            //if (SiharaiKanrenExpander.IsExpanded)
            //{
            //    return;
            //}

            if (string.IsNullOrEmpty(乗務員ID.ToString()) || 乗務員ID == 0)
            {
                e.Handled = true;
                SiharaiKanrenExpander.IsExpanded = true;
                DoEvents();
                Keyboard.Focus(this.Si_SiharaiHiduke);
                DoEvents();
                this.Si_SiharaiHiduke.Focus();
                return;
            }
			
                var ctl = sender as Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
			e.Handled = true;
			bool chk = ctl.CheckValidation();
			if (chk == true)
			{
				Keyboard.Focus(this.AddButton);
			}
			else
			{
				this.ErrorMessage = ctl.GetValidationMessage();
			}
		}

		private void Price計算_請求()
		{
			if (IsGridWorking)
				return;

			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
				return;
			FrameworkControl[] ctls =
			{
				this.Se_TokuiSakiNm,
				this.Se_HattiNm,
				this.Se_CyakuNm,
				this.Se_SyouhinNm,
				this.He_SyashuNm,
				this.Se_SoukouKm,
			};
			foreach (var ctl in ctls)
			{
				if (ctl.CheckValidation() != true)
				{
					return;
				}
			}
			//if (運転日報データ == null)
			//	return;
            decimal d数量 , p数量 = 0;
            decimal d重量 , p重量 = 0;
			int p計算区分 = (int)this.cmb計算区分.SelectedValue;
			int p請求支払区分 = 0;	// 請求
			int p得意先ID = (string.IsNullOrWhiteSpace(this.Se_TokuiSakiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_TokuiSakiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p発地ID = (string.IsNullOrWhiteSpace(this.Se_HattiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_HattiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p着地ID = (string.IsNullOrWhiteSpace(this.Se_CyakuNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_CyakuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p商品ID = (string.IsNullOrWhiteSpace(this.Se_SyouhinNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_SyouhinNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p車種ID = (string.IsNullOrWhiteSpace(this.He_SyashuNm.Text1)) ? 0 : AppCommon.IntParse(this.He_SyashuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p走行ＫＭ = (string.IsNullOrWhiteSpace(this.Se_SoukouKm.Text)) ? 0 : AppCommon.IntParse(this.Se_SoukouKm.Text, System.Globalization.NumberStyles.AllowThousands);
            if (Se_Suuryou.Text != null)
            {
                if (decimal.TryParse(Se_Suuryou.Text, out d数量) == true)
                {
                    p数量 = (string.IsNullOrWhiteSpace(this.Se_Suuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Suuryou.Text);
                }
            }

            if (Se_Jyuuryou.Text != null)
            {
                if (decimal.TryParse(Se_Jyuuryou.Text, out d重量) == true)
                {
					p重量 = (string.IsNullOrWhiteSpace(this.Se_Jyuuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Jyuuryou.Text);
                }
            }

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
					金額 = (int)Math.Round((p数量 * 単価),0,MidpointRounding.AwayFromZero);
					break;
				}
				CInputTrn.売上金額 = 金額;
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
				CInputTrn.売上金額 = 金額;
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
				if (p得意先ID != 0 && p重量 != 0 && p数量 != 0)
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
			if (IsGridWorking)
				return;

			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
				return;

			FrameworkControl[] ctls =
			{
				this.Si_SiharaiSaki,
				this.Se_HattiNm,
				this.Se_CyakuNm,
				this.Se_SyouhinNm,
				this.He_SyashuNm,
				this.Se_SoukouKm,
			};
			foreach (var ctl in ctls)
			{
				if (ctl.CheckValidation() != true)
				{
					return;
				}
			}
			//if (運転日報データ == null)
			//	return;
            decimal d数量, p数量 = 0;
            decimal d重量, p重量 = 0;
			int p計算区分 = (int)this.cmb支払計算区分.SelectedValue;
			int p請求支払区分 = 1;	// 支払
			int p支払先ID = (string.IsNullOrWhiteSpace(this.Si_SiharaiSaki.Text1)) ? 0 : AppCommon.IntParse(this.Si_SiharaiSaki.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p発地ID = (string.IsNullOrWhiteSpace(this.Se_HattiNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_HattiNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p着地ID = (string.IsNullOrWhiteSpace(this.Se_CyakuNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_CyakuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p商品ID = (string.IsNullOrWhiteSpace(this.Se_SyouhinNm.Text1)) ? 0 : AppCommon.IntParse(this.Se_SyouhinNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p車種ID = (string.IsNullOrWhiteSpace(this.He_SyashuNm.Text1)) ? 0 : AppCommon.IntParse(this.He_SyashuNm.Text1, System.Globalization.NumberStyles.AllowThousands);
			int p走行ＫＭ = (string.IsNullOrWhiteSpace(this.Se_SoukouKm.Text)) ? 0 : AppCommon.IntParse(this.Se_SoukouKm.Text, System.Globalization.NumberStyles.AllowThousands);
            if (Se_Suuryou.Text != null)
            {
                if (decimal.TryParse(Se_Suuryou.Text, out d数量) == true)
                {
					p数量 = (string.IsNullOrWhiteSpace(this.Se_Suuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Suuryou.Text);
                }
            }

            if (Se_Jyuuryou.Text != null)
            {
                if (decimal.TryParse(Se_Jyuuryou.Text, out d重量) == true)
                {
					p重量 = (string.IsNullOrWhiteSpace(this.Se_Jyuuryou.Text)) ? 0 : AppCommon.DecimalParse(this.Se_Jyuuryou.Text);
                }
            }
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
				if (p支払先ID != 0 && p重量 != 0 && p走行ＫＭ != 0)
				{
					SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
				}
				break;
			case 4:
                if (p支払先ID != 0 && p商品ID != 0)// && p重量 != 0 && p数量 != 0
				{
					SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
				}
				break;
			case 5:
				if (p支払先ID != 0 && p重量 != 0 && p数量 != 0)
				{
					SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
				}
				break;
			case 6:
				if (p支払先ID != 0 && p数量 != 0)
				{
					SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
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
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
            //if (ctl.IsModified != true)
            //{
            //    return;
            //}
			Price計算_請求();
		}

		private void 支払単価_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
            //if (ctl.IsModified != true)
            //{
            //    return;
            //}
			Price計算_支払();
		}

		private void 発地ID_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
			if (ctl == null)
			{
				return;
			}
			if (ctl.IsModified1 != true)
			{
				return;
			}
			Price計算_請求();
			Price計算_支払();
		}

		private void 着地ID_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
			if (ctl == null)
			{
				return;
			}
			if (ctl.IsModified1 != true)
			{
				return;
			}
			Price計算_請求();
			Price計算_支払();
		}

		private void 数量_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
            //if (ctl.IsModified != true)
            //{
            //    return;
            //}
			Price計算_請求();
			Price計算_支払();
		}

		private void 重量_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
            //if (ctl.IsModified != true)
            //{
            //    return;
            //}
			Price計算_請求();
			Price計算_支払();
		}

		private void 走行ＫＭ_LostFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
            //if (ctl.IsModified != true)
            //{
            //    return;
            //}
			Price計算_請求();
			Price計算_支払();
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
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox;
			if (ctl == null)
			{
				return;
			}
			if (ctl.IsModified1 != true)
			{
				return;
			}
			Price計算_請求();
		}

		private void txt就業時間_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
				return;

			//if (string.IsNullOrWhiteSpace(txt出庫時間.Text) || string.IsNullOrWhiteSpace(txt帰庫時間.Text))
			//{
			//	return;
			//}
			var ctl = sender as Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
			if (ctl.IsModified != true)
			{
				return;
			}
			拘束時間計算();
			休憩時間計算項目_LostFocus(sender,e);
		}

		private void 拘束時間計算()
		{
			if (string.IsNullOrWhiteSpace(this.txt運行開始日.Text) || string.IsNullOrWhiteSpace(this.txt運行終了日.Text)
				|| string.IsNullOrWhiteSpace(this.txt出庫時間.Text) || string.IsNullOrWhiteSpace(this.txt帰庫時間.Text))
			{
				拘束時間 = null;
				return;
			}

			if (this.txt運行開始日.CheckValidation() != true
			|| this.txt運行終了日.CheckValidation() != true
			|| this.txt出庫時間.CheckValidation() != true
			|| this.txt帰庫時間.CheckValidation() != true)
			{
				拘束時間 = null;
				return;
			}

			DateTime date1 = DateTime.Parse(txt運行開始日.Text);
			if (DateTime.TryParse(txt運行開始日.Text, out date1) != true)
			{
				拘束時間 = null;
				return;
			}
			DateTime date2;
			if (DateTime.TryParse(txt運行終了日.Text, out date2) != true)
			{
				拘束時間 = null;
				return;
			}
			出庫時間 = AppCommon.DecimalParse(txt出庫時間.Text);
			帰庫時間 = AppCommon.DecimalParse(txt帰庫時間.Text);
			int h1 = (int)Math.Truncate((decimal)出庫時間);
			int m1 = (int)(((decimal)出庫時間 - h1) * 100);
			date1 = date1.AddHours(h1);
			date1 = date1.AddMinutes(m1);
			int h2 = (int)Math.Truncate((decimal)帰庫時間);
			int m2 = (int)(((decimal)帰庫時間 - h2) * 100);
			date2 = date2.AddHours(h2);
			date2 = date2.AddMinutes(m2);
			TimeSpan ts1 = new TimeSpan(h1, m1, 0);
			TimeSpan ts2 = new TimeSpan(h2, m2, 0);
			TimeSpan ts = ts2 - ts1;
			ts = date2 - date1;
			if (ts.Days < 0 || ts.Hours < 0 || ts.Minutes < 0)
			{
				拘束時間 = null;
				//Hi_KousokuTm.Text = string.Empty;
			}
			else
			{
				拘束時間 = (ts.Days * 24) + ts.Hours + ((decimal)ts.Minutes / 100m);
				//Hi_KousokuTm.Text = string.Format("{0}.{1:D2}", (ts.Days * 24) + ts.Hours, ts.Minutes);
			}
		}

		private void txtMeterFROM_LostFocus(object sender, RoutedEventArgs e)
		{
			if (メーター終了 != null && (メーター開始 == null ? 0 : メーター開始) > (メーター終了 == null ? 0 : メーター終了))
			{
				MessageBox.Show("開始メーターが終了メーターを上回っています。");
			}
		}

		private void txtMeterTO_LostFocus(object sender, RoutedEventArgs e)
		{
			if (メーター終了 != null && (メーター開始 == null ? 0 : メーター開始) > (メーター終了 == null ? 0 : メーター終了))
			{
				MessageBox.Show("開始メーターが終了メーターを上回っています。");
			}
		}

		private void 休憩時間計算項目_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.MaintenanceMode))
				return;

			if (string.IsNullOrWhiteSpace(Hi_KousokuTm.Text))
			{
				return;
			}
			decimal btime = AppCommon.DecimalParse(Hi_KousokuTm.Text);
			decimal utime = 0;
			decimal ktime = 0;
			decimal wtime = 0;
			decimal ptime = 0;
			if (string.IsNullOrWhiteSpace(Hi_UntenIppan.Text) != true)
			{
				utime = AppCommon.DecimalParse(Hi_UntenIppan.Text);
			}
			if (string.IsNullOrWhiteSpace(Hi_UntenKousoku.Text) != true)
			{
				ktime = AppCommon.DecimalParse(Hi_UntenKousoku.Text);
			}
			if (string.IsNullOrWhiteSpace(Hi_SagyouTm.Text) != true)
			{
				wtime = AppCommon.DecimalParse(Hi_SagyouTm.Text);
			}
			if (string.IsNullOrWhiteSpace(Hi_TaikiTm.Text) != true)
			{
				ptime = AppCommon.DecimalParse(Hi_TaikiTm.Text);
			}
			btime = SubtructDecimalTime(btime, utime);
			btime = SubtructDecimalTime(btime, ktime);
			btime = SubtructDecimalTime(btime, wtime);
			btime = SubtructDecimalTime(btime, ptime);

			if (btime >= 0)
			{
				休憩時間 = btime;
				//Hi_KyuukeiTm.Text = btime.ToString("0.00");
			}
			//if (string.IsNullOrWhiteSpace(Hi_KyuukeiTm.Text))
			//{
			//	rtime = AppCommon.IntParse(Hi_KyuukeiTm.Text);
			//}
			else
			{
				休憩時間 = 0;
				MessageBox.Show("拘束時間を超えています。");
			}

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

		private void spUriage_SelectionChanged(object sender, EventArgs e)
		{
			if (this.spUriage.ActiveColumnIndex < 2 || this.spUriage.SelectedItem == null)
			{
				return;
			}
			if (this.spUriage.SelectedRanges[0].RangeType == CellRangeType.All || this.spUriage.SelectedRanges[0].RangeType == CellRangeType.Columns)
			{
				return;
			}
			var row_No = this.spUriage.ActiveRowIndex;
			CopyUriageRow(this.spUriage.ActiveRowIndex, false);
            DoEvents();

            var gyou = (int)this.spUriage.Rows[row_No].Cells[2].Value;
            DataRow cprow = null;
            foreach (DataRow row in this.DUriageData.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                int rgyo = (int)row["明細行"];
                if (rgyo == gyou)
                {
                    cprow = row;
                }
                if (gyou < rgyo)
                {
                    gyou = rgyo;
                }
            }
            _dInputData.ImportRow(cprow);

            //CopyUriageRow(row_No, false);
            DoEvents();
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

		private void txtMeterFROM_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.Tab)
			{

			}
		}

		private void txtMeterTO_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.Tab)
			{
				e.Handled = true;
				((TabItem)tab_nyuryoku.Items[0]).IsSelected = true;
				DoEvents();
				this.Se_SeikyuHiduke.Focus();
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

        private void cmb支払税区分_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            Keyboard.Focus(AddButton);
        }

		private void Ribbon_SizeChanged_1(object sender, SizeChangedEventArgs e)
		{
			this.ErrorMessage = "RIBBON size changed";
		}

		private void RibbonTab_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		private void Ribbon_PreviewMouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

        private void 支払先ID_cText1Changed(object sender, RoutedEventArgs e)
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
        }

        private void txt運行開始日_PreviewKeyDown(object sender, KeyEventArgs e)
        {
			//if (e.Key == Key.Enter )
			//{
			//	拘束時間計算();
			//}

        }

        private void txt運行終了日_PreviewKeyDown(object sender, KeyEventArgs e)
        {
			//if (e.Key == Key.Enter )
			//{
			//	拘束時間計算();
			//}

        }

		private void SiharaiKanrenExpander_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter)
			{
				return;
			}
			if (SiharaiKanrenExpander.IsExpanded)
			{
				var childlist = ViewBaseCommon.FindLogicalChildList<Control>(SiharaiKanrenExpander);
				foreach (var ctl in childlist)
				{
					if (ctl.IsFocused)
					{
						return;
					}
				}
				e.Handled = true;
				Si_SiharaiHiduke.Focus();
			}
			else
			{
				e.Handled = true;
				SiharaiKanrenExpander.IsExpanded = true;
				Si_SiharaiHiduke.Focus();
			}
		}

		private void Hi_SoukouKm_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if ((メーター終了 == null ? 0 : メーター終了) - (メーター開始 == null ? 0 : メーター開始) < 0)
			{
				MessageBox.Show("メーター走行距離を超えています。");
			}
		}

		private void Hi_JissyaKm_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if ((走行ＫＭ == null ? 0 : 走行ＫＭ) < (実車ＫＭ == null ? 0 : 実車ＫＭ))
			{
				MessageBox.Show("走行kmを超えています。");
				return;
			}
			if ((メーター終了 == null ? 0 : メーター終了) - (メーター開始 == null ? 0 : メーター開始) < 0)
			{
				MessageBox.Show("メーター走行距離を超えています。");
			}

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
			if (e.Key == Key.Tab)
			{
				if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) != KeyStates.Down && (Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) != KeyStates.Down)
				{
					e.Handled = true;
				}
			}

			if (e.Key == Key.Enter)
			{
				CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
				base.SendRequest(com);
				e.Handled = true;
			}

		}
		
		private void Jyunjyo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CommunicationObject com = new CommunicationObject(MessageType.RequestData, TRN_RIREKI, 担当者ID指定, 得意先ID指定, 表示順);
			base.SendRequest(com);

		}

		//private void spRireki_AutoGeneratingColumn(object sender, SpreadAutoGeneratingColumnEventArgs e)
		//{

		//		e.Column.IsReadOnly = true;
			

		//		NumberCellType num;
		//		//Column col = new Column();
		//		//col.HorizontalAlignment = CellHorizontalAlignment.Right;
				
		//		// 自動生成される列にセル型を設定する
		//		switch (e.PropertyName)
		//		{
		//		case "数量":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "重量":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "売上金額":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "通行料":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "支払金額":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "支払通行料":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "明細番号":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "行":
		//			num = new NumberCellType();
		//			num.SpinButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = num;
		//			break;
		//		case "請求日付":
		//			DateTimeCellType d_birth = new DateTimeCellType();
		//			//d_birth.DisplayFieldSet = new DateDisplayFieldSet("gggE年MM月dd日");
		//			d_birth.DropDownButtonVisibility = CellButtonVisibility.NotShow;
		//			e.Column.CellType = d_birth;
		//			break;
		//		case "PostalCode":
		//			GeneralCellType gen = new GeneralCellType();
		//			gen.FormatString = "\"〒\"000\"-\"0000";
		//			e.Column.CellType = gen;
		//			break;
		//		}
		//}

		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	spRireki.AutoFitColumns();
		//}


		private void spRireki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

			GrapeCity.Windows.SpreadGrid.HitTestInfo hinfo = spRireki.HitTest(e.GetPosition(spRireki));
			if (hinfo is GrapeCity.Windows.SpreadGrid.CellHitTestInfo)
			{
				GrapeCity.Windows.SpreadGrid.CellHitTestInfo cellinfo = (GrapeCity.Windows.SpreadGrid.CellHitTestInfo)hinfo;
				//// 行、列がダブルクリックされた情報を取得します。
				//if (cellinfo.RowIndex > -1)
				//{
				//	Console.WriteLine("インデックス {0} の行がダブルクリックされました", cellinfo.RowIndex);
				//}
				//if (cellinfo.ColumnIndex > -1)
				//{
				//	Console.WriteLine("インデックス {0} の列がダブルクリックされました", cellinfo.ColumnIndex);
				//}
				// セルまたはヘッダがダブルクリックされたかどうかを取得します。
				//if (cellinfo.Area == GrapeCity.Windows.SpreadGrid.SpreadArea.Cells)
				//{
				//	Console.WriteLine("セルがダブルクリックされました");
				//}
				//else if (cellinfo.Area == GrapeCity.Windows.SpreadGrid.SpreadArea.ColumnHeader)
				//{
				//	Console.WriteLine("列ヘッダがダブルクリックされました");
				//}
				//else if (cellinfo.Area == GrapeCity.Windows.SpreadGrid.SpreadArea.RowHeader)
				//{
				//	Console.WriteLine("行ヘッダがダブルクリックされました");
				//}

				if (cellinfo.Area == GrapeCity.Windows.SpreadGrid.SpreadArea.Cells)
				{
					var a = spRireki.Cells[cellinfo.RowIndex, "明細番号"].Value.ToString();
					if (MaintenanceMode == null)
					{
						DetailsNumber = a;
						He_MeisaiBangou.Focus();
					}
					if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
					{
						RefferNumber = a;
						He_RuijiMeisaiBangou.Focus();
					}

					Grid_RIREKI.Visibility = Visibility.Collapsed;
				}

			}

		}




	}
}
