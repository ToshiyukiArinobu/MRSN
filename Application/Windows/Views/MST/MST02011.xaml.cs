using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using GrapeCity.Windows.SpreadGrid;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 品番マスタ一括修正
    /// </summary>
    public partial class MST02011 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>
        /// <summary>
        /// データグリッドの新列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            自社色 = 1,
            商品形態分類 = 2,
            商品分類 = 3,
            大分類 = 4,
            中分類 = 5,
            ブランド = 6,
            シリーズ = 7,
            品群 = 8,
            自社品名 = 9,
            単位 = 10,
            原価 = 11,
            加工原価 = 12,
            卸値 = 13,
            売価 = 14,
            掛率 = 15,
            消費税区分 = 16,
            備考１ = 17,
            備考２ = 18,
            返却可能期限 = 19,
            ＪＡＮコード = 20,
        }

        /// <summary>
        /// コンボボックス用
        /// </summary>
        public class ComboBoxClass
        {
            public int コード { get; set; }
            public string 名称 { get; set; }

        }
        #endregion

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
        public class ConfigMST02011 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigMST02011 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string MST02011_GetData = "MST02011_GetData";
        private const string MST02011_Update = "MST02011_Update";
        private const string MST02011_GetMasterDataSet = "MST02011_GetMasterDataSet";


        //商品分類コンボ用
        private ComboBoxClass[] _ShouhinBunrui
            = { 
				  new ComboBoxClass() { コード = 0, 名称 = "", },
				  new ComboBoxClass() { コード = 1, 名称 = "食品", },
				  new ComboBoxClass() { コード = 2, 名称 = "繊維", },
				  new ComboBoxClass() { コード = 3, 名称 = "その他", },
			  };
        public ComboBoxClass[] ShouhinBunrui
        {
            get { return _ShouhinBunrui; }
            set { _ShouhinBunrui = value; NotifyPropertyChanged(); }
        }

        //商品形態コンボ用
        private ComboBoxClass[] _ShouhinKeitai
            = { 
				  new ComboBoxClass() { コード = 0, 名称 = "", },
				  new ComboBoxClass() { コード = 1, 名称 = "SET品", },
				  new ComboBoxClass() { コード = 2, 名称 = "資材・単品", },
				  new ComboBoxClass() { コード = 3, 名称 = "雑コード", },
				  new ComboBoxClass() { コード = 4, 名称 = "副資材", },
			  };
        public ComboBoxClass[] ShouhinKeitai
        {
            get { return _ShouhinKeitai; }
            set { _ShouhinKeitai = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region バインディングプロパティ

        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set { this._SearchResult = value; NotifyPropertyChanged(); }

        }

        public DataSet _MasterDataSet;
        public DataSet MasterDataSet
        {
            get { return this._MasterDataSet; }
            set { this._MasterDataSet = value; NotifyPropertyChanged(); }

        }

        private string _自社品名 = string.Empty;
        public string 自社品名
        {
            get { return this._自社品名; }
            set { this._自社品名 = value; NotifyPropertyChanged(); }
        }

        private int _商品分類 = 0;
        public int 商品分類
        {
            get { return this._商品分類; }
            set { this._商品分類 = value; NotifyPropertyChanged(); }
        }

        private int _商品形態 = 0;
        public int 商品形態
        {
            get { return this._商品形態; }
            set { this._商品形態 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region<< 仕入データ問合せ 初期処理群 >>

        /// <summary>
        /// 仕入データ問合せ コンストラクタ
        /// </summary>
        public MST02011()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            base.SendRequest(new CommunicationObject(MessageType.RequestData, MST02011_GetMasterDataSet, null));

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigMST02011)ucfg.GetConfigValue(typeof(ConfigMST02011));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST02011();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            ScreenClear();

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // コントロールの初期設定をおこなう
            //initSearchControl();

            spGridList.RowCount = 0;

            SetFocusToTopControl();
            ErrorMessage = "";

        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            this.spGridList.ItemsSource = null;
            this.spGridList.RowCount = 0;

            if (SearchResult != null)
            {
                SearchResult.Clear();
            }
            SearchResult = null;

            自社品名 = string.Empty;
            cmb_商品分類.SelectedIndex = 0;
            cmb_商品形態.SelectedIndex = 0;
            

            ResetAllValidation();

        }
        #endregion

        #region << 送信データ受信 >>
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
                case MST02011_GetMasterDataSet:
                    if (data is DataSet)
                    {
                        MasterDataSet = data as DataSet;
                    }
                    break;
                case MST02011_GetData :
                    base.SetFreeForInput();
                    if (tbl.Rows.Count > 0)
                    {
                        SearchResult = tbl;
                        SetData(tbl);
                        spGridList.ShowRow(0);
                    }

                    break;
                    
                case MST02011_Update:
                    base.SetFreeForInput();
                    if ((int)data == 1)
                    {
                        MessageBox.Show("更新完了しました。");
                    }
                    else
                    {
                        MessageBox.Show("更新に失敗しました。");
                        return;
                    }

                    ScreenClear();
                    break;
            }
        }

        /// <summary>
        /// 受信エラー時の処理をおこなう
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region << リボン >>

        #region F09 登録
        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            try
            {

                if (SearchResult == null)
                    return;

                if (UpdateValidataCheck() == false)
                {
                    return;
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(SearchResult);

                base.SendRequest(
                    new CommunicationObject(MessageType.UpdateData, MST02011_Update, new object[]{
                            ds,
                            ccfg.ユーザID,
                        }));
                base.SetBusyForInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
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
            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

        }
        #endregion

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

        /// <summary>
        /// 更新前データチェック
        /// </summary>
        /// <returns></returns>
        private bool UpdateValidataCheck()
        {
            //バイト数検索用
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int bytenum;
            int iData;
            decimal dData;
            for (int i = 0; i < SearchResult.Rows.Count; i++)
            {
                DataRow drData = SearchResult.Rows[i];
                //品番コード	int	Yes
                //自社品番	varchar(12)	
                bytenum = sjisEnc.GetByteCount(drData["自社品番"].ToString());
                if (bytenum > 12)
                {
                    this.ErrorMessage = (i+1).ToString() + "行目の自社品番が12byteを超えています。";
                    return false;
                }
                //自社色	varchar(3)
                if (string.IsNullOrEmpty(drData["自社色"].ToString()) == false)
                {
                    DataTable dtM06 = MasterDataSet.Tables["M06List"];

                    DataRow[] drlist = dtM06.Select("色コード = '" + drData["自社色"].ToString() +"'");

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の自社色はマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                //商品形態分類	int
                if (int.TryParse(drData["商品形態分類"].ToString(), out iData))
                {
                    if (iData > 4 || iData <= 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の商品形態分類を1～4でないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の商品形態分類を1～4でないデータがセットされています。";
                    return false;
                }
                //商品分類	int	
                if (int.TryParse(drData["商品分類"].ToString(), out iData))
                {
                    if (iData > 3 || iData <= 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の商品分類を1～3でないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の商品分類を1～3でないデータがセットされています。";
                    return false;
                }
                //大分類	int
                if (string.IsNullOrEmpty(drData["大分類"].ToString()) == false)
                {
                    DataTable dtM12 = MasterDataSet.Tables["M12List"];

                    DataRow[] drlist = dtM12.Select("大分類コード = " + drData["大分類"].ToString());

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の大分類はマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の大分類のデータがセットされていません。";
                    return false;
                }
                //中分類	int	
                if (string.IsNullOrEmpty(drData["中分類"].ToString()) == false)
                {
                    DataTable dtM13 = MasterDataSet.Tables["M13List"];

                    DataRow[] drlist = dtM13.Select("中分類コード = " + drData["中分類"].ToString());

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の中分類はマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の中分類のデータがセットされていません。";
                    return false;
                }
                //ブランド	varchar(3)
                if (string.IsNullOrEmpty(drData["ブランド"].ToString()) == false)
                {
                    DataTable dtM14 = MasterDataSet.Tables["M14List"];

                    DataRow[] drlist = dtM14.Select("ブランドコード = '" + drData["ブランド"].ToString() + "'");

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目のブランドはマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のブランドのデータがセットされていません。";
                    return false;
                }
                //シリーズ	varchar(3)	
                if (string.IsNullOrEmpty(drData["シリーズ"].ToString()) == false)
                {
                    DataTable dtM15 = MasterDataSet.Tables["M15List"];

                    DataRow[] drlist = dtM15.Select("シリーズコード = " + drData["シリーズ"].ToString());

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目のシリーズはマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のシリーズのデータがセットされていません。";
                    return false;
                }
                //品群	varchar(3)	
                if (string.IsNullOrEmpty(drData["品群"].ToString()) == false)
                {
                    DataTable dtM16 = MasterDataSet.Tables["M16List"];

                    DataRow[] drlist = dtM16.Select("品群コード = " + drData["品群"].ToString());

                    if (drlist.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の品群はマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の品群のデータがセットされていません。";
                    return false;
                }
                //自社品名	varchar(50)	
                bytenum = sjisEnc.GetByteCount(drData["自社品名"].ToString());
                if (bytenum > 50)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の自社品名が50byteを超えています。";
                    return false;
                }
                //単位	varchar(10)	
                bytenum = sjisEnc.GetByteCount(drData["単位"].ToString());
                if (bytenum > 10)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の単位が10byteを超えています。";
                    return false;
                }
                //原価	decimal(9, 2)	
                if (string.IsNullOrEmpty(drData["原価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["原価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の原価が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //加工原価	decimal(9, 2)
                if (string.IsNullOrEmpty(drData["加工原価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["加工原価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の加工原価が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //卸値	decimal(9, 2)	
                if (string.IsNullOrEmpty(drData["卸値"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["卸値"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の卸値が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //売価	decimal(9, 2)
                if (string.IsNullOrEmpty(drData["売価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["売価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の売価が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //掛率	decimal(4, 1)
                if (string.IsNullOrEmpty(drData["掛率"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["掛率"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の掛率が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //消費税区分	int	
                if (int.TryParse(drData["消費税区分"].ToString(), out iData))
                {
                    if (iData > 2)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の消費税区分を0～2でないデータがセットされています。";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の消費税区分を0～2でないデータがセットされています。";
                    return false;
                }
                //備考１	varchar(32)	
                bytenum = sjisEnc.GetByteCount(drData["備考１"].ToString());
                if (bytenum > 32)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の備考１が32byteを超えています。";
                    return false;
                }
                //備考２	varchar(32)	
                bytenum = sjisEnc.GetByteCount(drData["備考２"].ToString());
                if (bytenum > 32)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目の備考２が32byteを超えています。";
                    return false;
                }
                //返却可能期限	int
                if(string.IsNullOrEmpty(drData["返却可能期限"].ToString()) == false)
                {
                    if (int.TryParse(drData["返却可能期限"].ToString(), out iData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の返却可能期限が数値でないデータがセットされています。";
                        return false;
                    }
                }
                //ＪＡＮコード	varchar(13)	
                bytenum = sjisEnc.GetByteCount(drData["ＪＡＮコード"].ToString());
                if (bytenum > 13)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のＪＡＮコードが13byteを超えています。";
                    return false;
                }
            }

            return true;
        }

        #region 一覧検索処理

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

                //setSearchParams();

                base.SendRequest(
                    new CommunicationObject(MessageType.RequestData,MST02011_GetData,new object[]
                        {
                            自社品名
                            ,商品分類
                            ,商品形態
                        }));

                base.SetBusyForInput();

            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region << KeyDown Events >>

        /// <summary>
        /// コントロールでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    Keyboard.Focus(this.btnSearch);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }

            }
        }

        /// <summary>
        /// スプレッドグリッドでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && spGridList.EditElement == null)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
            {
                e.Handled = true;
            }

        }

        #endregion

        #region Window_Closed

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.spGridList.InputBindings.Clear();
            this.SearchResult = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigMST02011(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.spGridList);

                ucfg.SetConfigValue(frmcfg);
            }

        }

        #endregion

        #region 取得データをセット
        private void SetData(DataTable tbl)
        {

            int iSpdRowIndex = 0;

            spGridList.InputBindings.Clear();
            spGridList.RowCount = iSpdRowIndex;

            for (int row = 0; row <= tbl.Rows.Count - 1; row++)
            {
                //ここでグリッドに表示する

                spGridList.Rows.AddNew();

                ////自社品番
                //spGridList[iSpdRowIndex, GridColumnsMapping.自社品番.GetHashCode()].Value = tbl.Rows[row]["自社品番"].ToString();
                ////色
                //spGridList[iSpdRowIndex, GridColumnsMapping.色.GetHashCode()].Value = tbl.Rows[row]["色"].ToString();
                ////自社品名
                //spGridList[iSpdRowIndex, GridColumnsMapping.自社品名.GetHashCode()].Value = tbl.Rows[row]["自社品名"].ToString();
                ////原価
                //spGridList[iSpdRowIndex, GridColumnsMapping.原価.GetHashCode()].Value = tbl.Rows[row]["原価"].ToString();
                ////加工原価
                //spGridList[iSpdRowIndex, GridColumnsMapping.加工原価.GetHashCode()].Value = tbl.Rows[row]["加工原価"].ToString();
                ////卸値
                //spGridList[iSpdRowIndex, GridColumnsMapping.卸値.GetHashCode()].Value = tbl.Rows[row]["卸値"].ToString();
                ////売価
                //spGridList[iSpdRowIndex, GridColumnsMapping.売価.GetHashCode()].Value = tbl.Rows[row]["売価"].ToString();
                ////掛率
                //spGridList[iSpdRowIndex, GridColumnsMapping.掛率.GetHashCode()].Value = tbl.Rows[row]["掛率"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社品番.GetHashCode()].Value = tbl.Rows[row]["自社品番"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社色.GetHashCode()].Value = tbl.Rows[row]["自社色"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.商品形態分類.GetHashCode()].Value = tbl.Rows[row]["商品形態分類"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.商品分類.GetHashCode()].Value = tbl.Rows[row]["商品分類"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.大分類.GetHashCode()].Value = tbl.Rows[row]["大分類"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.中分類.GetHashCode()].Value = tbl.Rows[row]["中分類"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.ブランド.GetHashCode()].Value = tbl.Rows[row]["ブランド"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.シリーズ.GetHashCode()].Value = tbl.Rows[row]["シリーズ"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.品群.GetHashCode()].Value = tbl.Rows[row]["品群"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社品名.GetHashCode()].Value = tbl.Rows[row]["自社品名"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.単位.GetHashCode()].Value = tbl.Rows[row]["単位"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.原価.GetHashCode()].Value = tbl.Rows[row]["原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.加工原価.GetHashCode()].Value = tbl.Rows[row]["加工原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.卸値.GetHashCode()].Value = tbl.Rows[row]["卸値"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.売価.GetHashCode()].Value = tbl.Rows[row]["売価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.掛率.GetHashCode()].Value = tbl.Rows[row]["掛率"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.消費税区分.GetHashCode()].Value = tbl.Rows[row]["消費税区分"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.備考１.GetHashCode()].Value = tbl.Rows[row]["備考１"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.備考２.GetHashCode()].Value = tbl.Rows[row]["備考２"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.返却可能期限.GetHashCode()].Value = tbl.Rows[row]["返却可能期限"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.ＪＡＮコード.GetHashCode()].Value = tbl.Rows[row]["ＪＡＮコード"].ToString();


                //スプレッド行インデックスインクリメント
                iSpdRowIndex = iSpdRowIndex + 1;

            }

        }
        #endregion

        #region << 機能処理群 >>


       

        /// <summary>
        /// セル編集コミット時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            //明細行が存在しない場合は処理しない
            if (SearchResult == null) return;

            Row targetRow = grid.Rows[grid.ActiveRowIndex];

            //編集したセルの値を取得
            var CellValue = grid[grid.ActiveRowIndex,targetColumn].Value;

            if (CellValue != null)
            {
                SearchResult.Rows[targetRow.Index][targetColumn] = CellValue;
            }
            //SearchResult.Rows[targetRow.Index].EndEdit();
            //SearchResult.AcceptChanges();
        }

        

       

        #endregion

        /// <summary>
        /// CSVOutputボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSVOutPut_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResult == null)
            {
                ErrorMessage = "検索を行ってから出力ボタンを押下してください。";
                return;
            }
            WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            // 「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == WinForms.DialogResult.OK)
            {
                // CSVファイル出力
                CSVData.SaveCSV(SearchResult, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }

        /// <summary>
        /// CSVInputボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSVInPut_Click(object sender, RoutedEventArgs e)
        {
            string selectFile = string.Empty;

            try
            {
                //ファイル選択ダイアログ表示
                selectFile = SelectCsvFile();
                if (selectFile == String.Empty)
                {
                 return;
                }

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
            
            DataTable tbl = CSVData.ReadCsv(selectFile, ",", true, true, true);
            SearchResult = tbl;
            SearchResult.TableName = MST02011_GetData;
            SetData(tbl);
            MessageBox.Show("CSVファイルの内容を表に表示しました。");
        }

        /// <summary>
        /// ファイル選択ダイアログ表示(CSVファイル用)
        /// </summary>
        /// <param name="title">ダイアログタイトル</param>
        /// <param name="initFileName">初期表示ファイル名(フルパス)</param>
        /// <returns></returns>
        private string SelectCsvFile()
        {
            //OpenFileDialogクラスのインスタンスを作成
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            string folder = string.Empty, fileName = string.Empty;
            
            //はじめのファイル名を指定する
            ofd.FileName = fileName;

            //はじめに表示されるフォルダを指定する
            ofd.InitialDirectory = @folder;

            //[ファイルの種類]に表示される選択肢を指定する
            ofd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]にはじめに表示するものを指定する
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "取込ファイルを選択してください。";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return ofd.FileName;
            else
                return string.Empty;
        }

    }

}
