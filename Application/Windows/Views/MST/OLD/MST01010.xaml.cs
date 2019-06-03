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

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 取引先マスターメンテ
    /// </summary>
	public partial class MST01010 : WindowMasterMainteBase
	{
		public UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion
        public class ConfigMST01010 : FormConfigBase
		{
		}

		public ConfigMST01010 frmcfg = null;

		private const string TokID_CHG = "TokID_CHG";
		private const string TokGetData = "M_M01_TOK";
		private const string RTokGetData = "R_M_M01_TOK";
		private const string TokInsert = "M_M01_TOK_ADD";
		private const string TokUpdate = "M_M01_TOK_PUT";
		private const string TokDelete = "M_M01_TOK_DEL";
		private const string TOK_NEXT = "M_M01_TOK_NEXT";
		private const string GETZIP = "UcZIP";
        //登録件数
        private const string GetMaxMeisaiNo = "M01_GetMaxMeisaiNo";
		private bool IsDataChanged = false;

		private string _customerID = string.Empty;
		public string CustomerID
		{
			get { return this._customerID; }
			set { this._customerID = value; NotifyPropertyChanged(); }
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
					if (value.IsNull("取引先ID") != true)
					{
						this.CustomerID = string.Format("{0}", value["取引先ID"]);
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


        private int? _登録件数 = null;
        public int? 登録件数
        {
            get { return this._登録件数; }
            set { this._登録件数 = value; NotifyPropertyChanged(); }
        }

		private string _担当部門名 = string.Empty;
		public string 担当部門名
		{
			get { return this._担当部門名; }
			set { this._担当部門名 = value; NotifyPropertyChanged(); }
		}
		private string _親マスタコード名 = string.Empty;
		public string 親マスタコード名
		{
			get { return this._親マスタコード名; }
			set { this._親マスタコード名 = value; NotifyPropertyChanged(); }
		}

		private int _取引区分 = 0;
		public int 取引区分
		{
			get { return this._取引区分; }
			set { this._取引区分 = value; NotifyPropertyChanged(); }
		}

		//False
		private string _請求区分 = "True";
		public string 請求区分
		{
			get { return this._請求区分; }
			set { this._請求区分 = value; NotifyPropertyChanged(); }
		}

		private string _支払区分 = "True";
		public string 支払区分
		{
			get { return this._支払区分; }
			set { this._支払区分 = value; NotifyPropertyChanged(); }
		}

		private int _iNextCode = 0;
		public int iNextCode
		{
			get { return this._iNextCode; }
			set { this._iNextCode = value; NotifyPropertyChanged(); }
		}

		private int? _new_ID;
		public int? new_ID
		{
			get { return this._new_ID; }
			set { this._new_ID = value; NotifyPropertyChanged(); }
		}

		System.Windows.Forms.ContextMenuStrip cMenu = new System.Windows.Forms.ContextMenuStrip();
		bool IscMenuUsing = false;

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
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST01010)ucfg.GetConfigValue(typeof(ConfigMST01010));

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg == null)
			{
				frmcfg = new ConfigMST01010();
				//画面サイズをタスクバーをのぞいた状態で表示させる
				//this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Top;
			}
			else
			{
				this.Top = frmcfg.Top;
				this.Left = frmcfg.Left;
				this.Height = frmcfg.Height;
				this.Width = frmcfg.Width;
			}

			base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
			base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { typeof(MST10010), typeof(SCH10010) });
			base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST12010), typeof(SCH12010) });

			AppCommon.SetutpComboboxList(this.cmb消費税区分_支払, false);
			AppCommon.SetutpComboboxList(this.cmb消費税区分_請求, false);
			AppCommon.SetutpComboboxList(this.cmb親子区分, false);
			AppCommon.SetutpComboboxList(this.cmb請求書運賃計算区分_支払, false);
			AppCommon.SetutpComboboxList(this.cmb請求書運賃計算区分_請求, false);
			AppCommon.SetutpComboboxList(this.cmb請求書区分, false);

			ScreenClear();
			ChangeKeyItemChangeable(true);
            Txt登録件数.Focusable = false;
			SetFocusToTopControl();

        }

    	public override void OnF1Key(object sender, KeyEventArgs e)
		{
			try
			{
				ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
			}
			catch (Exception ex)
			{
				appLog.Error("検索画面起動エラー", ex);
				ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}
		}

		/// <summary>
		/// F2　リボンマスタ入力
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
				ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}
		}

		/// <summary>
		/// F8 リスト
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF8Key(object sender, KeyEventArgs e)
		{
			MST01020 frm = new MST01020();
			frm.ShowDialog(this);
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
		/// F10　取消し　
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF10Key(object sender, KeyEventArgs e)
		{
			//if (this.RowM01TOK == null)
			//{
			//    MessageBox.Show("登録内容がありません。");
			//    return;
			//}

			var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
			if (yesno == MessageBoxResult.No)
			{
				return;
			}

			ScreenClear();
		}

		/// <summary>
		/// F11　リボン終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF11Key(object sender, KeyEventArgs e)
		{
			ucfg.SetConfigValue(frmcfg);
			this.Close();
		}

		/// <summary>
		/// F12 削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF12Key(object sender, KeyEventArgs e)
		{

			if (string.IsNullOrEmpty(this.CustomerID))
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

			MessageBoxResult result = MessageBox.Show("データを削除しても宜しいですか？"
							 , "確認"
							 , MessageBoxButton.YesNo
							 , MessageBoxImage.Question
							 , MessageBoxResult.No);

			if (result == MessageBoxResult.Yes)
			{
				Delete();
			}

		}

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			base.OnReceivedResponseData(message);
			try
			{
				this.ErrorMessage = string.Empty;

				var data = message.GetResultData();

				if (data is DataTable)
				{
					DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
					switch (message.GetMessageName())
					{
					case TokGetData:

						if (tbl == null)
						{
							RowM01TOK = null;
							this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
							return;
						}
						IscMenuUsing = false;
						if (tbl.Rows.Count > 0)
						{
							if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
							{
								this.ErrorMessage = "既に削除されているデータです。";
								MessageBox.Show("削除されているデータです。");
								ScreenClear();
								return;
							}
							RowM01TOK = tbl.Rows[0];
							int i取引区分 = AppCommon.IntParse(tbl.Rows[0]["取引区分"].ToString());
							switch (i取引区分)
							{
							case 0:

								break;
							case 1:
								sShime.Text = null;
								sSite.Text = null;
								sShukin.Text = null;
								break;
							case 2:
								tShime.Text = null;
								tSite.Text = null;
								tShukin.Text = null;
								break;
							case 3:
								tShime.Text = null;
								tSite.Text = null;
								tShukin.Text = null;
								break;
							}

						}

						if (RowM01TOK == null)
						{
							RowM01TOK = tbl.NewRow();
							RowM01TOK["取引先ID"] = this.CustomerID;
							RowM01TOK["取引区分"] = 0;
							RowM01TOK["ラベル区分"] = 0;
							RowM01TOK["Ｔ路線計算まるめ"] = 0;
							RowM01TOK["請求内訳管理区分"] = 0;
							RowM01TOK["Ｓ路線計算まるめ"] = 0;
							RowM01TOK["親子区分ID"] = 0;
							RowM01TOK["請求運賃計算区分ID"] = 0;
							RowM01TOK["支払運賃計算区分ID"] = 0;
							RowM01TOK["請求書区分ID"] = 0;
							RowM01TOK["Ｔ税区分ID"] = 0;
							RowM01TOK["Ｓ税区分ID"] = 0;
                            // 追加
                            RowM01TOK["取引停止区分"] = false;
                            tbl.Rows.Add(RowM01TOK);
							NotifyPropertyChanged("RowM01TOK");
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
							ID変換.Label_Context = "類似ID";
                        }
						else
						{
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
							ID変換.Label_Context = "ID変換";
						}
						ChangeKeyItemChangeable(false);
						SetFocusToTopControl();
						IscMenuUsing = true;
						ResetAllValidation();
						Radio_TargetUpdated(null, null);
						break;
					case RTokGetData:

						if (tbl == null)
						{
							RowM01TOK = null;
							this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
							return;
						}
						IscMenuUsing = true;
						if (tbl.Rows.Count > 0)
						{
							if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
							{
								this.ErrorMessage = "既に削除されているデータです。";
								MessageBox.Show("削除されているデータです。");
								ScreenClear();
								return;
							}
							tbl.Rows[0]["取引先ID"] = this.CustomerID;
							RowM01TOK = tbl.Rows[0];
							int i取引区分 = AppCommon.IntParse(tbl.Rows[0]["取引区分"].ToString());
							switch (i取引区分)
							{
							case 0:

								break;
							case 1:
								sShime.Text = null;
								sSite.Text = null;
								sShukin.Text = null;
								break;
							case 2:
								tShime.Text = null;
								tSite.Text = null;
								tShukin.Text = null;
								break;
							case 3:
								tShime.Text = null;
								tSite.Text = null;
								tShukin.Text = null;
								break;
							}

						}

						if (RowM01TOK == null)
						{
							MessageBox.Show("データがありません。");
							return;
						}
						ResetAllValidation();
						Radio_TargetUpdated(null, null);
						break;
					case TOK_NEXT:
						if (data is int)
						{
							iNextCode = (int)data;
							CustomerID = iNextCode.ToString();
							ChangeKeyItemChangeable(false);
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
							ID変換.Label_Context = "類似ID";
							SetFocusToTopControl();
                            CustomerCd_LostFocus(null, null);
						}
						break;
					case TokUpdate:
					case TokInsert:
					case TokDelete:
						ScreenClear();
						break;
					case GETZIP:
						ShowAddresList(tbl);
						break;

                    }
				}
				else
				{
					switch (message.GetMessageName())
					{
					case TOK_NEXT:
						if (data is int)
						{
							int iNextCode = (int)data;
                            //自動採番で1以下の値を取ってきた場合はIDに1を導入します
                            if (iNextCode < 1)
                            {
                                iNextCode = 1;
                            }
							CustomerID = iNextCode.ToString();
							ChangeKeyItemChangeable(false);
							this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
							ID変換.Label_Context = "類似ID";
							SetFocusToTopControl();
                            CustomerCd_LostFocus(null, null);
						}
						break;

					case TokInsert:

						if ((int)data == -1)
						{
							MessageBoxResult result = MessageBox.Show("得意先ID: " + CustomerID + "は既に使われています。\n自動採番して登録しますか？",
																											"質問",
																										   MessageBoxButton.YesNo,
																										   MessageBoxImage.Exclamation,
																										   MessageBoxResult.No);

							if (result == MessageBoxResult.No)
							{
								return;
							}

							SendRequest(new CommunicationObject(MessageType.UpdateData, TokInsert, RowM01TOK
								, this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
								, true));
							break;
						}

						ScreenClear();
						break;
					case TokUpdate:
						ScreenClear();
						break;
					case TokDelete:
						ScreenClear();
						break;
					case TokID_CHG:
						if (data is int)
						{
							switch ((int)data)
							{
							case -1:
								MessageBox.Show("このIDはすでに使用済みです。");
								break;
							case 0:
								MessageBox.Show("変換に失敗しました。");
								break;
							default:
								CustomerID = ID変換.Text;
                                RowM01TOK["取引先ID"] = this.CustomerID;
								ID変換.Text = null;
								MessageBox.Show("変換完了しました。");
								break;
							}
						}
						break;

                    case GetMaxMeisaiNo:
                        登録件数 = (int)data;
                        break;

                    }
				}


			}
			catch (Exception ex)
			{
				RowM01TOK = null;
				appLog.Error("受信データ処理例外発生しました。", ex);
				this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
			}
		}

		private void ScreenClear()
		{
			new_ID = null;
			this.IsDataChanged = false;
			this.MaintenanceMode = null;
			CustomerID = string.Empty;
			RowM01TOK = null;
			ChangeKeyItemChangeable(true);
			ResetAllValidation();
			SetFocusToTopControl();

            //現在の登録件数を表示
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMaxMeisaiNo));
		}

		private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue == false)
			{
				(sender as Button).IsEnabled = true;
			}
		}

		private void TokCode_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (string.IsNullOrEmpty(CustomerID))
				{
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TOK_NEXT, new object[] { }));
					//CustomerID = iNextCode.ToString();
					//CustomerCd_LostFocus(sender, null);
					return;
				}

				int i取引先ID = 0;
				if (!int.TryParse(CustomerID, out i取引先ID))
				{
					this.ErrorMessage = "取引先IDの入力形式が不正です。";
					MessageBox.Show("取引先IDの入力形式が不正です。");
					return;
				}
				CustomerCd_LostFocus(sender, null);

			}
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}

		private void CustomerCd_LostFocus(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				cstmid = AppCommon.IntParse(this.CustomerID);
				//取引先マスタ
				datagetmode = DataGetMode.number;
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, cstmid, 0));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void Button1st_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				datagetmode = DataGetMode.first;
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, null, 0));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void ButtonPrev_Click(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				datagetmode = DataGetMode.previous;
				if (string.IsNullOrEmpty(this.CustomerID))
				{
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, null, 0));
				}
				else
				{
					cstmid = AppCommon.IntParse(this.CustomerID);
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, cstmid, -1));
				}
			}
			catch (Exception)
			{
				return;
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				datagetmode = DataGetMode.next;
				if (string.IsNullOrEmpty(this.CustomerID))
				{
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, null, 0));
				}
				else
				{
					cstmid = AppCommon.IntParse(this.CustomerID);
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, cstmid, 1));
				}
			}
			catch (Exception)
			{
				return;
			}
		}
		private void ButtonLast_Click(object sender, RoutedEventArgs e)
		{
			try
			{


				datagetmode = DataGetMode.last;
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TokGetData, null, 1));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void Update()
		{
			try
			{
                if (RowM01TOK == null)
                {
                    return;
                }
				switch (RowM01TOK["取引区分"].ToString())
				{
				case "0":
					if (string.IsNullOrEmpty(sShime.Text) || string.IsNullOrEmpty(sShukin.Text) || string.IsNullOrEmpty(tShime.Text) || string.IsNullOrEmpty(tShukin.Text))
					{
						this.ErrorMessage = "締日の入力形式が不正です。";
						MessageBox.Show("締日の入力形式が不正です。");
						return;
					}
					break;

				case "1":
					if (string.IsNullOrEmpty(tShime.Text) || string.IsNullOrEmpty(tShukin.Text))
					{
						this.ErrorMessage = "締日の入力形式が不正です。";
						MessageBox.Show("締日の入力形式が不正です。");
						return;
					}
					break;
				case "2":
					if (string.IsNullOrEmpty(sShime.Text) || string.IsNullOrEmpty(sShukin.Text))
					{
						this.ErrorMessage = "締日の入力形式が不正です。";
						MessageBox.Show("締日の入力形式が不正です。");
						return;
					}
					break;

				case "3":
					if (string.IsNullOrEmpty(sShime.Text) || string.IsNullOrEmpty(sShukin.Text))
					{
						this.ErrorMessage = "締日の入力形式が不正です。";
						MessageBox.Show("締日の入力形式が不正です。");
						return;
					}
					break;
				}

				int i取引先ID = 0;

				if (!int.TryParse(this.CustomerID, out i取引先ID))
				{
					this.ErrorMessage = "取引先IDの入力形式が不正です。";
					MessageBox.Show("取引先IDの入力形式が不正です。");
					return;
				}

				if (!base.CheckAllValidation())
				{
					this.ErrorMessage = "入力内容に誤りがあります。";
					MessageBox.Show("入力内容に誤りがあります。");
					SetFocusToTopControl();
					return;
				}


                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}

				if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
				{
					SendRequest(new CommunicationObject(MessageType.UpdateData, TokInsert, RowM01TOK
						, this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
						, false));
				}
				else
				{
					SendRequest(new CommunicationObject(MessageType.UpdateData, TokUpdate, RowM01TOK));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Delete()
		{
			try
			{
				var yesno = MessageBox.Show("データを削除しますか？", "削除確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}

				SendRequest(new CommunicationObject(MessageType.UpdateData, TokDelete, RowM01TOK));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
				RowM01TOK["住所１"] = addrlist[0];
				NotifyPropertyChanged("RowM01TOK");
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
			if (IscMenuUsing != true)
			{
				return;
			}
			var ctl = sender as KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox;
			if (ctl == null)
			{
				return;
			}
			string code = ctl.Text;
			if (string.IsNullOrWhiteSpace(code))
			{
				return;
			}
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
			//	RowM01TOK["住所１"] = addrlist[0];
			//	NotifyPropertyChanged("RowM01TOK");
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
			RowM01TOK["住所１"] = sender.ToString();
			NotifyPropertyChanged("RowM01TOK");
		}

		#region Window_Closed
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

		private void cmb親子区分_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count == 0 || this.RowM01TOK == null)
			{
				return;
			}
			if ((e.AddedItems[0] as CodeData).表示名.Contains("子") == true)
			{
				this.mcode.Text1IsReadOnly = false;
			}
			else
			{
				this.mcode.Text1IsReadOnly = true;
			}
			// 子以外は親なしとする
			if ((e.AddedItems[0] as CodeData).表示名.Contains("子") != true)
			{
				this.RowM01TOK["親ID"] = DBNull.Value;
				this.親マスタコード名 = "";
				NotifyPropertyChanged("RowM01TOK");
			}
		}


		private void UcLabelTextRadioButton_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Update();
		}

		private void cmb請求書区分_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				int Torihikikbn = AppCommon.IntParse(RowM01TOK["取引区分"].ToString());
				if (Torihikikbn == 1)
				{
					Update();
				}
			}

		}

		private void RadioValueChenge(object sender, DataTransferEventArgs e)
		{

		}

		//private void RadioChenge(object sender, RoutedEventArgs e)
		//{
		//    this.TOK.IsEnabled = true;
		//    //this.SHR.IsEnabled = true;

		//    int RadioNo = AppCommon.IntParse(this.Radio.Text);
		//    if (RadioNo == 1)
		//    {
		//        //this.SHR.IsEnabled = false;
		//    }
		//    else if (RadioNo == 2 || RadioNo == 3)
		//    {
		//        this.TOK.IsEnabled = false;
		//    }
		//}

		//private void RadioLostFocus(object sender, RoutedEventArgs e)
		//{
		//    this.TOK.IsEnabled = true;
		//    //this.SHR.IsEnabled = true;

		//    int RadioNo = AppCommon.IntParse(this.Radio.Text);
		//    if (RadioNo == 1)
		//    {
		//        this.SHR.IsEnabled = false;
		//    }
		//    else if (RadioNo == 2 || RadioNo == 3)
		//    {
		//        this.TOK.IsEnabled = false;
		//    }
		//}

		private void Radio_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			int kbn;
			string strTEXT = RowM01TOK["取引区分"].ToString();
			int.TryParse(strTEXT, out kbn);

			//int kbn = RowM01TOK == null ? 0 : RowM01TOK["取引区分"] ? 0 : AppCommon.IntParse(RowM01TOK["取引区分"].ToString());
			
			switch (kbn)
			{
			case 0:
				請求区分 = "True";
				支払区分 = "True";
				twn請求書発行元.IsRequired = true;
				break;
			case 1:
				請求区分 = "True";
				支払区分 = "False";
				twn請求書発行元.IsRequired = true;
				break;
			case 2:
				請求区分 = "False";
				支払区分 = "True";
				twn請求書発行元.IsRequired = false;
				break;
			case 3:
				請求区分 = "False";
				支払区分 = "True";
				twn請求書発行元.IsRequired = false;
				break;
			}

		}

		private void Radio_TextUpdated(object sender, DataTransferEventArgs e)
		{
			Radio_TargetUpdated(null, null);
		}

		private void test_LostFocus(object sender, RoutedEventArgs e)
		{
			if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD && string.IsNullOrEmpty(RowM01TOK["略称名"].ToString()))
			{
				Ryakusyou.Text = test.Text;
				//RowM01TOK["取引先名１"] = RowM01TOK["取引先名１"];
			}
		}


		private void ID変換_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				int old_ID = 0;
				if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
				{
					if (new_ID == 0 || new_ID == null)
					{
						return;
					}
					if (!int.TryParse(CustomerID, out old_ID))
					{
						MessageBox.Show("入力内容に誤りがあります。");
						return;
					}
					var yesno = MessageBox.Show("IDを変更しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
					if (yesno == MessageBoxResult.No)
					{
						return;
					}
					base.SendRequest(new CommunicationObject(MessageType.RequestData, TokID_CHG, old_ID, new_ID));
				}
				else
				{
					if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
					{
						int cstmid = 0;
						try
						{
							cstmid = AppCommon.IntParse(new_ID.ToString());
							if (cstmid == 0)
							{
								return;
							}
							//取引先マスタ
							datagetmode = DataGetMode.number;
							base.SendRequest(new CommunicationObject(MessageType.RequestData, RTokGetData, cstmid, 0));
							return;
						}
						catch (Exception)
						{
							return;
						}
					}

					MessageBox.Show("変換するデータを呼び出して下さい。");
					//return;
				}
			}
		}
	}
}
