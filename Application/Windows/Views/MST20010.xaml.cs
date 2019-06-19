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
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Reports.Preview;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 支払先別車種別単価マスタ
    /// </summary>
    public partial class MST20010 : WindowReportBase
    {
        //対象テーブル検索用
        private const string TargetTableNm = "M03_YTAN2_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M03_YTAN2_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M03_YTAN2_DEL";


        //表示用マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set
            {
                this._MstData = value;
                NotifyPropertyChanged();
            }
        }

        #region データバインド用プロパティ
        private string _支払先ID = string.Empty;
        public string 支払先ID
        {
            get { return this._支払先ID; }
            set { this._支払先ID = value; NotifyPropertyChanged(); }
        }
        private string _支払先名 = string.Empty;
        public string 支払先名
        {
            get { return this._支払先名; }
            set { this._支払先名 = value; NotifyPropertyChanged(); }
        }
        private string _車種ID = string.Empty;
        public string 車種ID
        {
            get { return this._車種ID; }
            set { this._車種ID = value; NotifyPropertyChanged(); }
        }
        private string _車種名 = string.Empty;
        public string 車種名
        {
            get { return this._車種名; }
            set { this._車種名 = value; NotifyPropertyChanged(); }
        }
        private string _発地ID = string.Empty;
        public string 発地ID
        {
            get { return this._発地ID; }
            set { this._発地ID = value; NotifyPropertyChanged(); }
        }
        private string _発地名 = string.Empty;
        public string 発地名
        {
            get { return this._発地名; }
            set { this._発地名 = value; NotifyPropertyChanged(); }
        }
        private string _着地ID = string.Empty;
        public string 着地ID
        {
            get { return this._着地ID; }
            set { this._着地ID = value; NotifyPropertyChanged(); }
        }
        private string _着地名 = string.Empty;
        public string 着地名
        {
            get { return this._着地名; }
            set { this._着地名 = value; NotifyPropertyChanged(); }
        }
        
        #endregion
        /// <summary>
        /// 支払先別車種別単価マスタ
        /// </summary>
        public MST20010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            // 画面が表示される最後の段階で処理すべき内容があれば、ここに記述します。
            base.MasterMaintenanceWindowList.Add("取引先", new List<Type> { typeof(MST01010), typeof(SCH14010) });
            base.MasterMaintenanceWindowList.Add("支払先", new List<Type> { typeof(MST01010), typeof(SCH15010) });
            base.MasterMaintenanceWindowList.Add("仕入先", new List<Type> { typeof(MST01010), typeof(SCH16010) });
            base.MasterMaintenanceWindowList.Add("請求内訳", new List<Type> { typeof(MST02010), null });
            base.MasterMaintenanceWindowList.Add("乗務員", new List<Type> { typeof(MST04010), typeof(SCH04010) });
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

        }

        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            // 基底クラスのエラー受信イベントを呼び出します。
            base.OnReveivedError(message);

            // 個別にエラー処理が必要な場合、ここに記述してください。

        }

        /// <summary>
        /// 取得データの正常受信時のイベント
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
                    case TargetTableNm:
                        if (tbl.Rows.Count == 0)
                        {
                            MstData = tbl.NewRow();
                        }
                        else
                        {
                            MstData = tbl.Rows[0];


                            支払先ID = MstData["取引先ID"].ToString();
                            車種ID = MstData["車種ID"].ToString();
                            発地ID = MstData["発地ID"].ToString();
                            着地ID = MstData["着地ID"].ToString();
                        }
                        //主キー変更不可
                        LabelTextShiharaisaki.Text1IsReadOnly = true;
                        LabelTextSyasyu.Text1IsReadOnly = true;
                        LabelTextHatuti.Text1IsReadOnly = true;
                        LabelTextTyakuti.Text1IsReadOnly = true;
                        break;

                    //更新時処理
                    case TargetTableNmUpdate:
                        MessageBox.Show("データの更新が完了しました。");
                        //コントロール初期化
                        UcInitialize();

                        break;

                    //削除時処理
                    case TargetTableNmDelete:
                        MessageBox.Show("データの削除が完了しました。");
                        //コントロール初期化
                        UcInitialize();

                        break;

                    default:
                        break;
				}
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region リボン
        /// <summary>
        /// F6 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {

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
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("マスタに存在していない項目があります。");
                return;
            }
            Update();
        }

        /// <summary>
        /// F10　リボン入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            UcInitialize();
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

        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region 処理メソッド
        /// <summary>
        /// 主キーテキストボックスロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iTokuisakiId = 0;
                int iSyasyuId = 0;
                int iHatutiId = 0;
                int iTyakutiId = 0;
                if (int.TryParse(支払先ID, out iTokuisakiId)
                    && int.TryParse(車種ID, out iSyasyuId)
                    && int.TryParse(発地ID, out iHatutiId)
                    && int.TryParse(着地ID, out iTyakutiId)
                    )
                {
                    //マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iTokuisakiId, iSyasyuId, iHatutiId, iTyakutiId }));
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        private void UcInitialize()
        {

            MstData = null;
            支払先ID = string.Empty;
            車種ID = string.Empty;
            発地ID = string.Empty;
            着地ID = string.Empty;
            LabelTextShiharaisaki.Text1IsReadOnly = false;
            LabelTextSyasyu.Text1IsReadOnly = false;
            LabelTextHatuti.Text1IsReadOnly = false;
            LabelTextTyakuti.Text1IsReadOnly = false;
            //フォーカス設定
            SetFocusToTopControl();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {

            try
            {
                int iShiharaisakiId = 0;
                int iSyasyuId = 0;
                int iHatutiId = 0;
                int iTyakutiId = 0;
                if (int.TryParse(支払先ID, out iShiharaisakiId)
                    && int.TryParse(車種ID, out iSyasyuId)
                    && int.TryParse(発地ID, out iHatutiId)
                    && int.TryParse(着地ID, out iTyakutiId)
                    )
                {
                    MstData["取引先ID"] = iShiharaisakiId;
                    MstData["車種ID"] = iSyasyuId;
                    MstData["発地ID"] = iHatutiId;
                    MstData["着地ID"] = iTyakutiId;


                    DataTable dt = new DataTable(TargetTableNmUpdate);
                    foreach (DataColumn col in this.MstData.Table.Columns)
                    {
                        DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                        newcol.AllowDBNull = col.AllowDBNull;
                        dt.Columns.Add(newcol);
                    }
                    DataRow row = dt.NewRow();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row[col.ColumnName] = this.MstData[col.ColumnName];
                    }
                    dt.Rows.Add(row);


                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, dt));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        private void Delete()
        {
            int iShiharaisakiId = 0;
            int iSyasyuId = 0;
            int iHatutiId = 0;
            int iTyakutiId = 0;
            if (int.TryParse(支払先ID, out iShiharaisakiId)
                && int.TryParse(車種ID, out iSyasyuId)
                && int.TryParse(発地ID, out iHatutiId)
                && int.TryParse(着地ID, out iTyakutiId)
                )
            {
                MstData["取引先ID"] = iShiharaisakiId;
                MstData["車種ID"] = iSyasyuId;
                MstData["発地ID"] = iHatutiId;
                MstData["着地ID"] = iTyakutiId;

                DataTable dt = new DataTable(TargetTableNmDelete);
                foreach (DataColumn col in this.MstData.Table.Columns)
                {
                    DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                    newcol.AllowDBNull = col.AllowDBNull;
                    dt.Columns.Add(newcol);
                }
                DataRow row = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    row[col.ColumnName] = this.MstData[col.ColumnName];
                }
                dt.Rows.Add(row);


                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, dt));
            }
        }
        #endregion
    }
}
