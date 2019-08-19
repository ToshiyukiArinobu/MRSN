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
using System.Windows.Threading;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 取引先マスターメンテ
    /// </summary>
	public partial class MST01010 : WindowMasterMainteBase
	{
        #region 権限関係
        public UserConfig ucfg = null;
        CommonConfig ccfg = null;
        public class ConfigMST0101011 : FormConfigBase
		{
		}

        public ConfigMST0101011 frmcfg = null;
        #endregion

        #region 列挙型定義

        /// <summary>
        /// 取引区分
        /// </summary>
        private enum TradingCategory : int
        {
            /// <summary>得意先</summary>
            Customer = 0,
            /// <summary>仕入先</summary>
            Supplier = 1,
            /// <summary>加工先</summary>
            processing = 2,
            /// <summary>相殺先</summary>
            Offset = 3,
            /// <summary>販社</summary>
            SalesCompany = 4
        }

        /// <summary>
        /// 支払区分
        /// </summary>
        private enum PaymentKbn : int
        {
            /// <summary>請求</summary>
            Claim = 0,
            /// <summary>支払</summary>
            Payment = 1
        }

        /// <summary>
        /// データ取得オプション
        /// </summary>
        private enum SearchOption : int
        {
            /// <summary>先頭データ取得</summary>
            first = -2,
            /// <summary>前のデータ取得</summary>
            prev = -1,
            /// <summary>キー指定取得</summary>
            code = 0,
            /// <summary>次のデータ取得</summary>
            next = 1,
            /// <summary>最後のデータ取得</summary>
            last = 2
        }

        #endregion

        #region 定数定義

        /// <summary>取引先コードのフォーカス有無</summary>
        private bool isFocusedSupCode = false;

        /// <summary>取引先データ取得</summary>
        private const string M01_TOK_GetData = "M01_TOK_GetData";
        /// <summary>取引先データ登録</summary>
        private const string M01_TOK_Update = "M01_TOK_Update";
        /// <summary>取引先データ削除</summary>
        private const string M01_TOK_Delete = "M01_TOK_Delete";
        /// <summary>住所取得</summary>
        private const string GET_UcZip = "UcZIP";

        #endregion

        #region プロパティ定義

        private string _取引先コード = string.Empty;
		public string 取引先コード
		{
            get { return this._取引先コード; }
            set { this._取引先コード = value; NotifyPropertyChanged(); }
		}

        private string _枝番 = string.Empty;
        public string 枝番
        {
            get { return this._枝番; }
            set { this._枝番 = value; NotifyPropertyChanged(); }
        }

        private string _Ｔ担当者名 = string.Empty;
        public string Ｔ担当者名
        {
            get { return this._Ｔ担当者名; }
            set { this._Ｔ担当者名 = value; NotifyPropertyChanged(); }
        }

        private string _Ｓ担当者名 = string.Empty;
        public string Ｓ担当者名
        {
            get { return this._Ｓ担当者名; }
            set { this._Ｓ担当者名 = value; NotifyPropertyChanged(); }
        }

		private DataRow _rowM01tok = null;
		public DataRow RowM01TOK
		{
			get { return this._rowM01tok; }
			set
			{
				this._rowM01tok = value;
				if (value != null)
				{
					if (value.IsNull("取引先コード") != true)
					{
                        this.取引先コード = string.Format("{0}", value["取引先コード"]);
					}
				}
				try
				{
					NotifyPropertyChanged();
				}
				catch (Exception)
				{
					// 新規インスタンス時のみ例外が発生する
					// バインドには影響しないので特に処理必要なし
				}
			}
		}

        #endregion

        #region コントロール操作プロパティ

        private bool _claimEnabled;
        private bool isClaimEnabled
        {
            get { return _claimEnabled; }
            set
            {
                _claimEnabled = value;
                // 請求関連項目の操作
                changeControl(PaymentKbn.Claim);
            }

        }

        private bool _paymentEnabled;
        private bool isPaymentEnabled
        {
            get { return _paymentEnabled; }
            set
            {
                _paymentEnabled = value;
                // 支払関連項目の操作
                changeControl(PaymentKbn.Payment);
            }

        }

        /// <summary>支払条件のフォーカスイベント判定</summary>
        private bool isShiharaiLostFocused = false;

        #endregion

        #region << 初期表示処理 >>
        /// <summary>
		/// 取引先マスタメンテ
		/// </summary>
        public MST01010()
		{
            InitializeComponent();
			this.DataContext = this;
        }
        
        /// <summary>
		/// Loadイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 画面初期設定(権限設定等)
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST0101011)ucfg.GetConfigValue(typeof(ConfigMST0101011));

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg == null)
			{
				frmcfg = new ConfigMST0101011();
				// 画面サイズをタスクバーをのぞいた状態で表示させる
				//this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Top;
			}
			else
			{
				this.Top = frmcfg.Top;
				this.Left = frmcfg.Left;
				this.Height = frmcfg.Height;
				this.Width = frmcfg.Width;
            }

            #endregion

            // コンボボックスデータ生成
            AppCommon.SetutpComboboxList(this.T_SHIHARAI_KBN, false);
            AppCommon.SetutpComboboxList(this.S_SHIHARAI_KBN, false);

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { typeof(MST23010), typeof(SCHM72_TNT) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });

            this.T_SHIHARAI.PreviewLostKeyboardFocus += SHIHARAI_LostFocus;
            this.S_SHIHARAI.PreviewLostKeyboardFocus += SHIHARAI_LostFocus;
            
            ScreenClear();
			ChangeKeyItemChangeable(true);
			SetFocusToTopControl();

        }

        #endregion

        #region リボン

        #region F01 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    	public override void OnF1Key(object sender, KeyEventArgs e)
		{
			try
			{
                object elmnt = FocusManager.GetFocusedElement(this);
                var tokBox = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (tokBox != null)
                {
                    // 取引先テキストの場合
                    tokBox.OpenSearchWindow(this);

                }
                else if (isFocusedSupCode)
                {
                    // 取引先の場合、別テキストに枝番設定の為独自処理
                    SCHM01_TOK di = new SCHM01_TOK();
                    di.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();

                    if (di.ShowDialog(this) == true)
                    {
                        this.TORI_CODE.Text = di.TwinTextBox.Text1;
                        this.TORI_EDA.Text = di.TwinTextBox.Text2;

                        SearchSupplierData(int.Parse(取引先コード), int.Parse(枝番));

                    }

                }
                else
                {
                    // 取引先以外はFW標準で開く
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }

            }
			catch (Exception ex)
			{
				appLog.Error("検索画面起動エラー", ex);
				ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}

        }
        #endregion

        #region F09 登録
        /// <summary>
		/// F9　リボン　登録
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
			var yesno =
                MessageBox.Show(
                        "入力を取り消しますか？",
                        "取消確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question,
                        MessageBoxResult.No);

            if (yesno == MessageBoxResult.No)
				return;

			ScreenClear();

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
			ucfg.SetConfigValue(frmcfg);
			this.Close();

        }
        #endregion

        #region F12 削除
        /// <summary>
		/// F12　リボン　削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF12Key(object sender, KeyEventArgs e)
		{

            if (string.IsNullOrEmpty(this.取引先コード))
			{
				this.ErrorMessage = "登録内容がありません。";
				MessageBox.Show("登録内容がありません。");
				return;
			}

			if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
			{
				this.ErrorMessage = "新規入力データは削除できません。";
				MessageBox.Show("新規入力データは削除できません。");
				return;
			}

			MessageBoxResult result =
                MessageBox.Show(
                        "データを削除しても宜しいですか？",
                        "確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question,
                        MessageBoxResult.No);

			if (result == MessageBoxResult.Yes)
				Delete();

		}
        #endregion

        #endregion

        #region データ受信

        /// <summary>
        /// データ受信処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
		{
			base.OnReceivedResponseData(message);

            try
            {
                this.ErrorMessage = string.Empty;

                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case M01_TOK_GetData:
                        #region 取引先検索
                        if (tbl == null || tbl.Rows.Count == 0)
                        {
                            // 新規入力
                            RowM01TOK = tbl.NewRow();
                            RowM01TOK["取引先コード"] = this.取引先コード;
                            RowM01TOK["枝番"] = this.枝番;
                            RowM01TOK["取引区分"] = (int)TradingCategory.Customer;

                            tbl.Rows.Add(RowM01TOK);
                            NotifyPropertyChanged("RowM01TOK");
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                        }
                        else
                        {
                            // 変更入力
                            DataRow row = tbl.Rows[0];

                            if (row["削除日時"] == null)
                            {
                                string msg = "既に削除されているデータです。";
                                this.ErrorMessage = msg;
                                MessageBox.Show(msg);
                                ScreenClear();
                                return;
                            }

                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                        }

                        RowM01TOK = tbl.Rows[0];
                        ChangeKeyItemChangeable(false);
                        SetFocusToTopControl();
                        ResetAllValidation();

                        // 取引区分によるコントロール制御
                        TORI_KBN_TargetUpdated(this.M01_TORI_KBN, null);

                        #endregion
                        break;

                    case M01_TOK_Update:
                        MessageBox.Show("登録が完了しました。");
                        ScreenClear();
                        break;

                    case M01_TOK_Delete:
                        MessageBox.Show("削除が完了しました。");
                        ScreenClear();
                        break;

                    case GET_UcZip:
                        // 住所情報取得
                        ShowAddresList(tbl);
                        break;

                }

            }
            catch (Exception ex)
            {
                RowM01TOK = null;
                appLog.Error("受信データ処理例外発生しました。", ex);
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";

            }

        }

        /// <summary>
        /// 受信エラー時の処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
        }

        #endregion

        #region 取引区分変更関連処理

        /// <summary>
        /// コントロール変更ポータル
        /// </summary>
        /// <param name="kbn">支払区分</param>
        private void changeControl(PaymentKbn kbn)
        {
            changeControlEnabled(kbn);
            clearControlText(kbn);

        }

        /// <summary>
        /// 対象コントロールの編集可否を設定する
        /// </summary>
        /// <param name="kbn">支払区分</param>
        private void changeControlEnabled(PaymentKbn kbn)
        {
            bool isReadOnly = (kbn ==  PaymentKbn.Claim) ? !_claimEnabled : !_paymentEnabled;
            
            switch (kbn)
            {
                case PaymentKbn.Claim:
                    #region 請求項目
                    this.T_YOSHIN.cIsReadOnly = isReadOnly;
                    this.T_TAX_KBN.IsEnabled = _claimEnabled;
                    this.T_TAX_TANI.IsEnabled = _claimEnabled;
                    this.T_SHIME_DT.cIsReadOnly = isReadOnly;
                    this.T_SHIHARAI.cIsReadOnly = isReadOnly;
                    this.T_SHIHARAI_KBN.Combo_IsEnabled = _claimEnabled;
                    this.T_SITE1.IsEnabled = _claimEnabled;
                    this.T_SITE2.IsEnabled = _claimEnabled;
                    this.T_NYUKIN_DT1.cIsReadOnly = isReadOnly;
                    this.T_NYUKIN_DT2.cIsReadOnly = isReadOnly;
                    this.T_TANTO.Text1IsReadOnly = isReadOnly;

                    // フォーカス無効
                    this.T_YOSHIN.Focusable = _claimEnabled;
                    this.T_TAX_KBN.Focusable = _claimEnabled;
                    this.T_TAX_TANI.Focusable = _claimEnabled;
                    this.T_SHIME_DT.Focusable = _claimEnabled;
                    this.T_SHIHARAI.Focusable = _claimEnabled;
                    this.T_SHIHARAI_KBN.Focusable = _claimEnabled;
                    this.T_SITE1.Focusable = _claimEnabled;
                    this.T_SITE2.Focusable = _claimEnabled;
                    this.T_NYUKIN_DT1.Focusable = _claimEnabled;
                    this.T_NYUKIN_DT2.Focusable = _claimEnabled;
                    this.T_TANTO.Focusable = _claimEnabled;

                    #endregion

                    break;

                case PaymentKbn.Payment:
                    #region 支払項目
                    this.S_TAX_KBN.IsEnabled = _paymentEnabled;
                    this.S_TAX_TANI.IsEnabled = _paymentEnabled;
                    this.S_SHIME_DT.cIsReadOnly = isReadOnly;
                    this.S_SHIHARAI.cIsReadOnly = isReadOnly;
                    this.S_SHIHARAI_KBN.Combo_IsEnabled = _paymentEnabled;
                    this.S_SITE1.IsEnabled = _paymentEnabled;
                    this.S_SITE2.IsEnabled = _paymentEnabled;
                    this.S_NYUKIN_DT1.cIsReadOnly = isReadOnly;
                    this.S_NYUKIN_DT2.cIsReadOnly = isReadOnly;
                    this.S_TANTO.Text1IsReadOnly = isReadOnly;

                    // 支払項目
                    this.S_TAX_KBN.Focusable = _paymentEnabled;
                    this.S_TAX_TANI.Focusable = _paymentEnabled;
                    this.S_SHIME_DT.Focusable = _paymentEnabled;
                    this.S_SHIHARAI.Focusable = _paymentEnabled;
                    this.S_SHIHARAI_KBN.Focusable = _paymentEnabled;
                    this.S_SITE1.Focusable = _paymentEnabled;
                    this.S_SITE2.Focusable = _paymentEnabled;
                    this.S_NYUKIN_DT1.Focusable = _paymentEnabled;
                    this.S_NYUKIN_DT2.Focusable = _paymentEnabled;
                    this.S_TANTO.Focusable = _paymentEnabled;

                    #endregion

                    break;

                default:
                    break;

            }

            // REMARKS:FW共通処理で変更されるので常に再設定
            this.ADD_STAFF.Focusable = false;
            this.ADD_DT.Focusable = false;
            this.UPD_STAFF.Focusable = false;
            this.UPD_DT.Focusable = false;
            this.DEL_STAFF.Focusable = false;
            this.DEL_DT.Focusable = false;

        }

        /// <summary>
        /// 対象コントロールのテキストクリアをおこなう
        /// </summary>
        /// <param name="kbn"></param>
        private void clearControlText(PaymentKbn kbn)
        {
            bool isEnabled = (kbn == PaymentKbn.Claim) ? _claimEnabled : _paymentEnabled;

            // コントロールが有効となる場合は処理しない
            if (isEnabled)
                return;

            switch (kbn)
            {
                case PaymentKbn.Claim:
                    #region 請求項目
                    this.T_YOSHIN.Text = null;
                    this.T_TAX_KBN.Text = null;
                    this.T_TAX_TANI.Text = null;
                    this.T_SHIME_DT.Text = null;
                    this.T_SHIHARAI.Text = null;
                    this.T_SHIHARAI_KBN.SelectedValue = 1;
                    this.T_SITE1.Text = null;
                    this.T_SITE2.Text = null;
                    this.T_NYUKIN_DT1.Text = null;
                    this.T_NYUKIN_DT2.Text = null;
                    this.T_TANTO.Text1 = null;
                    this.T_TANTO.Text2 = string.Empty;
                    #endregion

                    break;

                case PaymentKbn.Payment:
                    #region 支払項目
                    this.S_TAX_KBN.Text = null;
                    this.S_TAX_TANI.Text = null;
                    this.S_SHIME_DT.Text = null;
                    this.S_SHIHARAI.Text = null;
                    this.S_SHIHARAI_KBN.SelectedValue = 1;
                    this.S_SITE1.Text = null;
                    this.S_SITE2.Text = null;
                    this.S_NYUKIN_DT1.Text = null;
                    this.S_NYUKIN_DT2.Text = null;
                    this.S_TANTO.Text1 = null;
                    this.S_TANTO.Text2 = string.Empty;
                    #endregion

                    break;

                default:
                    break;

            }

        }

        #endregion

        #region 処理ロジック群

        /// <summary>
        /// 画面項目の初期化処理
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            取引先コード = string.Empty;
            枝番 = string.Empty;
            Ｔ担当者名 = string.Empty;
            Ｓ担当者名 = string.Empty;
            BIKO_1.Text1 = string.Empty;
            BIKO_2.Text1 = string.Empty;
            RowM01TOK = null;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();
            SetFocusToTopControl();

        }

        /// <summary>
        /// 取引先データ取得をおこなう
        /// </summary>
        private void SearchSupplierData(int supCode, int supEda)
        {
            try
            {
                // 取引先マスタ
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        M01_TOK_GetData,
                        new object[] {
                            supCode,
                            supEda,
                            ccfg.自社コード,
                            (int)SearchOption.code
                        }));

            }
            catch { }

        }

        /// <summary>
        /// 住所情報取得
        /// </summary>
        /// <param name="tbl"></param>
        private void ShowAddresList(DataTable tbl)
        {
            foreach (DataRow dr in tbl.Rows)
            {
                // 取得データの１件目を対象として設定
                RowM01TOK["住所１"] = dr["住所漢字"];
                NotifyPropertyChanged("RowM01TOK");
                break;
            }

            // REMARKS:見つからなかった場合は何もしない

        }

        /// <summary>
        /// 支払条件によるフォーカス変更処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        private void SkipPaymentTerms(object sender)
        {
            // 入力されている値
            string price = ((UcLabelTextBox)sender).Text;

            // 入力値がある場合は処理しない
            if (!string.IsNullOrEmpty(price) || isShiharaiLostFocused)
                return;

            if (this.T_SHIHARAI.Equals(sender))
            {
                // 請求 支払条件の場合
                // 関連項目の初期化
                this.T_SHIHARAI_KBN.SelectedIndex = 0;
                this.T_SITE2.Text = string.Empty;
                this.T_NYUKIN_DT2.Text = string.Empty;

                // 担当者にフォーカスを設定
                isShiharaiLostFocused = true;
                this.T_TANTO.Focus();

            }
            else if (this.S_SHIHARAI.Equals(sender))
            {
                // 支払 支払条件の場合
                // 関連項目の初期化
                this.S_SHIHARAI_KBN.SelectedIndex = 0;
                this.S_SITE2.Text = string.Empty;
                this.S_NYUKIN_DT2.Text = string.Empty;

                // 担当者にフォーカスを設定
                isShiharaiLostFocused = true;
                this.S_TANTO.Focus();

            }

            isShiharaiLostFocused = false;

        }

        /// <summary>
        /// 更新処理をおこなう
        /// </summary>
		private void Update()
		{
            // 未検索時は処理しない
            if (RowM01TOK == null)
                return;

            try
			{
				if (!base.CheckAllValidation())
				{
                    string msg = "入力内容に誤りがあります。";
                    this.ErrorMessage = msg;
					MessageBox.Show(msg);

                    SetFocusToTopControl();
					return;

				}

                if (!CheckFormValidation())
                {
                    string msg = "入力内容に誤りがあります。";
                    //this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;

                }

                // 確認メッセージ表示
                var yesno = MessageBox.Show(
                                "入力内容を登録しますか？",
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
					return;

				base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        M01_TOK_Update,
                        new object[] {
                            RowM01TOK,
                            ccfg.ユーザID
                        }));

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

        }

        /// <summary>
        /// 削除処理をおこなう
        /// </summary>
		private void Delete()
		{
			try
			{
				var yesno = MessageBox.Show(
                                "データを削除しますか？",
                                "削除確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.No);
				if (yesno == MessageBoxResult.No)
					return;

				SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        M01_TOK_Delete,
                        RowM01TOK));

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);

            }

        }

        /// <summary>
        /// 業務入力チェックをおこない、結果を返す
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValidation()
        {
            if (string.IsNullOrEmpty(TORI_NM_1.Text))
            {
                ErrorMessage = "正式名称が入力されていません。";
                TORI_NM_1.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(JIS_COMPANY.Text1))
            {
                ErrorMessage = "担当会社コードが入力されていません。";
                JIS_COMPANY.SetFocus();
                return false;
            }

            if (isClaimEnabled)
            {
                #region 請求が有効な場合
                // -- 消費税区分
                if (string.IsNullOrEmpty(T_TAX_KBN.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "消費税区分が選択されていません。";
                    T_TAX_KBN.Focus();
                    return false;
                }

                // -- 消費税単位
                if (string.IsNullOrEmpty(T_TAX_TANI.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "消費税単位が選択されていません。";
                    T_TAX_TANI.Focus();
                    return false;
                }

                // -- 締日
                if (string.IsNullOrEmpty(T_SHIME_DT.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "締日が入力されていません。";
                    T_TAX_TANI.Focus();
                    return false;
                }
                else
                {
                    // 数値の範囲チェック
                    bool isTShimeResult = false;
                    int ival;
                    if (int.TryParse(T_SHIME_DT.Text, out ival))
                    {
                        if (ival >= 0 && ival <= 31)
                            isTShimeResult = true;

                    }

                    if (!isTShimeResult)
                    {
                        ErrorMessage = "締日の入力内容に誤りがあります。";
                        T_SHIME_DT.Focus();
                        return false;
                    }

                }

                // -- サイト１
                if (string.IsNullOrEmpty(T_SITE1.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "サイト１が入力されていません。";
                    T_SITE1.Focus();
                    return false;
                }

                // -- 入金日１
                if (string.IsNullOrEmpty(T_NYUKIN_DT1.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "入金日１が入力されていません。";
                    T_NYUKIN_DT1.Focus();
                    return false;
                }
                else
                {
                    // 数値の範囲チェック
                    bool isTNyukinResult = false;
                    int ival;
                    if (int.TryParse(T_SHIME_DT.Text, out ival))
                    {
                        if (ival >= 0 && ival <= 31)
                            isTNyukinResult = true;

                    }

                    if (!isTNyukinResult)
                    {
                        ErrorMessage = "入金日１の入力内容に誤りがあります。";
                        T_SHIME_DT.Focus();
                        return false;
                    }

                }

                #endregion

            }

            if (isPaymentEnabled)
            {
                #region 支払が有効な場合
                // -- 消費税区分
                if (string.IsNullOrEmpty(S_TAX_KBN.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "消費税区分が選択されていません。";
                    S_TAX_KBN.Focus();
                    return false;
                }

                // -- 消費税単位
                if (string.IsNullOrEmpty(S_TAX_TANI.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "消費税単位が選択されていません。";
                    S_TAX_TANI.Focus();
                    return false;
                }

                // -- 締日
                if (string.IsNullOrEmpty(S_SHIME_DT.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "締日が入力されていません。";
                    S_TAX_TANI.Focus();
                    return false;
                }
                else
                {
                    // 数値の範囲チェック
                    bool isSShimeResult = false;
                    int ival;
                    if (int.TryParse(S_SHIME_DT.Text, out ival))
                    {
                        if (ival >= 1 && ival <= 31)
                            isSShimeResult = true;

                    }

                    if (!isSShimeResult)
                    {
                        ErrorMessage = "締日の入力内容に誤りがあります。";
                        S_SHIME_DT.Focus();
                        return false;
                    }

                }

                // -- サイト１
                if (string.IsNullOrEmpty(S_SITE1.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "サイト１が入力されていません。";
                    S_SITE1.Focus();
                    return false;
                }

                // -- 入金日１
                if (string.IsNullOrEmpty(S_NYUKIN_DT1.Text))
                {
                    // 必須入力チェック
                    ErrorMessage = "入金日１が入力されていません。";
                    S_NYUKIN_DT1.Focus();
                    return false;
                }
                else
                {
                    // 数値の範囲チェック
                    bool isSNyukinResult = false;
                    int ival;
                    if (int.TryParse(S_SHIME_DT.Text, out ival))
                    {
                        if (ival >= 1 && ival <= 31)
                            isSNyukinResult = true;

                    }

                    if (!isSNyukinResult)
                    {
                        ErrorMessage = "入金日１の入力内容に誤りがあります。";
                        S_SHIME_DT.Focus();
                        return false;
                    }

                }

                #endregion

            }

            return true;

        }

        #endregion

        #region フォームイベント関連

        #region 郵便番号変更イベント
        /// <summary>
        /// 郵便番号変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YUBIN_NO_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            try
            {
                // 住所情報取得
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_UcZip,
                        new object[] {
                            RowM01TOK["郵便番号"]
                        }));

            }
            catch { }

        }
        #endregion

        #region 枝番キー押下イベント
        /// <summary>
        /// 枝番項目でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORI_EDA_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                int i取引先コード = 0;
                int i枝番 = 0;
                if (string.IsNullOrEmpty(取引先コード) || string.IsNullOrEmpty(枝番))
                    return;

                if (!int.TryParse(取引先コード, out i取引先コード) || !int.TryParse(枝番, out i枝番))
                {
                    this.ErrorMessage = "取引先コードの入力形式が不正です。";
                    return;
                }

                SearchSupplierData(i取引先コード, i枝番);

            }

        }
        #endregion

        #region 取引区分関連イベント群

        /// <summary>
        /// 取引区分が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORI_KBN_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (RowM01TOK == null)
                return;

            int kbn = AppCommon.IntParse(RowM01TOK["取引区分"].ToString());
            changeTradingCategorySet(kbn);

        }

        /// <summary>
        /// 取引区分(テキスト)が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M01_TORI_KBN_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TORI_KBN_TargetUpdated(sender, null);

        }

        /// <summary>
        /// 取引区分に応じた画面設定をおこなう
        /// </summary>
        /// <param name="kbn">取引区分</param>
        private void changeTradingCategorySet(int? kbn)
        {
            if (kbn == null)
                kbn = -1;   // nullの場合は範囲外値を設定

            #region 取引区分によるフラグ設定
            switch (kbn)
            {
                case (int)TradingCategory.Customer:
                    // 得意先
                    /* ******************************************* *
                     *  ・得意先を選択した場合                     *
　                   *    支払の条件設定を入力出来ないようにする   *
                     * ******************************************* */
                    isClaimEnabled = true;
                    isPaymentEnabled = false;
                    break;

                case (int)TradingCategory.Supplier:
                    // 仕入先
                case (int)TradingCategory.processing:
                    // 加工先
                    /* ******************************************* *
                     *  ※仕入先・加工先を選択した場合、           *
　                   *    請求の条件設定を入力出来ないようにする   *
                     * ******************************************* */
                    isClaimEnabled = false;
                    isPaymentEnabled = true;
                    break;

                case (int)TradingCategory.Offset:
                case (int)TradingCategory.SalesCompany:
                    // 相殺
                    /* ******************************************* *
                     *  ※「相殺」「販社」の場合は、               *
                     *     両方の条件を入力出来る様にする          *
                     * ******************************************* */
                    isClaimEnabled = true;
                    isPaymentEnabled = true;
                    break;

                default:
                    // nullの場合などは全て入力不可とする
                    isClaimEnabled = false;
                    isPaymentEnabled = false;
                    break;

            }
            #endregion

        }

        #endregion

        #region 取引先コードのフォーカスイベント群

        /// <summary>
        /// 取引先コードフォーカス取得時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supCode_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocusedSupCode = true;
        }

        /// <summary>
        /// 取引先コードフォーカスアウト時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supCode_LostFocus(object sender, RoutedEventArgs e)
        {
            isFocusedSupCode = false;
        }

        /// <summary>
        /// 取引先コードマウスフォーカス取得時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isFocusedSupCode = true;
        }

        #endregion

        #region 支払条件フォーカスアウト
        /// <summary>
        /// 支払条件からフォーカスアウトした時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_LostFocus(object sender, RoutedEventArgs e)
        {
            SkipPaymentTerms(sender);

        }
        #endregion

        #region << 売価設定ボタン押下イベント群 >>

        /// <summary>
        /// 得意先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTokuShoBaikaSet_Click(object sender, RoutedEventArgs e)
        {
            MST19010 form = new MST19010();

            // 必要情報をプロパティに設定
            form.SendFormId = (int)MST19010.SEND_FORM.取引先マスタ;
            form.CustomerCode =  this.TORI_CODE.Text;
            form.CustomerEda = this.TORI_EDA.Text;

            // 画面を表示する
            form.Show(this);

        }

        /// <summary>
        /// 仕入先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShiShoGenkaSet_Click(object sender, RoutedEventArgs e)
        {
            MST17010 form = new MST17010();

            // 必要情報をプロパティに設定
            form.SendFormId = (int)MST19010.SEND_FORM.取引先マスタ;
            form.SupplierCode = this.TORI_CODE.Text;
            form.SupplierEda = this.TORI_EDA.Text;

            // 画面を表示する
            form.Show(this);

        }

        /// <summary>
        /// 外注先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGaiShoKakoSet_Click(object sender, RoutedEventArgs e)
        {
            MST18010 form = new MST18010();

            // 必要情報をプロパティに設定
            form.SendFormId = (int)MST19010.SEND_FORM.取引先マスタ;
            form.OutsourceCode =  this.TORI_CODE.Text;
            form.OutsourceEda = this.TORI_EDA.Text;

            // 画面を表示する
            form.Show(this);

        }
        #endregion

        #endregion

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion


        private void M01_TORI_KBN_LostFocus(object sender, RoutedEventArgs e)
        {
            TORI_KBN_TargetUpdated(sender, null);

        }

	}

}
