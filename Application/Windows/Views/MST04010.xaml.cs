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
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 乗務員マスターメンテナンス
	/// </summary>
	public partial class MST04010 : WindowMasterMainteBase
	{
		/// <summary>
		/// 乗務員マスタ
		/// </summary>
		private const string StaffTableName = "M_M04_DRV";
		private const string StaffTableUpdate = "M_M04_DRV_UPD";
		private const string StaffTableDelete = "M_M04_DRV_DEL";
		/// <summary>
		/// 乗務員画像マスタ
		/// </summary>
		private const string StaffPicTableName = "M_M04_DRVPIC";
		/// <summary>
		/// 適正診断実施
		/// </summary>
		private const string ProperDiagnosis ="M04_DDT1";
		/// <summary>
		/// 事故履歴
		/// </summary>
		private const string AccidentHistory ="M04_DDT2";
		/// <summary>
		/// 違反履歴
		/// </summary>
		private const string ViolationHistory = "M04_DDT3";
		/// <summary>
		/// 特別教育実績
		/// </summary>
		private const string SpecialEducation = "M04_DDT4";
		/// <summary>
		/// 特別教育実績
		/// </summary>
		private const string JisyaTableName = "M71_JIS";
		/// <summary>
		/// 特別教育実績
		/// </summary>
		private const string BumonTableName = "M71_BUM";

		private string _driverID = string.Empty;
		public string DriverID
		{
			get { return this._driverID; }
			set { this._driverID = value; NotifyPropertyChanged(); }
		}
		private DataRow _rowM04;
		public DataRow RowM04
		{
			get { return this._rowM04; }
			set
			{
				this._rowM04 = value;
				NotifyPropertyChanged();
				if (value != null)
				{
					this.DriverID = string.Format("{0}", value["乗務員ID"]);
					NotifyPropertyChanged("DriverID");
					for (int ix = 0; ix < 10; ix++)
					{
						if (value[string.Format("免許種類{0}", ix + 1)] == DBNull.Value)
						{
							LisenceChecked[ix] = false;
						}
						else
						{
							LisenceChecked[ix] = (((int)value[string.Format("免許種類{0}", ix + 1)] == 1) ? true : false);
						}
					}
					//LisenceChecked[1] = (((int)value["免許種類2"] == 1) ? true : false);
					//LisenceChecked[2] = (((int)value["免許種類3"] == 1) ? true : false);
					//LisenceChecked[3] = (((int)value["免許種類4"] == 1) ? true : false);
					//LisenceChecked[4] = (((int)value["免許種類5"] == 1) ? true : false);
					//LisenceChecked[5] = (((int)value["免許種類6"] == 1) ? true : false);
					//LisenceChecked[6] = (((int)value["免許種類7"] == 1) ? true : false);
					//LisenceChecked[7] = (((int)value["免許種類8"] == 1) ? true : false);
					//LisenceChecked[8] = (((int)value["免許種類9"] == 1) ? true : false);
					//LisenceChecked[9] = (((int)value["免許種類10"] == 1) ? true : false);
					NotifyPropertyChanged("LisenceChecked");
				}
				else
				{
					for (int ix = 0; ix < 10; ix++)
					{
						LisenceChecked[ix] = false;
					}
					NotifyPropertyChanged("LisenceChecked");
				}
			}
		}

		private string _jisyaName = string.Empty;
		public string JisyaName
		{
			get { return this._jisyaName; }
			set { this._jisyaName = value; NotifyPropertyChanged(); }
		}

		private string _bumonName = string.Empty;
		public string BumonName
		{
			get { return this._bumonName; }
			set { this._bumonName = value; NotifyPropertyChanged(); }
		}

		private List<bool?> _lisenceChecked = new List<bool?>() { false, false, false, false, false, false, false, false, false, false, };
		public List<bool?> LisenceChecked
		{
			get { return _lisenceChecked; }
			set
			{ 
				_lisenceChecked = value;
				NotifyPropertyChanged();
			}
		}

		private DataTable _driverData = new DataTable();
		public DataTable DriverData
		{
			get { return this._driverData; }
			set
			{
				this._driverData = value;
				if (value == null)
				{
					this.RowM04 = null;
				}
				else
				{
					if (value.Rows.Count == 0)
					{
						value.Rows.Add(value.NewRow());
						value.Rows[0]["乗務員ID"] = this.DriverID;
					}
					this.RowM04 = value.Rows[0];
				}
				NotifyPropertyChanged();
			}
		}
		private DataTable _properDiagnosisData;
		public DataTable ProperDiagnosisData
		{
			get { return this._properDiagnosisData; }
			set { this._properDiagnosisData = value; NotifyPropertyChanged(); }
		}
		private DataTable _accidentHistoryData;
		public DataTable AccidentHistoryData
		{
			get { return this._accidentHistoryData; }
			set { this._accidentHistoryData = value; NotifyPropertyChanged(); }
		}
		private DataTable _violationHistoryData;
		public DataTable ViolationHistoryData
		{
			get { return this._violationHistoryData; }
			set { this._violationHistoryData = value; NotifyPropertyChanged(); }
		}
		private DataTable _specialEducationData;
		public DataTable SpecialEducationData
		{
			get { return this._specialEducationData; }
			set { this._specialEducationData = value; NotifyPropertyChanged(); }
		}

		private byte[] _driverPicData;
		public byte[] DriverPicData
		{
			get { return this._driverPicData; }
			set 
			{
				this._driverPicData = value;
				NotifyPropertyChanged();
			}
		}

		private string _自社名 = string.Empty;
		public string 自社名
		{
			get { return this._自社名; }
			set { this._自社名 = value; NotifyPropertyChanged(); }
		}
		private string _担当部門名 = string.Empty;
		public string 担当部門名
		{
			get { return this._担当部門名; }
			set { this._担当部門名 = value; NotifyPropertyChanged(); }
		}
		private string _デジタコ名 = string.Empty;
		public string デジタコ名
		{
			get { return this._デジタコ名; }
			set { this._デジタコ名 = value; NotifyPropertyChanged(); }
		}


		/// <summary>
		/// 運転日報入力
		/// </summary>
		public MST04010() : base()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

			base.MasterMaintenanceWindowList.Add("取引先", new List<Type> { typeof(MST01010), typeof(SCH14010) });
			base.MasterMaintenanceWindowList.Add("支払先", new List<Type> { typeof(MST01010), typeof(SCH15010) });
			base.MasterMaintenanceWindowList.Add("仕入先", new List<Type> { typeof(MST01010), typeof(SCH16010) });
			base.MasterMaintenanceWindowList.Add("請求内訳", new List<Type> { typeof(MST02010), null });
			base.MasterMaintenanceWindowList.Add("乗務員", new List<Type> { typeof(MST04010), null });
			base.MasterMaintenanceWindowList.Add("車種", new List<Type> { typeof(MST05010), typeof(SCH05010) });
			base.MasterMaintenanceWindowList.Add("車輌", new List<Type> { typeof(MST06010), typeof(SCH06010) });
			base.MasterMaintenanceWindowList.Add("商品", new List<Type> { typeof(MST07010), typeof(SCH07010) });
			base.MasterMaintenanceWindowList.Add("摘要", new List<Type> { typeof(MST08010), typeof(SCH08010) });
			base.MasterMaintenanceWindowList.Add("自社部門", new List<Type> { typeof(MST10010), typeof(SCH10010) });
			base.MasterMaintenanceWindowList.Add("コース配車", new List<Type> { typeof(MST11010), typeof(SCH11010) });
			base.MasterMaintenanceWindowList.Add("自社名", new List<Type> { typeof(MST12010), typeof(SCH12010) });
			base.MasterMaintenanceWindowList.Add("消費税率", new List<Type> { typeof(MST13010), null });
			base.MasterMaintenanceWindowList.Add("支払先別軽油", new List<Type> { typeof(MST14010), null });
			base.MasterMaintenanceWindowList.Add("得意先別車種別単価", new List<Type> { typeof(MST16010), null });
			base.MasterMaintenanceWindowList.Add("得意先別距離別運賃", new List<Type> { typeof(MST17010), null });
			base.MasterMaintenanceWindowList.Add("得意先別個建単価", new List<Type> { typeof(MST18010), null });
			base.MasterMaintenanceWindowList.Add("支払先別地区単価", new List<Type> { typeof(MST19010), null });
			base.MasterMaintenanceWindowList.Add("支払先別車種別単価", new List<Type> { typeof(MST20010), null });
			base.MasterMaintenanceWindowList.Add("支払先別距離別運賃", new List<Type> { typeof(MST21010), null });
			base.MasterMaintenanceWindowList.Add("支払先別個建単価", new List<Type> { typeof(MST22010), null });
			base.MasterMaintenanceWindowList.Add("担当者", new List<Type> { typeof(MST23010), typeof(SCH13010) });

			ChangeKeyItemChangeable(true);
			SetFocusToTopControl();
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
		}

		/// <summary>
		/// 取得データの取り込み
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
				case StaffTableName:
					DriverData = tbl;
					ChangeKeyItemChangeable(false);
					SetFocusToTopControl();
					MakeLinkeDataTable();
					break;
				case StaffPicTableName:
					if (tbl.Rows.Count == 0)
					{
						DriverPicData = null;
					}
					else
					{
						if (tbl.Rows[0]["画像"] == DBNull.Value)
						{
							DriverPicData = null;
						}
						else
						{
							DriverPicData = (byte[])tbl.Rows[0]["画像"];
						}
					}
					break;
				case ProperDiagnosis:
					this.ProperDiagnosisData = (data as DataTable);
					break;
				case AccidentHistory:
					this.AccidentHistoryData = (data as DataTable);
					break;
				case ViolationHistory:
					this.ViolationHistoryData = (data as DataTable);
					break;
				case SpecialEducation:
					this.SpecialEducationData = (data as DataTable);
					break;
				case StaffTableUpdate:
					ScreenClear();
					break;
				case StaffTableDelete:
					ScreenClear();
					break;
				}
			}
			catch (Exception ex)
			{
				this.ErrorMessage = ex.Message;
			}
		}

		/// <summary>
		/// 関連テーブルの読み込み
		/// </summary>
		private void MakeLinkeDataTable()
		{
			try
			{
				int? drvCD;
				drvCD = int.Parse(this.DriverID);
				//適正診断実施
				//事故履歴
				//違反履歴
				//特別教育実績
				CommunicationObject[] comlist = {
												new CommunicationObject(MessageType.RequestData, StaffPicTableName, new object[] { drvCD }),
												new CommunicationObject(MessageType.RequestData, ProperDiagnosis, new object[] { drvCD }),
												new CommunicationObject(MessageType.RequestData, AccidentHistory, new object[] { drvCD }),
												new CommunicationObject(MessageType.RequestData, ViolationHistory, new object[] { drvCD }),
												new CommunicationObject(MessageType.RequestData, SpecialEducation, new object[] { drvCD }),
											};
				foreach (CommunicationObject com in comlist)
				{
					SendRequest(com);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				//乗務員マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, StaffTableName, new object[] { null, 0 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BeforeIdButton_Click_1(object sender, RoutedEventArgs e)
		{
			int drvid = 0;
			try
			{
				drvid = int.Parse(this.DriverID);
				//乗務員マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, StaffTableName, new object[] { drvid, -1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextIdButton_Click_1(object sender, RoutedEventArgs e)
		{
			int drvid = 0;
			try
			{
				drvid = int.Parse(this.DriverID);
				//乗務員マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, StaffTableName, new object[] { drvid, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LastIdButoon_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				//乗務員マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, StaffTableName, new object[] { null, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}


		/// <summary>
		/// 乗務員コードの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void JyoumuinCd_LostFocus_1(object sender, RoutedEventArgs e)
		{
			int drvid = 0;
			try
			{
				drvid = int.Parse(this.DriverID);
				//乗務員マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, StaffTableName, new object[] { drvid, 0 }));
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
				if (this.RowM04 == null)
				{
					MessageBox.Show("登録内容がありません。");
					return;
				}

				int? drvCD;
				drvCD = int.Parse(this.DriverID);

				DataTable m04 = new DataTable(StaffTableName);
				foreach (DataColumn col in this.RowM04.Table.Columns)
				{
					DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
					newcol.AllowDBNull = col.AllowDBNull;
					m04.Columns.Add(newcol);
				}
				DataRow row = m04.NewRow();
				foreach(DataColumn col in m04.Columns)
				{
					row[col.ColumnName] = this.RowM04[col.ColumnName];
				}
				for (int idx = 0; idx < this.LisenceChecked.Count; idx++)
				{
					row[string.Format("免許種類{0}", idx+1)] = (this.LisenceChecked[idx] == true) ? 1 : 0;
				}
				m04.Rows.Add(row);

				DataTable m04pic = new DataTable(StaffPicTableName);
				m04pic.Columns.AddRange(new DataColumn[] { 
					new DataColumn("乗務員ID", m04.Columns["乗務員ID"].DataType),
					new DataColumn("画像", typeof(byte[])),
					}
					);
				DataRow rowpic = m04pic.NewRow();
				rowpic["乗務員ID"] = this.DriverID;
				rowpic["画像"] = this.DriverPicData;
				m04pic.Rows.Add(rowpic);

				CommunicationObject com = new CommunicationObject(MessageType.UpdateData, StaffTableUpdate, new object[] { m04, m04pic, this.ProperDiagnosisData, this.ViolationHistoryData, this.AccidentHistoryData, this.SpecialEducationData, });
				SendRequest(com);
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
				if (this.RowM04 == null)
				{
					MessageBox.Show("登録内容がありません。");
					return;
				}

				int? drvCD;
				drvCD = int.Parse(this.DriverID);

				CommunicationObject com = new CommunicationObject(MessageType.UpdateData, StaffTableDelete, drvCD);
				SendRequest(com);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ScreenClear()
		{
			this.DriverData = null;
			this.DriverPicData = null;
			ChangeKeyItemChangeable(true);
			SetFocusToTopControl();
		}

		/// <summary>
		/// 画像エリアのダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (RowM04 == null)
			{
				return;
			}
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileName = "";
			ofd.DefaultExt = "*.jpg;*.jpeg;*.png;*.bmp;*.gif";
			if (ofd.ShowDialog() == true)
			{
				ChangeImageData(ofd.FileName);
			}
		}

		private void ChangeImageData(string filename)
		{
			using (FileStream rdr = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				try
				{
					long size = rdr.Length;
					if (size > (2*1024*1024))
					{
						// 2MBまでとする
						MessageBox.Show(string.Format("画像ファイルサイズが 2MB を超えています。\r\n[{0}]", size));
						return;
					}
					byte[] img = new byte[size];
					rdr.Read(img, 0, (int)size);
					this.DriverPicData = img;
					NotifyPropertyChanged("DriverPicData");
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("画像の読み込みに失敗しました。\r\n[{0}]", ex.Message));
				}
			}
		}


		/// <summary>
		/// F8 リボン　印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF8Key(object sender, KeyEventArgs e)
		{
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
			ScreenClear();
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

		public override void OnF12Key(object sender, KeyEventArgs e)
		{
			Delete();
		}

		private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue == false)
			{
				(sender as Button).IsEnabled = true;
			}
		}

		private void CheckBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				(sender as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			}
		}



	}
}
