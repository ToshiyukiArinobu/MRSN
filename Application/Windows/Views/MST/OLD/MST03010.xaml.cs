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


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
    /// 発着地マスタ入力
	/// </summary>
	public partial class MST03010 : WindowMasterMainteBase
    {
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
        public class ConfigMST03010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST03010 frmcfg = null;

        #endregion

        #region 定数定義
		private const string TargetTableNm = "M08_TIK";
		private const string RTargetTableNm = "RM08_TIK";
		private const string UpdateTable = "M08_TIK_UP";
        private const string DeleteTable = "M08_TIK_DEL";
        private const string GetNextID = "M08_TIK_NEXT";
		private const string GETZIP = "UcZIP";
		#endregion

        #region バインド用変数
        private string _発着地コード = string.Empty;
        public string 発着地コード
        {
            get { return this._発着地コード; }
            set { this._発着地コード = value; NotifyPropertyChanged(); }
        }
        private string _発着地名 = string.Empty;
        public string 発着地名
        {
            get { return this._発着地名; }
            set { this._発着地名 = value; NotifyPropertyChanged(); }
        }
        private string _発着地かな = string.Empty;
        public string 発着地かな
        {
            get { return this._発着地かな; }
            set { this._発着地かな = value; NotifyPropertyChanged(); }
        }
        
        private string _運賃計算距離 = string.Empty;
        public string 運賃計算距離
        {
            get { return this._運賃計算距離; }
            set { this._運賃計算距離 = value; NotifyPropertyChanged(); }
        }
        private string _郵便番号 = string.Empty;
        public string 郵便番号
        {
            get { return this._郵便番号; }
            set { this._郵便番号 = value; NotifyPropertyChanged(); }
        }
        private string _住所１ = string.Empty;
        public string 住所１
        {
            get { return this._住所１; }
            set { this._住所１ = value; NotifyPropertyChanged(); }
        }
        private string _住所２ = string.Empty;
        public string 住所２
        {
            get { return this._住所２; }
            set { this._住所２ = value; NotifyPropertyChanged(); }
        }
        private string _電話番号 = string.Empty;
        public string 電話番号
        {
            get { return this._電話番号; }
            set { this._電話番号 = value; NotifyPropertyChanged(); }
        }
        private string _ＦＡＸ番号 = string.Empty;
        public string ＦＡＸ番号
        {
            get { return this._ＦＡＸ番号; }
            set { this._ＦＡＸ番号 = value; NotifyPropertyChanged(); }
        }
        private string _配達エリアコード = string.Empty;
        public string 配達エリアコード
        {
            get { return this._配達エリアコード; }
            set { this._配達エリアコード = value;

            if (value == "0")
            {
                this.配達エリアコード = string.Empty;
            }
                NotifyPropertyChanged();
            }
        }
        private string _配達エリア名 = string.Empty;
        public string 配達エリア名
        {
            get { return this._配達エリア名; }
            set { this._配達エリア名 = value; NotifyPropertyChanged(); }
        }

		private int? _類似ID = null;
		public int? 類似ID
		{
			get { return this._類似ID; }
			set { this._類似ID = value; NotifyPropertyChanged(); }
		}

        System.Windows.Forms.ContextMenuStrip cMenu = new System.Windows.Forms.ContextMenuStrip();
        bool IscMenuUsing = false;

        #endregion

        /// <summary>
        /// 発着地マスタ入力
		/// </summary>
		public MST03010()
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
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST03010)ucfg.GetConfigValue(typeof(ConfigMST03010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST03010();
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

            ScreenClear();

            base.MasterMaintenanceWindowList.Add("M08_TIK_UC", new List<Type> { null, typeof(SCH03010) });

		}

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            発着地コード = string.Empty;
            発着地名 = string.Empty;
            発着地かな = string.Empty;

            運賃計算距離 = string.Empty;
            郵便番号 = string.Empty;
            住所１ = string.Empty;
            住所２ = string.Empty;
            電話番号 = string.Empty;
            ＦＡＸ番号 = string.Empty;
            配達エリアコード = string.Empty;
            配達エリア名 = string.Empty;

            this.MaintenanceMode = string.Empty;
			txt類似ID.Visibility = Visibility.Collapsed;
			
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            this.ResetAllValidation();

            SetFocusToTopControl();

        }
        
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
            MST03020 view = new MST03020();
            view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
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

            if (string.IsNullOrEmpty(this.発着地コード))
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

            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question
                            , MessageBoxResult.No);
            //キャンセルなら終了
            if (result == MessageBoxResult.Yes)
            {
                Delete();
            }
            
        }

        #endregion

        #region Delete()
        public void Delete()
        {
            try
            {
                int i発着地コード = 0;

            if (!int.TryParse(発着地コード, out i発着地コード))
            {
                this.ErrorMessage = "発着地IDの入力形式が不正です。";
                MessageBox.Show("発着地IDの入力形式が不正です。");
                return;
            }


            //最後尾検索
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i発着地コード }));
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region Update()
        public void Update()
        {
            try
            {
                int i発着地コード = 0;

                if (!int.TryParse(発着地コード, out i発着地コード))
                {
                    this.ErrorMessage = "発着地IDの入力形式が不正です。";
                    MessageBox.Show("発着地IDの入力形式が不正です。");
                    return;
                }

                if (string.IsNullOrEmpty(発着地名))
                {
                    this.ErrorMessage = "発着地名は入力必須項目です。";
                    MessageBox.Show("発着地名は入力必須項目です。");
                    return;
                }

                int i運賃計算距離;
                int i配達エリアコード;

                if (!int.TryParse(運賃計算距離, out i運賃計算距離))
                {

                }

                if (!int.TryParse(配達エリアコード, out i配達エリアコード))
                {
                }

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i発着地コード
                                                                                                            ,発着地名
                                                                                                            ,発着地かな
                                                                                                            ,i運賃計算距離
                                                                                                            ,郵便番号
                                                                                                            ,住所１
                                                                                                            ,住所２
                                                                                                            ,電話番号
                                                                                                            ,ＦＡＸ番号
                                                                                                            ,i配達エリアコード
                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                            ,false}));
                }
            }
            catch (Exception)
            {
                return;
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

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;                  

                    switch (message.GetMessageName())
                    {
					//発着地データ取得
					case TargetTableNm:
						IscMenuUsing = false;
						if (tbl.Rows.Count > 0)
						{
							if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
							{
								//this.ErrorMessage = "既に削除されているデータです。";
								//MessageBox.Show("既に削除されているデータです。");
								//return;

								MessageBoxResult result = MessageBox.Show("既に削除されているデータです。\nこのコードを復元しますか？",
																												"質問",
																											   MessageBoxButton.YesNo,
																											   MessageBoxImage.Exclamation,
																											   MessageBoxResult.No);

								if (result == MessageBoxResult.No)
								{
									return;
								}
							}

							SetTblData(tbl);

						}

						if (tbl.Rows.Count > 0)
						{
							//編集ステータス表示
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
							txt類似ID.Visibility = Visibility.Collapsed;
						}
						else
						{
							//新規ステータス表示
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
							txt類似ID.Visibility = Visibility.Visible;
							//Keyboard.Focus(txt類似ID);
						}


						//キーをfalse
						ChangeKeyItemChangeable(false);
						SetFocusToTopControl();

						break;

					//類似データ取得
					case RTargetTableNm:
						類似ID = null;
						IscMenuUsing = false;
						if (tbl.Rows.Count > 0)
						{
							if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
							{
								this.ErrorMessage = "既に削除されているデータです。";
								MessageBox.Show("既に削除されているデータです。");
								return;
							}

							発着地名 = tbl.Rows[0]["発着地名"].ToString();
							発着地かな = tbl.Rows[0]["かな読み"].ToString();

							運賃計算距離 = tbl.Rows[0]["タリフ距離"].ToString();
							郵便番号 = tbl.Rows[0]["郵便番号"].ToString();
							住所１ = tbl.Rows[0]["住所１"].ToString();
							住所２ = tbl.Rows[0]["住所２"].ToString();

							電話番号 = tbl.Rows[0]["電話番号"].ToString();
							ＦＡＸ番号 = tbl.Rows[0]["ＦＡＸ番号"].ToString();
							配達エリアコード = tbl.Rows[0]["配送エリアID"].ToString();

						}

						break;

					case UpdateTable:


                        case DeleteTable:
                            ScreenClear();
                            break;
						case GETZIP:
							IscMenuUsing = true;
							ShowAddresList(tbl);
							break;
						default:
                            break;
                    }
                }
                else
                {
                    switch (message.GetMessageName())
                    {
                        case GetNextID:
                            if (data is int)
                            {
                                int iNextCode = (int) data;
                                発着地コード = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
								txt類似ID.Visibility = Visibility.Visible;
                                SetFocusToTopControl();
                            }
                            break;

                        case UpdateTable:
                            
                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("発着地コード: " + 発着地コード + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i発着地コード = AppCommon.IntParse(発着地コード);
                                int i運賃計算距離 = AppCommon.IntParse(運賃計算距離);
                                int i配達エリアコード = AppCommon.IntParse(配達エリアコード);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i発着地コード
                                                                                                                            ,発着地名
                                                                                                                            ,発着地かな
                                                                                                                            ,i運賃計算距離
                                                                                                                            ,郵便番号
                                                                                                                            ,住所１
                                                                                                                            ,住所２
                                                                                                                            ,電話番号
                                                                                                                            ,ＦＡＸ番号
                                                                                                                            ,i配達エリアコード
                                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                            ,true}));

                                break;
                            }


                            ScreenClear();
                            break;
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
            発着地コード = tbl.Rows[0]["発着地ID"].ToString();
            発着地名 = tbl.Rows[0]["発着地名"].ToString();
            発着地かな = tbl.Rows[0]["かな読み"].ToString();

            運賃計算距離 = tbl.Rows[0]["タリフ距離"].ToString();
            郵便番号 = tbl.Rows[0]["郵便番号"].ToString();
            住所１ = tbl.Rows[0]["住所１"].ToString();
            住所２ = tbl.Rows[0]["住所２"].ToString();

            電話番号 = tbl.Rows[0]["電話番号"].ToString();
            ＦＡＸ番号 = tbl.Rows[0]["ＦＡＸ番号"].ToString();
            配達エリアコード = tbl.Rows[0]["配送エリアID"].ToString();
        }

		/// <summary>
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click(object sender, RoutedEventArgs e)
		{
            
            try
            {
				//先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
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
            int iKeyCD = 0;
            try
			{
                if (!string.IsNullOrEmpty(発着地コード))
                {
                    iKeyCD = AppCommon.IntParse(this.発着地コード);
                }
                
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, -1 }));
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
            int iKeyCD = 0;
            try
            {
                if (!string.IsNullOrEmpty(発着地コード))
                {
                    iKeyCD = AppCommon.IntParse(this.発着地コード);
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, 1 }));
                }
                else
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));

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
				//最後尾検索
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}


        /// <summary>
        /// 発着地コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    
                    try
                    {

                        if (string.IsNullOrEmpty(発着地コード))
                        {
                            //自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));
                    
                            return;
                        }
                        int i発着地コード = 0;

                        if (!int.TryParse(発着地コード, out i発着地コード))
                        {
                            this.ErrorMessage = "発着地IDの入力形式が不正です。";
                            MessageBox.Show("発着地IDの入力形式が不正です。");
                            return;
                        }

                        
                        //発着地データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i発着地コード, 0 }));
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


        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

		private void ShowAddresList(DataTable tbl)
		{
			List<string> addrlist = new List<string>();
			foreach (DataRow row in tbl.Rows)
			{
				addrlist.Add((string)row["住所漢字"]);
			}
			if (addrlist == null || addrlist.Count == 0)
			{
				return;
			}
			if (addrlist.Count == 1)
			{
				住所１ = addrlist[0];
				return;
			}

			cMenu = new System.Windows.Forms.ContextMenuStrip();
			cMenu.PreviewKeyDown += cMenu_PreviewKeyDown;
			foreach (string addr in addrlist)
			{
				cMenu.Items.Add(addr, null, new System.EventHandler(SelectedAddress));
			}
			Point pnt = this.ZipCode.PointToScreen(new Point(0.0, 0.0));
			cMenu.Show((int)pnt.X + 80, (int)(pnt.Y) + 28);
		}

		void cMenu_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				cMenu.Close();
			}
		}

        private void ZIPCODE_Changed(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(郵便番号))
            {
                住所１ = string.Empty;
                return;
            }
            var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
            if (ctl == null)
            {
                return;
            }
            string code = ctl.Text;
            code = code.Replace("-", "");
            if (code.Length != 7)
            {
                return;
            }
			CommunicationObject com = new CommunicationObject(MessageType.RequestData, GETZIP, code);
			base.SendRequest(com);
			//string[] addrlist = AppCommon.GetZipData(this, code);
			//if (addrlist == null || addrlist.Length == 0)
			//{
			//	return;
			//}
			//if (addrlist.Length == 1)
			//{
			//	住所１ = addrlist[0].ToString();
			//	return;
			//}

			//cMenu = new System.Windows.Forms.ContextMenuStrip();
			//foreach (string addr in addrlist)
			//{
			//	cMenu.Items.Add(addr, null, new System.EventHandler(SelectedAddress));
			//}
			//Point pnt = (sender as Control).PointToScreen(new Point(0.0, 0.0));
			//cMenu.Show((int)pnt.X + 80, (int)(pnt.Y) + 28);
        }

        private void SelectedAddress(object sender, EventArgs e)
        {
            住所１ = sender.ToString();
        }

        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }


		private void txt類似ID_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
				{
					//発着地データ検索
					base.SendRequest(new CommunicationObject(MessageType.RequestData, RTargetTableNm, new object[] { 類似ID }));
				}
			}
		}
	}
}
