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
using System.Linq.Expressions;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 売上明細問合せ
    /// </summary>
    public partial class DLY10010 : RibbonWindowViewBase
    {

        #region DLY10010_Member

        public class DLY10010_Member : INotifyPropertyChanged
        {
            private int _明細番号;
            private int _明細行;
            private DateTime _請求日付;
            private DateTime? _支払日付;
            private DateTime _配送日付;
            private decimal? _配送時間;
            private int? _得意先ID;
            private string _得意先名;
            private int? _請求内訳ID;
            private string _請求内訳名;
            private int? _車輌ID;
            private string _車輌番号;
            private int? _車種ID;
            private string _車種名;
            private int? _支払先ID;
            private string _支払先略称名;
            private int? _乗務員ID;
            private string _乗務員名;
            private string _支払先名２次;
            private string _実運送乗務員;
            private string _乗務員連絡先;
            private decimal _数量;
            private string _単位;
            private decimal _重量;
            private decimal _才数;
            private int _走行ＫＭ;
            private int _実車ＫＭ;
            private decimal _売上単価;
            private int _売上金額;
            private decimal _d売上金額;
            private int _通行料;
            private decimal _d通行料;
            private int _請求割増１;
            private decimal _d請求割増１;
            private int _請求割増２;
            private decimal _d請求割増２;
            private int _請求消費税;
            private int _売上金額計;
            private decimal _d売上金額計;
            private decimal _支払単価;
            private int _支払金額;
            private decimal _d支払金額;
            private int _支払通行料;
            private decimal _d支払通行料;
            private int _支払割増１;
            private int _支払割増２;
			private int _支払消費税;
			private int _差益;
            private int _社内区分;
            private string _確認名称;
            private int _請求税区分;
            private int _支払税区分;
            private int _売上未定区分;
            private int _確認名称区分;
            private int _支払未定区分;
            private string _商品名;
            private string _発地名;
            private string _着地名;
            private string _請求摘要;
            private string _社内備考;
            private int? _S締日;
            private int? _T締日;
            private string _入力区分;
            private string _未定区分名;
            private string _社内区分名;
            private string _請求税区分名;
            private string _請求年月日;
            private string _支払年月日;
            private string _配送年月日;
            private int _発地ID;
            private int _着地ID;
            private int _商品ID;
            private int _自社部門;
            private int _請求運賃計算区分ID;
            private int _支払運賃計算区分ID;



            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public DateTime 請求日付 { get { return _請求日付; } set { _請求日付 = value; NotifyPropertyChanged(); } }
            public DateTime? 支払日付 { get { return _支払日付; } set { _支払日付 = value; NotifyPropertyChanged(); } }
            public DateTime 配送日付 { get { return _配送日付; } set { _配送日付 = value; NotifyPropertyChanged(); } }
            public decimal? 配送時間 { get { return _配送時間; } set { _配送時間 = value; NotifyPropertyChanged(); } }
            public int? 得意先ID { get { return _得意先ID; } set { _得意先ID = value; NotifyPropertyChanged(); } }
            public string 得意先名 { get { return _得意先名; } set { _得意先名 = value; NotifyPropertyChanged(); } }
            public int? 請求内訳ID { get { return _請求内訳ID; } set { _請求内訳ID = value; NotifyPropertyChanged(); } }
            public string 請求内訳名 { get { return _請求内訳名; } set { _請求内訳名 = value; NotifyPropertyChanged(); } }
            public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
            public int? 車種ID { get { return _車種ID; } set { _車種ID = value; NotifyPropertyChanged(); } }
            public string 車種名 { get { return _車種名; } set { _車種名 = value; NotifyPropertyChanged(); } }
            public int? 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
            public string 支払先略称名 { get { return _支払先略称名; } set { _支払先略称名 = value; NotifyPropertyChanged(); } }
            public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
            public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
            public string 支払先名２次 { get { return _支払先名２次; } set { _支払先名２次 = value; NotifyPropertyChanged(); } }
            public string 実運送乗務員 { get { return _実運送乗務員; } set { _実運送乗務員 = value; NotifyPropertyChanged(); } }
            public string 乗務員連絡先 { get { return _乗務員連絡先; } set { _乗務員連絡先 = value; NotifyPropertyChanged(); } }
            public decimal 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            public string 単位 { get { return _単位; } set { _単位 = value; NotifyPropertyChanged(); } }
            public decimal 重量 { get { return _重量; } set { _重量 = value; NotifyPropertyChanged(); } }
            public decimal 才数 { get { return _才数; } set { _才数 = value; NotifyPropertyChanged(); } }
            public int 走行ＫＭ { get { return _走行ＫＭ; } set { _走行ＫＭ = value; NotifyPropertyChanged(); } }
            public int 実車ＫＭ { get { return _実車ＫＭ; } set { _実車ＫＭ = value; NotifyPropertyChanged(); } }
            public decimal 売上単価 { get { return _売上単価; } set { _売上単価 = value; NotifyPropertyChanged(); } }
            public int 売上金額 { get { return _売上金額; } set { _売上金額 = value; NotifyPropertyChanged(); } }
            public decimal d売上金額 { get { return _d売上金額; } set { _d売上金額 = value; NotifyPropertyChanged(); } }
            public int 通行料 { get { return _通行料; } set { _通行料 = value; NotifyPropertyChanged(); } }
            public decimal d通行料 { get { return _d通行料; } set { _d通行料 = value; NotifyPropertyChanged(); } }
            public int 請求割増１ { get { return _請求割増１; } set { _請求割増１ = value; NotifyPropertyChanged(); } }
            public decimal d請求割増１ { get { return _d請求割増１; } set { _d請求割増１ = value; NotifyPropertyChanged(); } }
            public int 請求割増２ { get { return _請求割増２; } set { _請求割増２ = value; NotifyPropertyChanged(); } }
            public decimal d請求割増２ { get { return _d請求割増２; } set { _d請求割増２ = value; NotifyPropertyChanged(); } }
            public int 請求消費税 { get { return _請求消費税; } set { _請求消費税 = value; NotifyPropertyChanged(); } }
            public int 売上金額計 { get { return _売上金額計; } set { _売上金額計 = value; NotifyPropertyChanged(); } }
            public decimal d売上金額計 { get { return _d売上金額計; } set { _d売上金額計 = value; NotifyPropertyChanged(); } }
            public decimal 支払単価 { get { return _支払単価; } set { _支払単価 = value; NotifyPropertyChanged(); } }
            public int 支払金額 { get { return _支払金額; } set { _支払金額 = value; NotifyPropertyChanged(); } }
            public decimal d支払金額 { get { return _d支払金額; } set { _d支払金額 = value; NotifyPropertyChanged(); } }
            public int 支払通行料 { get { return _支払通行料; } set { _支払通行料 = value; NotifyPropertyChanged(); } }
            public decimal d支払通行料 { get { return _d支払通行料; } set { _d支払通行料 = value; NotifyPropertyChanged(); } }
            public int 支払割増１ { get { return _支払割増１; } set { _支払割増１ = value; NotifyPropertyChanged(); } }
            public int 支払割増２ { get { return _支払割増２; } set { _支払割増２ = value; NotifyPropertyChanged(); } }
			public int 支払消費税 { get { return _支払消費税; } set { _支払消費税 = value; NotifyPropertyChanged(); } }
			public int 差益 { get { return _差益; } set { _差益 = value; NotifyPropertyChanged(); } }
            public int 社内区分 { get { return _社内区分; } set { _社内区分 = value; NotifyPropertyChanged(); } }
            public string 確認名称 { get { return _確認名称; } set { _確認名称 = value; NotifyPropertyChanged(); } }
            public int 請求税区分 { get { return _請求税区分; } set { _請求税区分 = value; NotifyPropertyChanged(); } }
            public int 支払税区分 { get { return _支払税区分; } set { _支払税区分 = value; NotifyPropertyChanged(); } }
            public int 売上未定区分 { get { return _売上未定区分; } set { _売上未定区分 = value; NotifyPropertyChanged(); } }
            public int 確認名称区分 { get { return _確認名称区分; } set { _確認名称区分 = value; NotifyPropertyChanged(); } }
            public int 支払未定区分 { get { return _支払未定区分; } set { _支払未定区分 = value; NotifyPropertyChanged(); } }
            public string 商品名 { get { return _商品名; } set { _商品名 = value; NotifyPropertyChanged(); } }
            public string 発地名 { get { return _発地名; } set { _発地名 = value; NotifyPropertyChanged(); } }
            public string 着地名 { get { return _着地名; } set { _着地名 = value; NotifyPropertyChanged(); } }
            public string 請求摘要 { get { return _請求摘要; } set { _請求摘要 = value; NotifyPropertyChanged(); } }
            public string 社内備考 { get { return _社内備考; } set { _社内備考 = value; NotifyPropertyChanged(); } }
            public int? S締日 { get { return _S締日; } set { _S締日 = value; NotifyPropertyChanged(); } }
            public int? T締日 { get { return _T締日; } set { _T締日 = value; NotifyPropertyChanged(); } }
            public string 入力区分 { get { return _入力区分; } set { _入力区分 = value; NotifyPropertyChanged(); } }
            public string 未定区分名 { get { return _未定区分名; } set { _未定区分名 = value; NotifyPropertyChanged(); } }
            public string 社内区分名 { get { return _社内区分名; } set { _社内区分名 = value; NotifyPropertyChanged(); } }
            public string 請求税区分名 { get { return _請求税区分名; } set { _請求税区分名 = value; NotifyPropertyChanged(); } }
            public string 請求年月日 { get { return _請求年月日; } set { _請求年月日 = value; NotifyPropertyChanged(); } }
            public string 支払年月日 { get { return _支払年月日; } set { _支払年月日 = value; NotifyPropertyChanged(); } }
            public string 配送年月日 { get { return _配送年月日; } set { _配送年月日 = value; NotifyPropertyChanged(); } }
            public int 発地ID { get { return _発地ID; } set { _発地ID = value; NotifyPropertyChanged(); } }
            public int 着地ID { get { return _着地ID; } set { _着地ID = value; NotifyPropertyChanged(); } }
            public int 商品ID { get { return _商品ID; } set { _商品ID = value; NotifyPropertyChanged(); } }
            public int 自社部門 { get { return _自社部門; } set { _自社部門 = value; NotifyPropertyChanged(); } }
            public int 請求運賃計算区分ID { get { return _請求運賃計算区分ID; } set { _請求運賃計算区分ID = value; NotifyPropertyChanged(); } }
            public int 支払運賃計算区分ID { get { return _支払運賃計算区分ID; } set { _支払運賃計算区分ID = value; NotifyPropertyChanged(); } }


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

        #endregion

        #region MasterList_Member

        public class MasterList_Member : INotifyPropertyChanged
        {
            private string _選択;
            private int _コード;
            private string _表示名;

            public string 選択 { get { return _選択; } set { _選択 = value; NotifyPropertyChanged(); } }
            public int コード { get { return _コード; } set { _コード = value; NotifyPropertyChanged(); } }
            public string 表示名 { get { return _表示名; } set { _表示名 = value; NotifyPropertyChanged(); } }

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

        #endregion

        #region データアクセス用ID

        private const string GET_CNTL = "M87_CNTL";
        private const string GET_DATA = "DLY10010";
        private const string UPDATE_ROW = "DLY10010_UPDATE";
        private const string UPDATE_ROW2 = "DLY10010_UPDATE2";
        private const string GET_MST1 = "DLY10010_MST1";
        private const string GET_MST2 = "DLY10010_MST2";
        private const string GET_MST3 = "DLY10010_MST3";
        private const string GET_MST4 = "DLY10010_MST4";
        private const string GET_MST5 = "DLY10010_MST5";
        private const string GET_MST6 = "DLY10010_MST6";
        private const string GET_MST7 = "DLY10010_MST7";
        private const string GET_MST8 = "DLY10010_MST8";
        private const string GET_MST9 = "DLY10010_MST9";
        private const string GetTanka = "DLY01010_TANKA";
        private const string GetBumon = "M71_Bumon";

        const string SelectedChar = "a";
        const string UnselectedChar = "";

        const string ReportFileName = @"Files\DLY\DLY10010.rpt";

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
        public class ConfigDLY10010 : FormConfigBase
        {
            public string[] 表示順 { get; set; }
            public bool[] 表示順方向 { get; set; }
            // コンボボックスの位置
            public int 自社部門index { get; set; }
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

            public byte[] spConfig20180118 = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY10010 frmcfg = null;

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
                    var gNo = row.Cells[this._gcSpreadGrid.Columns["明細行"].Index].Value;
                    var wnd = GetWindow(this._gcSpreadGrid);

                    // 入力区分により起動する画面を分けるとしたらココ
                    var kbn = row.Cells[this._gcSpreadGrid.Columns["入力区分"].Index].Value;
                    if (kbn.ToString() == "運転日報")
                    {
                        DLY01010 frm = new DLY01010();
                        frm.初期明細番号 = (int?)mNo;
                        frm.初期行番号 = (int?)gNo;
                        frm.ShowDialog(wnd);
                        if (frm.IsUpdated)
                        {
                            // 日報側で更新された場合、再検索を実行する
                            var parent = ViewBaseCommon.FindVisualParent<DLY10010>(this._gcSpreadGrid);
                            parent.Button_Click_1(null, null);
                        }

                    }
					else if (kbn.ToString() == "売上入力")
                    {
                        DLY02015 frm = new DLY02015();
                        frm.初期明細番号 = (int?)mNo;
                        frm.初期行番号 = (int?)gNo;
                        frm.ShowDialog(wnd);
                        if (frm.IsUpdated)
                        {
                            // 日報側で更新された場合、再検索を実行する
                            var parent = ViewBaseCommon.FindVisualParent<DLY10010>(this._gcSpreadGrid);
                            parent.Button_Click_1(null, null);
                        }
                    }
					else if (kbn.ToString() == "内訳入力")
					{
						DLY02010 frm = new DLY02010();
						frm.初期明細番号 = (int?)mNo;
						frm.初期行番号 = (int?)gNo;
						frm.ShowDialog(wnd);
						if (frm.IsUpdated)
						{
							// 日報側で更新された場合、再検索を実行する
							var parent = ViewBaseCommon.FindVisualParent<DLY10010>(this._gcSpreadGrid);
							parent.Button_Click_1(null, null);
						}
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

        #region BindingMember

        private int pRowCount;
        private int p明細番号;
        private int p明細行;

        string _originalText = null;
        string CellName = string.Empty;
        string CellText = string.Empty;

        public int? 金額計算端数処理区分 = null;

        public class BumonData
        {
            public int 自社部門ID;
            public string 自社部門名;
        }
        private List<BumonData> BumonList;


        private int _p売上金額 = 0;
        public int p売上金額
        {
            get { return this._p売上金額; }
            set { this._p売上金額 = value; NotifyPropertyChanged(); }
        }

        private decimal _p売上単価 = 0;
        public decimal p売上単価
        {
            get { return this._p売上単価; }
            set { this._p売上単価 = value; NotifyPropertyChanged(); }
        }


        private int _p支払金額 = 0;
        public int p支払金額
        {
            get { return this._p支払金額; }
            set { this._p支払金額 = value; NotifyPropertyChanged(); }
        }

        private decimal _p支払単価 = 0;
        public decimal p支払単価
        {
            get { return this._p支払単価; }
            set { this._p支払単価 = value; NotifyPropertyChanged(); }
        }

        bool _請求内訳条件Enabled = false;
        public bool 請求内訳条件Enabled
        {
            get { return this._請求内訳条件Enabled; }
            set { this._請求内訳条件Enabled = value; NotifyPropertyChanged(); }
        }

        string _検索日付名 = null;
        public string 検索日付名
        {
            get { return this._検索日付名; }
            set { this._検索日付名 = value; NotifyPropertyChanged(); }
        }
        string _検索日付選択 = null;
        public string 検索日付選択
        {
            get { return this._検索日付選択; }
            set { this._検索日付選択 = value; NotifyPropertyChanged(); }
        }

        string _検索日付From = null;
        public string 検索日付From
        {
            get { return this._検索日付From; }
            set { this._検索日付From = value; NotifyPropertyChanged(); }
        }

        string _検索日付To = null;
        public string 検索日付To
        {
            get { return this._検索日付To; }
            set { this._検索日付To = value; NotifyPropertyChanged(); }
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

        private string _得意先ID = null;
        public string 得意先ID
        {
            set
            {
                _得意先ID = value;
                NotifyPropertyChanged();
            }
            get { return _得意先ID; }
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

        private string _請求内訳ID = null;
        public string 請求内訳ID
        {
            set
            {
                _請求内訳ID = value;
                NotifyPropertyChanged();
            }
            get { return _請求内訳ID; }
        }

        private string _担当者ID = null;
        public string 担当者ID
        {
            set
            {
                _担当者ID = value;
                NotifyPropertyChanged();
            }
            get { return _担当者ID; }
        }


        private string _支払先ID = null;
        public string 支払先ID
        {
            set
            {
                _支払先ID = value;
                NotifyPropertyChanged();
            }
            get { return _支払先ID; }
        }

        private int? _自社部門ID = null;
        public int? 自社部門ID
        {
            set
            {
                _自社部門ID = value;
                NotifyPropertyChanged();
            }
            get { return _自社部門ID; }
        }

        private string _発地名 = "";
        public string 発地名
        {
            set
            {
                _発地名 = value;
                NotifyPropertyChanged();
            }
            get { return _発地名; }
        }
        private string _着地名 = "";
        public string 着地名
        {
            set
            {
                _着地名 = value;
                NotifyPropertyChanged();
            }
            get { return _着地名; }
        }

        private string _商品名 = "";
        public string 商品名
        {
            set
            {
                _商品名 = value;
                NotifyPropertyChanged();
            }
            get { return _商品名; }
        }

        private string _請求摘要 = "";
        public string 請求摘要
        {
            set
            {
                _請求摘要 = value;
                NotifyPropertyChanged();
            }
            get { return _請求摘要; }
        }

        private string _社内備考 = "";
        public string 社内備考
        {
            set
            {
                _社内備考 = value;
                NotifyPropertyChanged();
            }
            get { return _社内備考; }
        }

        private int? _売上未定区分 = null;
        public int? 売上未定区分
        {
            set
            {
                _売上未定区分 = value;
                NotifyPropertyChanged();
            }
            get { return _売上未定区分; }
        }

        private int? _社内区分 = null;
        public int? 社内区分
        {
            set
            {
                _社内区分 = value;
                NotifyPropertyChanged();
            }
            get { return _社内区分; }
        }

        private int? _確認名称区分 = null;
        public int? 確認名称区分
        {
            set
            {
                _確認名称区分 = value;
                NotifyPropertyChanged();
            }
            get { return _確認名称区分; }
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
        private List<MasterList_Member> _pickup得意先 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup得意先
        {
            get { return this._pickup得意先; }
            set { this._pickup得意先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup支払先 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup支払先
        {
            get { return this._pickup支払先; }
            set { this._pickup支払先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _Pickup仕入先 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup仕入先
        {
            get { return this._Pickup仕入先; }
            set { this._Pickup仕入先 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup乗務員 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup乗務員
        {
            get { return this._pickup乗務員; }
            set { this._pickup乗務員 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup車輌 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup車輌
        {
            get { return this._pickup車輌; }
            set { this._pickup車輌 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup車種 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup車種
        {
            get { return this._pickup車種; }
            set { this._pickup車種 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup発地 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup発地
        {
            get { return this._pickup発地; }
            set { this._pickup発地 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup着地 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup着地
        {
            get { return this._pickup着地; }
            set { this._pickup着地 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickup商品 = new List<MasterList_Member>();
        public List<MasterList_Member> Pickup商品
        {
            get { return this._pickup商品; }
            set { this._pickup商品 = value; NotifyPropertyChanged(); NotifyPropertyChanged("PickupData"); }
        }

        private List<MasterList_Member> _pickupdata = new List<MasterList_Member>();
        public List<MasterList_Member> PickupData
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

        private DataTable 管理データ = null;

        private List<DLY10010_Member> _dUriageData = null;
        public List<DLY10010_Member> 売上明細データ
        {
            get
            {
                return this._dUriageData;
            }
            set
            {
                this._dUriageData = value;
                this.sp売上明細データ.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _売上金額 = 0;
        public decimal 売上金額
        {
            get { return _売上金額; }
            set { _売上金額 = value; NotifyPropertyChanged(); }
        }

        private decimal _請求割増１ = 0;
        public decimal 請求割増１
        {
            get { return _請求割増１; }
            set { _請求割増１ = value; NotifyPropertyChanged(); }
        }

        private decimal _請求割増２ = 0;
        public decimal 請求割増２
        {
            get { return _請求割増２; }
            set { _請求割増２ = value; NotifyPropertyChanged(); }
        }

        private decimal _通行料 = 0;
        public decimal 通行料
        {
            get { return _通行料; }
            set { _通行料 = value; NotifyPropertyChanged(); }
        }

        private decimal _売上合計 = 0;
        public decimal 売上合計
        {
            get { return _売上合計; }
            set { _売上合計 = value; NotifyPropertyChanged(); }
        }

        private decimal _数量 = 0;
        public decimal 数量
        {
            get { return _数量; }
            set { _数量 = value; NotifyPropertyChanged(); }
        }

        private decimal _重量 = 0;
        public decimal 重量
        {
            get { return _重量; }
            set { _重量 = value; NotifyPropertyChanged(); }
        }

        private decimal _支払社内 = 0;
        public decimal 支払社内
        {
            get { return _支払社内; }
            set { _支払社内 = value; NotifyPropertyChanged(); }
        }

        private decimal _支払通行料 = 0;
        public decimal 支払通行料
        {
            get { return _支払通行料; }
            set { _支払通行料 = value; NotifyPropertyChanged(); }
        }

        private decimal _支払合計 = 0;
        public decimal 支払合計
        {
            get { return _支払合計; }
            set { _支払合計 = value; NotifyPropertyChanged(); }
        }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; NotifyPropertyChanged(); }
        }

        private int _表示固定列数 = 5;
        public int 表示固定列数
        {
            get { return _表示固定列数; }
            set { _表示固定列数 = value; NotifyPropertyChanged(); SetupSpreadFixedColumn(this.sp売上明細データ, value); }
        }


        #endregion

        #region ソートデータ変換用
        //public class DLY10010_Member
        //{
        //    public int 明細番号 { get; set; }
        //    public int 明細行 { get; set; }
        //    public DateTime 請求日付 { get; set; }
        //    public DateTime? 支払日付 { get; set; }
        //    public DateTime 配送日付 { get; set; }
        //    public decimal? 配送時間 { get; set; }
        //    public int? 得意先ID { get; set; }
        //    public string 得意先名 { get; set; }
        //    public int? 請求内訳ID { get; set; }
        //    public string 請求内訳名 { get; set; }
        //    public int? 車輌ID { get; set; }
        //    public string 車輌番号 { get; set; }
        //    public int? 車種ID { get; set; }
        //    public string 車種名 { get; set; }
        //    public int? 支払先ID { get; set; }
        //    public string 支払先略称名 { get; set; }
        //    public int? 乗務員ID { get; set; }
        //    public string 乗務員名 { get; set; }
        //    public string 支払先名２次 { get; set; }
        //    public string 実運送乗務員 { get; set; }
        //    public string 乗務員連絡先 { get; set; }
        //    public decimal 数量 { get; set; }
        //    public string 単位 { get; set; }
        //    public decimal 重量 { get; set; }
        //    public decimal 才数 { get; set; }

        //    public int 走行ＫＭ { get; set; }
        //    public int 実車ＫＭ { get; set; }
        //    public decimal 売上単価 { get; set; }
        //    public int 売上金額 { get; set; }
        //    public int 通行料 { get; set; }
        //    public int 請求割増１ { get; set; }
        //    public int 請求割増２ { get; set; }
        //    public int 請求消費税 { get; set; }

        //    public int 売上金額計 { get; set; }

        //    public decimal 支払単価 { get; set; }
        //    public int 支払金額 { get; set; }
        //    public int 支払通行料 { get; set; }
        //    public int 支払割増１ { get; set; }
        //    public int 支払割増２ { get; set; }
        //    public int 支払消費税 { get; set; }
        //    public int 社内区分 { get; set; }
        //    public int 請求税区分 { get; set; }
        //    public int 支払税区分 { get; set; }
        //    public int 売上未定区分 { get; set; }
        //    public int 支払未定区分 { get; set; }
        //    public string 商品名 { get; set; }
        //    public string 発地名 { get; set; }
        //    public string 着地名 { get; set; }
        //    public string 請求摘要 { get; set; }
        //    public string 社内備考 { get; set; }
        //    public int? S締日 { get; set; }
        //    public int? T締日 { get; set; }
        //    public string 入力区分 { get; set; }

        //    public string 未定区分名 { get; set; }
        //    public string 社内区分名 { get; set; }
        //    public string 請求税区分名 { get; set; }

        //    public string 請求年月日 { get; set; }
        //    public string 支払年月日 { get; set; }
        //    public string 配送年月日 { get; set; }

        //    public int 発地ID { get; set; }
        //    public int 着地ID { get; set; }
        //    public int 商品ID { get; set; }

        //}

        #endregion

        // 試験
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        #region DLY10010

        /// <summary>
        /// 売上明細問合せ
        /// </summary>
        public DLY10010()
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
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp売上明細データ.Rows.Clear();
            this.sp_Config = AppCommon.SaveSpConfig(this.sp売上明細データ);

            // コンテキストメニューの作成

            var dat = RangeVisibilities["得意先"];

            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M01_TOK_SHIHARAI_SCH", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });
            base.MasterMaintenanceWindowList.Add("M10_UHK_UC", new List<Type> { null, typeof(SCH02020) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);

            // 試験
            #region "権限関係"
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            #endregion

            frmcfg = (ConfigDLY10010)ucfg.GetConfigValue(typeof(ConfigDLY10010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY10010();
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
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;
                this.cmb検索日付種類.SelectedIndex = frmcfg.区分1;
                this.検索日付From = frmcfg.集計期間From;
                this.検索日付To = frmcfg.集計期間To;
            }
            #endregion

            AppCommon.LoadSpConfig(this.sp売上明細データ, frmcfg.spConfig20180118 != null ? frmcfg.spConfig20180118 : this.sp_Config, "売上明細データ");
            this.表示固定列数 = this.sp売上明細データ.FrozenColumnCount;


            //ComboBoxに値を設定する
            GetComboBoxItems();

            this.spPickupList.ActiveCellPosition = CellPosition.Empty;
            //this.spPickupList.RowCount = 0;
            //this.sp売上明細データ.RowCount = 0;
            //this.sp売上明細データ.PreviewKeyDown += sp売上明細データ_PreviewKeyDown;
            ButtonCellType btn = this.sp売上明細データ.Columns[0].CellType as ButtonCellType;
            btn.Command = new cmd売上詳細表示(sp売上明細データ);


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

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CNTL, 1, 0));

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST1, "得意先"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST2, "支払先"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST3, "仕入先"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST4, "乗務員"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST5, "車輌"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST6, "車種"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST7, "発地"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST8, "着地"));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_MST9, "商品"));

            this.textbox検索日付From.SetFocus();

            this.ピックアップ種類 = "得意先";

            this.売上明細データ = null;
        }

        #endregion

        #region sp売上明細データ_PreviewKeyDown

        private void sp売上明細データ_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && sp売上明細データ.EditElement == null)
            {
                e.Handled = true;
            }
            if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region コンボボックス取得

        /// <summary>
        /// コンボボックスのアイテムをDBから取得
        /// </summary>
        private void GetComboBoxItems()
        {

            AppCommon.SetutpComboboxList(this.cmb検索日付種類, false);
            AppCommon.SetutpComboboxList(this.cmb未定区分, false);
            AppCommon.SetutpComboboxList(this.cmb確認名称, false);
            AppCommon.SetutpComboboxList(this.cmbPickup, false);
            AppCommon.SetutpComboboxList(this.cmb社内区分, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定0, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定1, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定2, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定3, false);
            AppCommon.SetutpComboboxList(this.cmb表示順指定4, false);

            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "売上未定区分", "日次", "運転日報入力", "未定区分", false);
            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "社内区分", "日次", "運転日報入力", "社内区分", false);
            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "確認名称区分", "マスタ", "基礎情報設定", "確認名称", false);
			AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "請求税区分", "日次", "運転日報入力", "税区分", false);

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetBumon));
        }

        #endregion

        #region ピックアップ指定のGridの読み込み

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

        #endregion

        #region エラーメッセージ

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
            List<MasterList_Member> list = new List<MasterList_Member>();
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                case GET_CNTL:
                    管理データ = tbl;
                    this.txt請求割増１.Label_Context = AppCommon.GetWarimasiName1(tbl);
                    this.txt請求割増２.Label_Context = AppCommon.GetWarimasiName2(tbl);
                    AppCommon.SetupSpreadHeaderWarimasiName(this.sp売上明細データ, tbl);
                    金額計算端数処理区分 = (int)tbl.Rows[0]["金額計算端数区分"];
                    break;
                case GET_DATA:
                    base.SetFreeForInput();
                    var ds = data as DataSet;
                    if (ds == null)
                    {
                        this.売上明細データ = null;
                        textbox検索日付From.Focus();
                    }
                    else
                    {
                        売上明細データ = (List<DLY10010_Member>)AppCommon.ConvertFromDataTable(typeof(List<DLY10010_Member>), ds.Tables["DataList"]);
                        //this.sp売上明細データ.ItemsSource = 売上明細データ;
                        if (this.売上明細データ.Count == 0)
                        {
							//Summary(ds.Tables["TotalList"]);
							Summary();
							textbox検索日付From.Focus();
                            this.ErrorMessage = "該当するデータはありません。";
                            return;
                        }

                        DataReSort();
						//Summary(ds.Tables["TotalList"]);
						Summary();


                        textbox検索日付From.Focus();

						foreach (var row in sp売上明細データ.Rows)
						{
							if (AppCommon.IntParse(sp売上明細データ.Cells[row.Index, "確認名称区分"].Value.ToString()) == 0)
							{
								sp売上明細データ.Cells[row.Index, "確認名称区分"].Foreground = new SolidColorBrush(Colors.Red);
							}
							if (AppCommon.IntParse(sp売上明細データ.Cells[row.Index, "社内区分"].Value.ToString()) == 1)
							{
								sp売上明細データ.Cells[row.Index, "社内区分"].Foreground = new SolidColorBrush(Colors.Red);
							}
							if (AppCommon.IntParse(sp売上明細データ.Cells[row.Index, "売上未定区分"].Value.ToString()) == 1)
							{
								sp売上明細データ.Cells[row.Index, "売上未定区分"].Foreground = new SolidColorBrush(Colors.Red);
							}
						}

                        #region "ロック設定"
                        if (DataUpdateVisible == System.Windows.Visibility.Hidden)
                        {
                            権限Get.SpreadGridLock(this, true);
                        }
                        #endregion
                    }
                    break;
                case UPDATE_ROW:
                    //Summary(null);
                    break;
                case GET_MST1:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup得意先 = list;
                    break;
                case GET_MST2:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup支払先 = list;
                    break;
                case GET_MST3:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup仕入先 = list;
                    break;
                case GET_MST4:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup乗務員 = list;
                    break;
                case GET_MST5:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup車輌 = list;
                    break;
                case GET_MST6:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup車種 = list;
                    break;
                case GET_MST7:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup発地 = list;
                    break;
                case GET_MST8:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup着地 = list;
                    break;
                case GET_MST9:
                    list = (List<MasterList_Member>)AppCommon.ConvertFromDataTable(list.GetType(), tbl);
                    this.Pickup商品 = list;
                    break;

                case GetBumon:
                    this.BumonList = new List<BumonData>();
                    foreach (DataRow row in tbl.Rows)
                    {
                        BumonData xxx = new BumonData();
                        xxx.自社部門ID = (int)row["自社部門ID"];
                        xxx.自社部門名 = row["自社部門名"] as string;
                        BumonList.Add(xxx);
                    }
                    List<CodeData> lll = new List<CodeData>();
                    lll.Add(new CodeData() { コード = 0, 表示順 = 0, 表示名 = "(全部門検索)" });
                    foreach (var row in BumonList)
                    {
                        lll.Add(new CodeData()
                        {
                            コード = row.自社部門ID,
                            表示名 = row.自社部門名,
                            表示順 = row.自社部門ID
                        });
                    }
                    cmb部門指定.ComboboxItems = lll;
                    break;

                case UPDATE_ROW2:

                    //変数定義
                    int TOKFlg = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["請求運賃計算区分ID"].Index].Value);
                    int SHRFlg = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["支払運賃計算区分ID"].Index].Value);
                    int p計算区分 = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["請求運賃計算区分ID"].Index].Value);
                    int p請求支払区分 = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["支払運賃計算区分ID"].Index].Value);
                    decimal p数量 = Convert.ToDecimal(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["数量"].Index].Value);
                    decimal p重量 = Convert.ToDecimal(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["重量"].Index].Value);
                    decimal p売上単価 = Convert.ToDecimal(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["売上単価"].Index].Value);
                    decimal p支払単価 = Convert.ToDecimal(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["支払単価"].Index].Value);
                    int p得意先ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["得意先ID"].Index].Value);
                    int p支払先ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["支払先ID"].Index].Value);
                    int p発地ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["発地ID"].Index].Value);
                    int p着地ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["着地ID"].Index].Value);
                    int p商品ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["商品ID"].Index].Value);
                    int p走行ＫＭ = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["走行ＫＭ"].Index].Value);
                    int p車種ID = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["車種ID"].Index].Value);
                    p明細番号 = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["明細番号"].Index].Value);
                    p明細行 = Convert.ToInt32(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["明細行"].Index].Value);
                    decimal goukei = 0;

                    p請求支払区分 = 0;
                    int 金額 = 0;
                    switch (TOKFlg)
                    {
                        case 0:
                            // 手入力は何もしない
                            break;
                        case 1:
                            // 数量 ｘ 単価
                            switch (金額計算端数処理区分)
                            {
                                case 0:
                                    金額 = (int)Math.Floor((p数量 * p売上単価));
                                    break;
                                case 1:
                                    金額 = (int)Math.Ceiling((p数量 * p売上単価));
                                    break;
                                case 2:
                                    金額 = (int)Math.Round((p数量 * p売上単価), 0, MidpointRounding.AwayFromZero);
                                    break;
                            }

                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Text = 金額.ToString();
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Value = 金額;

                            goukei = (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Value ?? 0) + (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d請求割増１"].Index].Value ?? 0) + (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d請求割増２"].Index].Value ?? 0);
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額計"].Index].Text = goukei.ToString();

                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Goukei", 1, 金額, p売上単価));
                            break;
                        case 2:
                            // 重量 ｘ 単価
                            switch (金額計算端数処理区分)
                            {
                                case 0:
                                    金額 = (int)Math.Floor((p重量 * p売上単価));
                                    break;
                                case 1:
                                    金額 = (int)Math.Ceiling((p重量 * p売上単価));
                                    break;
                                case 2:
                                    金額 = (int)Math.Round((p重量 * p売上単価), 0, MidpointRounding.AwayFromZero);
                                    break;
                            }
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Text = 金額.ToString();
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Value = 金額;

                            goukei = (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額"].Index].Value ?? 0) + (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d請求割増１"].Index].Value ?? 0) + (decimal)(sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d請求割増２"].Index].Value ?? 0);
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d売上金額計"].Index].Text = goukei.ToString();

                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Goukei", 1, 金額, p売上単価));
                            break;
                        case 3:
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
                            break;
                        case 4:
                            if (p得意先ID != 0 && p商品ID != 0)// && p重量 != 0 && p数量 != 0
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


                    p請求支払区分 = 1;
                    switch (SHRFlg)
                    {
                        case 0:
                            // 手入力は何もしない
                            break;
                        case 1:
                            // 数量 ｘ 単価

                            switch (金額計算端数処理区分)
                            {
                                case 0:
                                    金額 = (int)Math.Floor((p数量 * p支払単価));
                                    break;
                                case 1:
                                    金額 = (int)Math.Ceiling((p数量 * p支払単価));
                                    break;
                                case 2:
                                    金額 = (int)Math.Round((p数量 * p支払単価), 0, MidpointRounding.AwayFromZero);
                                    break;
                            }

                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d支払金額"].Index].Text = 金額.ToString();
                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Total", 1, 金額, p支払単価));
                            break;
                        case 2:
                            // 重量 ｘ 単価
                            switch (金額計算端数処理区分)
                            {
                                case 0:
                                    金額 = (int)Math.Floor((p重量 * p支払単価));
                                    break;
                                case 1:
                                    金額 = (int)Math.Ceiling((p重量 * p支払単価));
                                    break;
                                case 2:
                                    金額 = (int)Math.Round((p重量 * p支払単価), 0, MidpointRounding.AwayFromZero);
                                    break;
                            }
                            sp売上明細データ.Cells[pRowCount, this.sp売上明細データ.Columns["d支払金額"].Index].Text = 金額.ToString();
                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Total", 1, 金額, p支払単価));
                            break;
                        case 3:
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
                            break;
                        case 4:
                            if (p得意先ID != 0 && p商品ID != 0)// && p重量 != 0 && p数量 != 0
                            {
                                SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
                            }
                            break;
                        case 5:
                            if (p得意先ID != 0 && p重量 != 0 && p数量 != 0)
                            {
                                SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
                            }
                            break;
                        case 6:
                            if (p得意先ID != 0 && p数量 != 0)
                            {
                                SendRequest(new CommunicationObject(MessageType.RequestData, GetTanka, p計算区分, p請求支払区分, p支払先ID, p発地ID, p着地ID, p商品ID, p車種ID, p走行ＫＭ, p重量, p数量, 金額計算端数処理区分));
                            }
                            break;
                    }
                    Next_calculation();
					if (CloseFlg) { CloseFlg = false; this.Close(); }
                    break;


                //[売上金額・売上単価]算出
                case "DLY01010_TANKA":
                    if (tbl != null)
                    {
                        int kbn = (int)tbl.Rows[0]["Kubun"];
                        decimal tanka = (decimal)tbl.Rows[0]["Tanka"];
                        decimal kingaku = (decimal)tbl.Rows[0]["Kingaku"];
                        if (kbn == 0)
                        {
                            //売上金額計算
                            if (tanka >= 0)
                            {
                                p売上単価 = (decimal)tanka;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["売上単価"].Index].Text = p売上単価.ToString();
                            }
                            else
                            {
                                p売上単価 = 0;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["売上単価"].Index].Text = p売上単価.ToString();
                            }
                            if (kingaku >= 0)
                            {
                                p売上金額 = (int)kingaku;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["d売上金額"].Index].Text = p売上金額.ToString();
                            }
                            else
                            {
                                p売上金額 = 0;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["d売上金額"].Index].Text = p売上金額.ToString();
                            }
                            decimal meisai = AppCommon.DecimalParse(this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["明細番号"].Index].Text);
                            int gyo = AppCommon.IntParse(this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["明細行"].Index].Text);
                            decimal d合計 = 売上明細データ.Where(c => c.明細番号 == meisai && c.明細行 == gyo).Sum(c => c.売上金額 + c.請求割増１ + c.請求割増２);
                            this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["d売上金額計"].Index].Text = d合計.ToString();

                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Goukei", 1, p売上金額, p売上単価));
                        }
                        else
                        {
                            if (tanka >= 0)
                            {
                                p支払単価 = (decimal)tanka;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["支払単価"].Index].Text = p支払単価.ToString();
                            }
                            else
                            {
                                p支払単価 = 0;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["支払単価"].Index].Text = p支払単価.ToString();
                            }
                            if (kingaku >= 0)
                            {
                                p支払金額 = (int)kingaku;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["d支払金額"].Index].Text = p支払金額.ToString();
                            }
                            else
                            {
                                p売上金額 = 0;
                                this.sp売上明細データ[pRowCount, sp売上明細データ.Columns["d支払金額"].Index].Text = p売上金額.ToString();
                            }
                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, p明細番号, p明細行, "Total", 1, p支払金額, p支払単価));

                        }
                    }
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
            if (this.sp売上明細データ.ActiveCellPosition.Row < 0)
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
            if (this.sp売上明細データ.ActiveCellPosition.Row < 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }
            DataTable CSVデータ = new DataTable();
            //リストをデータテーブルへ
            AppCommon.ConvertToDataTable(売上明細データ, CSVデータ);
            CSVデータ.Columns.Remove("社内区分");
            CSVデータ.Columns.Remove("請求税区分");
            CSVデータ.Columns.Remove("支払税区分");
            CSVデータ.Columns.Remove("売上未定区分");
            CSVデータ.Columns.Remove("支払未定区分");
            CSVデータ.Columns.Remove("請求運賃計算区分ID");
            CSVデータ.Columns.Remove("支払運賃計算区分ID");
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
                CSVData.SaveCSV(CSVデータ, sfd.FileName, true, true, false, ',', true);
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }
        #endregion

        /// <summary>
        /// 印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            PrintOut();
        }

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

        #region 印刷

        void PrintOut()
        {
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
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付FROM", VALUE=(this.検索日付From==null?"":this.検索日付From)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付TO", VALUE=(this.検索日付To==null?"":this.検索日付To)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="得意先指定", VALUE=(this.txt得意先指定.Text2==null?"":this.txt得意先指定.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="内訳指定", VALUE=(this.txt内訳指定.Text2==null?"":this.txt内訳指定.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="支払先指定", VALUE=(this.txtbox支払先.Text2==null?"":this.txtbox支払先.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="部門指定", VALUE=(this.cmb部門指定.Text==null?"":this.cmb部門指定.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="未定区分", VALUE=(this.cmb未定区分.Text==null?"":this.cmb未定区分.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="商品名", VALUE=(this.商品名==null?"":this.商品名)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="発地名", VALUE=(this.発地名==null?"":this.発地名)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="着地名", VALUE=(this.着地名==null?"":this.着地名)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="社内備考", VALUE=(this.社内備考==null?"":this.社内備考)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="請求摘要", VALUE=(this.請求摘要==null?"":this.請求摘要)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="表示順序", VALUE=string.Format("{0} {1} {2} {3} {4}", 表示順名[0], 表示順名[1], 表示順名[2], 表示順名[3], 表示順名[4])},
				};
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = null;

                DataTable 印刷データ = new DataTable("売上明細一覧");
                //リストをデータテーブルへ
                Dictionary<string, string> changecols = new Dictionary<string, string>()
				{
					{ "請求年月日", "請求日付" },
					{ "支払年月日", "支払日付" },
					{ "配送年月日", "配送日付" },
					{ "d売上金額", "売上金額" },
					{ "d通行料", "通行料" },
					{ "d請求割増１", "請求割増１" },
					{ "d請求割増２", "請求割増２" },
					{ "d売上金額計", "売上金額計" },
					{ "d支払金額", "支払金額" },
					{ "d支払通行料", "支払通行料" },
				};
                AppCommon.ConvertSpreadDataToTable<DLY10010_Member>(this.sp売上明細データ, 印刷データ, changecols);

                view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.PrinterInfo = frmcfg.PrinterInfo;		// 印刷設定をセット
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterInfo = view.PrinterInfo;		// 印刷設定を取得
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

        #region 検索ボタン

        /// <summary>
        /// 検索ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // ★
            // ★ このイベントの引数 sender と e はSPREAD内の詳細ボタン処理後に呼び出されるときは null である。
            // ★

			sp売上明細データ.FilterDescriptions.Clear();

            try
            {

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

                if (ExpSyousai.IsExpanded == true)
                {
                    ExpSyousai.IsExpanded = false;
                }

                DateTime? p検索日付From = null;
                DateTime? p検索日付To = null;
                int p検索日付区分 = 0;
                int? 得意先ID = null;
                int? 担当者ID = null;
                int? 支払先ID = null;
                int? 請求内訳ID = null;
                int p自社部門ID = 0;

                int iwk;
                if (int.TryParse(this.得意先ID, out iwk) == true)
                {
                    得意先ID = iwk;
                }
                if (int.TryParse(this.担当者ID, out iwk) == true)
                {
                    担当者ID = iwk;
                }
                if (int.TryParse(this.支払先ID, out iwk) == true)
                {
                    支払先ID = iwk;
                }
                if (int.TryParse(this.請求内訳ID, out iwk) == true)
                {
                    請求内訳ID = iwk;
                }
                p自社部門ID = (this.自社部門ID == null) ? 0 : (int)this.自社部門ID;

                if (int.TryParse(this.検索日付選択, out p検索日付区分) != true)
                {
                    p検索日付区分 = 0;
                }
                DateTime dtwk;
                if (DateTime.TryParse(this.検索日付From, out dtwk) == true)
                {
                    p検索日付From = dtwk;
                }
                if (DateTime.TryParse(this.検索日付To, out dtwk) == true)
                {
                    p検索日付To = dtwk;
                }

                int p得意先FROM = -1;
                if (string.IsNullOrWhiteSpace(this.得意先FROM) != true)
                {
                    int.TryParse(this.得意先FROM, out p得意先FROM);
                }
                int p得意先TO = -1;
                if (string.IsNullOrWhiteSpace(this.得意先TO) != true)
                {
                    int.TryParse(this.得意先TO, out p得意先TO);
                }
                int p支払先FROM = -1;
                if (string.IsNullOrWhiteSpace(this.支払先FROM) != true)
                {
                    int.TryParse(this.支払先FROM, out p支払先FROM);
                }
                int p支払先TO = -1;
                if (string.IsNullOrWhiteSpace(this.支払先TO) != true)
                {
                    int.TryParse(this.支払先TO, out p支払先TO);
                }
                int p仕入先FROM = -1;
                if (string.IsNullOrWhiteSpace(this.仕入先FROM) != true)
                {
                    int.TryParse(this.仕入先FROM, out p仕入先FROM);
                }
                int p仕入先TO = -1;
                if (string.IsNullOrWhiteSpace(this.仕入先TO) != true)
                {
                    int.TryParse(this.仕入先TO, out p仕入先TO);
                }
                int p乗務員FROM = -1;
                if (string.IsNullOrWhiteSpace(this.乗務員FROM) != true)
                {
                    int.TryParse(this.乗務員FROM, out p乗務員FROM);
                }
                int p乗務員TO = -1;
                if (string.IsNullOrWhiteSpace(this.乗務員TO) != true)
                {
                    int.TryParse(this.乗務員TO, out p乗務員TO);
                }
                int p車輌FROM = -1;
                if (string.IsNullOrWhiteSpace(this.車輌FROM) != true)
                {
                    int.TryParse(this.車輌FROM, out p車輌FROM);
                }
                int p車輌TO = -1;
                if (string.IsNullOrWhiteSpace(this.車輌TO) != true)
                {
                    int.TryParse(this.車輌TO, out p車輌TO);
                }
                int p車種FROM = -1;
                if (string.IsNullOrWhiteSpace(this.車種FROM) != true)
                {
                    int.TryParse(this.車種FROM, out p車種FROM);
                }
                int p車種TO = -1;
                if (string.IsNullOrWhiteSpace(this.車種TO) != true)
                {
                    int.TryParse(this.車種TO, out p車種TO);
                }
                int p発地FROM = -1;
                if (string.IsNullOrWhiteSpace(this.発地FROM) != true)
                {
                    int.TryParse(this.発地FROM, out p発地FROM);
                }
                int p発地TO = -1;
                if (string.IsNullOrWhiteSpace(this.発地TO) != true)
                {
                    int.TryParse(this.発地TO, out p発地TO);
                }
                int p着地FROM = -1;
                if (string.IsNullOrWhiteSpace(this.着地FROM) != true)
                {
                    int.TryParse(this.着地FROM, out p着地FROM);
                }
                int p着地TO = -1;
                if (string.IsNullOrWhiteSpace(this.着地TO) != true)
                {
                    int.TryParse(this.着地TO, out p着地TO);
                }
                int p商品FROM = -1;
                if (string.IsNullOrWhiteSpace(this.商品FROM) != true)
                {
                    int.TryParse(this.商品FROM, out p商品FROM);
                }
                int p商品TO = -1;
                if (string.IsNullOrWhiteSpace(this.商品TO) != true)
                {
                    int.TryParse(this.商品TO, out p商品TO);
                }

                List<int?[]> IdList = new List<int?[]>();
                foreach (var data in new List<int?[]> {(from r in this.Pickup得意先 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup支払先 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup仕入先 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup乗務員 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup車輌 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup車種 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup発地 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup着地 where r.選択==SelectedChar select (int?)r.コード).ToArray()
				,(from r in this.Pickup商品 where r.選択==SelectedChar select (int?)r.コード).ToArray()
															})
                {
                    IdList.Add(data);
                }
                int p締日 = -1;
                if (string.IsNullOrWhiteSpace(this.ピックアップ締日) != true)
                {
                    int.TryParse(this.ピックアップ締日, out p締日);
                }
                CommunicationObject com
                    = new CommunicationObject(MessageType.RequestData, "DLY10010", 担当者ID,
                        得意先ID, 支払先ID, 請求内訳ID, p検索日付From, p検索日付To, p検索日付区分, p自社部門ID, this.売上未定区分, this.社内区分, this.確認名称区分
                        , 商品名, 発地名, 着地名, 請求摘要, 社内備考, p締日
                        , p得意先FROM, p得意先TO
                        , p支払先FROM, p支払先TO
                        , p仕入先FROM, p仕入先TO
                        , p乗務員FROM, p乗務員TO
                        , p車輌FROM, p車輌TO
                        , p車種FROM, p車種TO
                        , p発地FROM, p発地TO
                        , p着地FROM, p着地TO
                        , p商品FROM, p商品TO
                        , IdList[0]
                        , IdList[1]
                        , IdList[2]
                        , IdList[3]
                        , IdList[4]
                        , IdList[5]
                        , IdList[6]
                        , IdList[7]
                        , IdList[8]
                        );

                base.SendRequest(com);
                base.SetBusyForInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 日付用コンボボックス

        private void cmb検索日付種類_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.表示順名.Contains("指定日付"))
            {
                DataReSort();
            }
        }

        #endregion

        #region コンボボックス重複チェック

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
            DataReSort();
        }

        #endregion

        #region SortButton_Click

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            DataReSort();
        }

        #endregion

        #region 表示順変更

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
            this.sp売上明細データ.SortDescriptions.Clear();
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
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            sort = new SpreadSortDescription();
                            sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                            sort.ColumnName = "明細行";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        case "指定日付":
                            sort.ColumnName = (cmb検索日付種類.Combo_SelectedItem as CodeData).表示名 + "年月日";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        case "運行者名":
                            sort.ColumnName = "乗務員名";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        default:
                            sort.ColumnName = cd.表示名;
                            this.sp売上明細データ.SortDescriptions.Add(sort);
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

        #region リスト全解除

        private void AllDeSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (var row in this.PickupData)
            {
                row.選択 = UnselectedChar;
            }
        }
        #endregion

        #region ピックアップ全選択

        private void AllSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (var row in this.PickupData)
            {
                row.選択 = SelectedChar;
            }
        }

        #endregion

        #region ピックアップクリア

        private void AllClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var tbl in new List<MasterList_Member>[] { Pickup得意先, Pickup支払先, Pickup仕入先, Pickup乗務員, Pickup車輌, Pickup車種, Pickup発地, Pickup着地, Pickup商品, })
            {
                foreach (var rec in tbl.Where(x => x.選択 != UnselectedChar))
                {
                    rec.選択 = UnselectedChar;
                }
            }
            this.得意先FROM = string.Empty;
            this.得意先TO = string.Empty;
            this.支払先FROM = string.Empty;
            this.支払先TO = string.Empty;
            this.仕入先FROM = string.Empty;
            this.仕入先TO = string.Empty;
            this.乗務員FROM = string.Empty;
            this.乗務員TO = string.Empty;
            this.車輌FROM = string.Empty;
            this.車輌TO = string.Empty;
            this.車種FROM = string.Empty;
            this.車種TO = string.Empty;
            this.発地FROM = string.Empty;
            this.発地TO = string.Empty;
            this.着地FROM = string.Empty;
            this.着地TO = string.Empty;
            this.商品FROM = string.Empty;
            this.商品TO = string.Empty;
            this.ピックアップ締日 = string.Empty;
        }

        #endregion

        #region 合計計算

        // 合計計算
        void Summary()
        {

            売上金額 = 0m;
            請求割増１ = 0m;
            請求割増２ = 0m;
            通行料 = 0m;
            売上合計 = 0m;
            数量 = 0m;
            重量 = 0m;
            支払社内 = 0m;
            支払通行料 = 0m;
            支払合計 = 0m;

			//if (tbl == null)
			//	return;

			//if (tbl.Rows.Count == 0)
			//{
			//	return;
			//}


			if (sp売上明細データ.Columns[1].Name == null)
			{
				return;
			}

			DataTable 印刷データ = new DataTable("印刷データ");

			Dictionary<string, string> changecols = new Dictionary<string, string>()
			{
			};

			AppCommon.ConvertSpreadDataToTable<DLY10010_Member>(this.sp売上明細データ, 印刷データ, changecols);

			売上金額 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d売上金額)", null).ToString());
			請求割増１ = AppCommon.DecimalParse(印刷データ.Compute("Sum(d請求割増１)", null).ToString());
			請求割増２ = AppCommon.DecimalParse(印刷データ.Compute("Sum(d請求割増２)", null).ToString());
			通行料 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d通行料)", null).ToString());
			売上合計 = 売上金額 + 請求割増１ + 請求割増２ + 通行料;
			重量 = AppCommon.DecimalParse(印刷データ.Compute("Sum(重量)", null).ToString());
			数量 = AppCommon.DecimalParse(印刷データ.Compute("Sum(数量)", null).ToString());
			支払社内 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d支払金額)", null).ToString());
			支払通行料 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d支払通行料)", null).ToString());
			支払合計 = 支払社内 + 支払通行料;

        }

        #endregion

        #region コンボボックスリスト作成

        /// <summary>
        /// 部門のコンボボックスのリスト準備完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb部門指定_DataListInitialized(object sender, RoutedEventArgs e)
        {
            this.cmb部門指定.SelectedIndex = frmcfg.自社部門index;
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
            spPickupList.ActiveCellPosition = CellPosition.Empty;
        }

        #endregion

        #region SPREAD CellEnter

        private void sp売上明細データ_CellEnter(object sender, SpreadCellEnterEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            this._originalText = grid.Cells[e.Row, e.Column].Text;
        }

        #endregion

        #region SPREAD CellEditEnding

        private void sp売上明細データ_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            CellName = e.CellPosition.ColumnName;
            CellText = sp売上明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;

            //Cell売上金額 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "売上金額"].Text);
            //Cell請求割増１ = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "請求割増１"].Text);
            //Cell請求割増２ = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "請求割増２"].Text);
            //Cell通行料 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "通行料"].Text);
            //Cell数量 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "数量"].Text);
            //Cell重量 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "重量"].Text);
            //Cell支払社内 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "支払金額"].Text);
            //Cell支払通行料 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "支払通行料"].Text);

			//スプレッドコンボイベント関連付け解除				
			if (sp売上明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = sp売上明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged -= comboEdit_SelectionChanged;
				}
			}

			if (sp売上明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = sp売上明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
				if (gcchk != null)
				{
					gcchk.Checked -= checkEdit_Checked;
					gcchk.Unchecked -= checkEdit_Unchecked;
				}
			}				

        }

		/// <summary>				
		/// comboEdit_SelectionChanged				
		/// スプレッドコンボリストチェンジイベント				
		/// </summary>				
		/// <param name="sender"></param>				
		/// <param name="e"></param>				
		private void comboEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sp売上明細データ.ActiveCell.IsEditing)
			{
				sp売上明細データ.CommitCellEdit();
			}
		}				

        #endregion

        #region SPREAD CellEditEnded

        private void sp売上明細データ_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
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
                string ctext = sp売上明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                ctext = ctext == null ? string.Empty : ctext;
				if (cname == CellName && ctext == CellText && (!(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType) && !(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)))
				{
					if (CloseFlg) { CloseFlg = false; }				
                    // セルの値が変化していなければ何もしない
                    return;
                }

				//リロードせずに対象セルのみ色変更
				if (cname == "確認名称区分")
				{
					if (AppCommon.IntParse(sp売上明細データ.Cells[e.CellPosition.Row, "確認名称区分"].Value.ToString()) == 0)
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "確認名称区分"].Foreground = new SolidColorBrush(Colors.Red);
					}
					else
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "確認名称区分"].Foreground = new SolidColorBrush(Colors.Black);
					}
				}
				if (cname == "社内区分")
				{
					if (AppCommon.IntParse(sp売上明細データ.Cells[e.CellPosition.Row, "社内区分"].Value.ToString()) == 1)
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "社内区分"].Foreground = new SolidColorBrush(Colors.Red);
					}
					else
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "社内区分"].Foreground = new SolidColorBrush(Colors.Black);
					}
				}
				if (cname == "売上未定区分")
				{
					if (AppCommon.IntParse(sp売上明細データ.Cells[e.CellPosition.Row, "売上未定区分"].Value.ToString()) == 1)
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "売上未定区分"].Foreground = new SolidColorBrush(Colors.Red);
					}
					else
					{
						sp売上明細データ.Cells[e.CellPosition.Row, "売上未定区分"].Foreground = new SolidColorBrush(Colors.Black);
					}
				}


                var row = gcsp.Rows[e.CellPosition.Row];
                pRowCount = e.CellPosition.Row;
                object val = row.Cells[e.CellPosition.Column].Value;
                val = val == null ? "" : val;
                if (cname == "売上未定区分")
                {
                    val = row.Cells[e.CellPosition.Column].Value.ToString();
                }
                if (cname == "確認名称区分")
                {
                    val = row.Cells[e.CellPosition.Column].Value.ToString();
                }
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

                if (cname.Contains("配送時間") == true && val == "")
                {
                    val = 0;
                }
                var colM = gcsp.Columns.Where(x => x.Name == "明細番号").FirstOrDefault();
                if (colM == null)
                {
                    // ありえない
                    throw new Exception("システムエラー");
                }
                var colL = gcsp.Columns.Where(x => x.Name == "明細行").FirstOrDefault();
                if (colL == null)
                {
                    // ありえない
                    throw new Exception("システムエラー");
                }

                //変数定義
                int TOKFlg = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["請求運賃計算区分ID"].Index].Value);
                int SHRFlg = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["支払運賃計算区分ID"].Index].Value);
                int p計算区分 = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["請求運賃計算区分ID"].Index].Value);
                int p請求支払区分 = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["支払運賃計算区分ID"].Index].Value);
                decimal p数量 = Convert.ToDecimal(row.Cells[this.sp売上明細データ.Columns["数量"].Index].Value);
                decimal p重量 = Convert.ToDecimal(row.Cells[this.sp売上明細データ.Columns["重量"].Index].Value);
                decimal p売上単価 = Convert.ToDecimal(row.Cells[this.sp売上明細データ.Columns["売上単価"].Index].Value);
                decimal p支払単価 = Convert.ToDecimal(row.Cells[this.sp売上明細データ.Columns["支払単価"].Index].Value);
                int p得意先ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["得意先ID"].Index].Value);
                int p支払先ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["支払先ID"].Index].Value);
                int p発地ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["発地ID"].Index].Value);
                int p着地ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["着地ID"].Index].Value);
                int p商品ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["商品ID"].Index].Value);
                int p走行ＫＭ = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["走行ＫＭ"].Index].Value);
                int p車種ID = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["車種ID"].Index].Value);
                p明細番号 = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["明細番号"].Index].Value);
                p明細行 = Convert.ToInt32(row.Cells[this.sp売上明細データ.Columns["明細行"].Index].Value);



                string 項目名 = e.CellPosition.ColumnName;
                if (!String.IsNullOrEmpty(e.CellPosition.ColumnName))
                {
                    if (e.CellPosition.ColumnName[0].ToString() == "d")
                    {
                        項目名 = e.CellPosition.ColumnName.Substring(1);
                    };
                }

                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW2, row.Cells[colM.Index].Value, row.Cells[colL.Index].Value, 項目名, val));
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "入力内容が不正です。";
            }
        }

        #endregion

        #region 合計再計算
        //合計金額再計算
        public void Next_calculation()
        {
            売上金額 = 0;
            支払社内 = 0;
            請求割増１ = 0;
            請求割増２ = 0;
            通行料 = 0;
            売上合計 = 0;
            数量 = 0;
            重量 = 0;
            支払社内 = 0;
            支払通行料 = 0;
            支払合計 = 0;

            int Count = 0;

            売上金額 = 売上明細データ.Select(C => C.d売上金額).Sum();
            支払社内 = 売上明細データ.Select(C => C.d支払金額).Sum();
            請求割増１ = 売上明細データ.Select(C => C.d請求割増１).Sum();
            請求割増２ = 売上明細データ.Select(C => C.d請求割増２).Sum();
            通行料 = 売上明細データ.Select(C => C.d通行料).Sum();
            数量 = 売上明細データ.Select(C => C.数量).Sum();
            重量 = 売上明細データ.Select(C => C.重量).Sum();
            支払通行料 = 売上明細データ.Select(C => C.d支払通行料).Sum();

            //foreach (var Rows in sp売上明細データ.Rows)
            //{
            //	//売上金額 = 0;
            //	//支払社内 = 0;
            //	//請求割増１ = 0;
            //	//請求割増２ = 0;
            //	//通行料 = 0;
            //	//売上合計 = 0;
            //	//数量 = 0;
            //	//重量 = 0;
            //	//支払社内 = 0;
            //	//支払通行料 = 0;
            //	//支払合計 = 0;

            //	売上金額 += sp売上明細データ[Count, sp売上明細データ.Columns["d売上金額"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d売上金額"].Index].Value);
            //	支払社内 += sp売上明細データ[Count, sp売上明細データ.Columns["d支払金額"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d支払金額"].Index].Value);
            //	請求割増１ += sp売上明細データ[Count, sp売上明細データ.Columns["d請求割増１"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d請求割増１"].Index].Value);
            //	請求割増２ += sp売上明細データ[Count, sp売上明細データ.Columns["d請求割増２"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d請求割増２"].Index].Value);
            //	通行料 += sp売上明細データ[Count, sp売上明細データ.Columns["d通行料"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d通行料"].Index].Value);
            //	数量 += sp売上明細データ[Count, sp売上明細データ.Columns["数量"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["数量"].Index].Value);
            //	重量 += sp売上明細データ[Count, sp売上明細データ.Columns["重量"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["重量"].Index].Value);
            //	支払通行料 += sp売上明細データ[Count, sp売上明細データ.Columns["d支払通行料"].Index].Value == null ? 0 : Convert.ToDecimal(sp売上明細データ[Count, sp売上明細データ.Columns["d支払通行料"].Index].Value);

            //	Count++;
            //}
            売上合計 = 売上明細データ.Sum(C => C.d売上金額 + C.d請求割増１ + C.d請求割増２ + C.d通行料);
            支払合計 = 売上明細データ.Sum(C => C.d支払金額 + C.d支払金額);

        }
        #endregion

        #region ConvTextToInt

        int ConvTextToInt(string txt)
        {
            int ret = 0;
            if (string.IsNullOrWhiteSpace(txt) != true)
            {
                int.TryParse(txt, System.Globalization.NumberStyles.Currency, System.Globalization.NumberFormatInfo.CurrentInfo, out ret);
            }
            return ret;
        }

        #endregion

        #region spPickupList_SelectionChanged

        private void spPickupList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.PickupSwitch == true)
            {
                // 表示切替直後の時は何もしない。
                this.PickupSwitch = false;
                spPickupList.ActiveCellPosition = CellPosition.Empty;
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
            spPickupList.ActiveCellPosition = CellPosition.Empty;
        }

        #endregion

        #region 得意先指定_PreviewKeyDown

        private void 得意先指定_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                if (IsExpanded)
                {
                    txtbox支払先.Focus();
                }
                else
                {
                    btnKensaku.Focus();
                }

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

        #region 得意先指定_LostFocus

        private void 得意先指定_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #region 締日入力

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

        #endregion

        #region PickUpSime_PreviewKeyDown

        private void PickUpSime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                btnKensaku.Focus();
            }
        }

        #endregion

        #region 得意先指定_TextChanged

        private void 得意先指定_TextChanged(object sender, RoutedEventArgs e)
        {
            this.請求内訳ID = string.Empty;
        }

        #endregion

        #region Window_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
        {
            this.sp売上明細データ.InputBindings.Clear();
            this.spPickupList.InputBindings.Clear();
            this.売上明細データ = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigDLY10010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.表示順 = this.表示順;
                frmcfg.表示順方向 = this.表示順方向;
                frmcfg.自社部門index = this.cmb部門指定.SelectedIndex;
                frmcfg.区分1 = this.cmb検索日付種類.SelectedIndex;
                frmcfg.集計期間From = this.検索日付From;
                frmcfg.集計期間To = this.検索日付To;

                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.sp売上明細データ);

                ucfg.SetConfigValue(frmcfg);
            }
        }
        #endregion

        #region pickup_Expanded

        private void pickup_Expanded(object sender, RoutedEventArgs e)
        {
            PickupSelect(this.ピックアップ種類);

        }

        #endregion

        #region ColumnResert_Click

        private void ColumnResert_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.sp売上明細データ, this.sp_Config, "売上明細データ");

            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "売上未定区分", "日次", "運転日報入力", "未定区分", false);
            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "確認名称区分", "マスタ", "基礎情報設定", "確認名称", false);
            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "社内区分", "日次", "運転日報入力", "社内区分", false);
            AppCommon.SetutpComboboxListToCell(this.sp売上明細データ, "請求税区分", "日次", "運転日報入力", "税区分", false);

            DataReSort();
            textbox検索日付From.Focus();
            this.表示固定列数 = this.sp売上明細データ.FrozenColumnCount;


            売上金額 = 0;
            請求割増１ = 0;
            請求割増２ = 0;
            通行料 = 0;
            売上合計 = 0;
            重量 = 0;
            数量 = 0;
            支払社内 = 0;
            支払通行料 = 0;
            支払合計 = 0;
        }

        private void ColumnNum_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                this.SetFocusToTopControl();
            }
        }

        #endregion

        #region SetupSpreadFixedColumn

        private void SetupSpreadFixedColumn(GcSpreadGrid gcsp, int colNum)
        {
            if (colNum < 1)
            {
                return;
            }
            gcsp.FrozenColumnCount = colNum;
        }

        #endregion

        #region DisplayDetail

        private void DisplayDetail()
        {
            int rowNo = this.sp売上明細データ.ActiveCellPosition.Row;
            var row = this.sp売上明細データ.Rows[rowNo];
            string 入力区分 = sp売上明細データ[rowNo, sp売上明細データ.Columns["入力区分"].Index].Value.ToString();
            var mNo = row.Cells[sp売上明細データ.Columns["明細番号"].Index].Value;
            var gNo = row.Cells[sp売上明細データ.Columns["明細行"].Index].Value;
            if (入力区分 == "運転日報")
            {
                DLY01010 frm = new DLY01010();
                frm.初期明細番号 = (int?)mNo;
                frm.初期行番号 = (int?)gNo;
                frm.ShowDialog(this);
                if (frm.IsUpdated)
                {
                    // 日報側で更新された場合、再検索を実行する
                    this.Button_Click_1(null, null);
                }
            }
			else if (入力区分 == "売上入力")
            {
                DLY02015 frm = new DLY02015();
                frm.初期明細番号 = (int?)mNo;
                frm.初期行番号 = (int?)gNo;
                frm.ShowDialog(this);
                if (frm.IsUpdated)
                {
                    // 日報側で更新された場合、再検索を実行する
                    this.Button_Click_1(null, null);
                }
            }
			else if (入力区分 == "内訳入力")
			{
				DLY02010 frm = new DLY02010();
				frm.初期明細番号 = (int?)mNo;
				frm.初期行番号 = (int?)gNo;
				frm.ShowDialog(this);
				if (frm.IsUpdated)
				{
					// 日報側で更新された場合、再検索を実行する
					this.Button_Click_1(null, null);
				}
			}
        }

        #endregion

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (sp売上明細データ.ActiveCell != null && sp売上明細データ.ActiveCell.IsEditing)
			{
				CloseFlg = true;
				sp売上明細データ.CommitCellEdit();
				if (CloseFlg) { e.Cancel = true; }
				return;
			}		
		}

		private void sp売上明細データ_CellBeginEdit(object sender, SpreadCellBeginEditEventArgs e)
		{
			EditFlg = true;
		}

		/// <summary>						
		/// sp売上明細データ_EditElementShowing						
		/// スプレッドコンボイベント関連付け						
		/// デザイン画面でイベント追加						
		/// </summary>						
		/// <param name="sender"></param>						
		/// <param name="e"></param>						
		void sp売上明細データ_EditElementShowing(object sender, EditElementShowingEventArgs e)
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

		private void checkEdit_Checked(object sender, RoutedEventArgs e)
		{
			sp売上明細データ.CommitCellEdit();
		}

		private void checkEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			sp売上明細データ.CommitCellEdit();
		}

		private void sp売上明細データ_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
		{
			if (sp売上明細データ.Columns[1].Name == null)
			{
				return;
			}
			if (sp売上明細データ.Rows.Count() > 0)
			{
				Summary();
			}
		}			




    }
}
