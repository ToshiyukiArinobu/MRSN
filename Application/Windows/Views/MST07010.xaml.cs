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

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 商品マスタ入力
    /// </summary>
    public partial class MST07010 : WindowMasterMainteBase
    {
        //対象テーブル検索用
        private const string TargetTableNm = "M09_HIN_UC";
        //対象テーブルボタン検索用
        private const string TargetTableNmBtn = "M09_HIN_BTN";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M09_HIN_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M09_HIN_DEL";


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

        #region バインド用プロパティ

        private string _商品ID = string.Empty;
        public string 商品ID
        {
            get { return this._商品ID; }
            set { this._商品ID = value; NotifyPropertyChanged(); }
        }
        private string _商品名 = string.Empty;
        public string 商品名
        {
            get { return this._商品名; }
            set { this._商品名 = value; NotifyPropertyChanged(); }
        }
        private string _商品ｶﾅ = string.Empty;
        public string 商品ｶﾅ
        {
            get { return this._商品ｶﾅ; }
            set { this._商品ｶﾅ = value; NotifyPropertyChanged(); }
        }

        private string _商品単位 = string.Empty;
        public string 商品単位
        {
            get { return this._商品単位; }
            set { this._商品単位 = value; NotifyPropertyChanged(); }
        }

        private string _商品重量 = string.Empty;
        public string 商品重量
        {
            get { return this._商品重量; }
            set { this._商品重量 = value; NotifyPropertyChanged(); }
        }
        private string _商品才数 = string.Empty;
        public string 商品才数
        {
            get { return this._商品才数; }
            set { this._商品才数 = value; NotifyPropertyChanged(); }
        }

        #endregion
        /// <summary>
        /// 商品マスタ入力
        /// </summary>
        public MST07010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
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


        private void SearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            DataRequest();
        }

        void DataRequest()
        {
            try
            {
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// データ受信メソッド
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
                    //検索時処理
                    case TargetTableNm:
                    case TargetTableNmBtn:
                        if (tbl.Rows.Count == 0)
                        {
                            ///	MstData = tbl.NewRow();
                        }
                        else
                        {
                            MstData = tbl.Rows[0];

                            商品ID = MstData["商品ID"].ToString();
                        }
                        //商品ID変更不可
                        LabelTextSyouhinId.Text1IsReadOnly = true;
                        break;

                    //更新時処理
                    case TargetTableNmUpdate:
                        MessageBox.Show("データの更新が完了しました。");
                        //コントロール初期化
                        UcInitialize();

                        break;

                    //更新時処理
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
                this.ErrorMessage = ex.Message;
            }
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 0 }));

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// １つ前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            int triCD = 0;
            try
            {
                //前データ検索
                try
                {
                    int iSyouhinId = 0;
                    if (商品ID == "")
                    {
                        //先頭データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 0 }));
                    }
                    else
                    {
                        if (int.TryParse(商品ID, out iSyouhinId))
                        {
                            //商品マスタ
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { iSyouhinId, -1 }));
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// １つ次のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //次データ検索
                try
                {
                    int iSyouhinId = 0;
                    if (商品ID == "")
                    {
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { iSyouhinId, 1 }));
                    }
                    else
                    {
                        if (int.TryParse(商品ID, out iSyouhinId))
                        {
                            //車輌マスタ
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { iSyouhinId, 1 }));
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 最後尾のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
                try
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 1 }));

                }
                catch (Exception)
                {
                    return;
                }
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
                int iSyouhinId = 0;
                if (int.TryParse(商品ID, out iSyouhinId))
                {
                    MstData["商品ID"] = iSyouhinId;

                    DataTable dtSyouhin = new DataTable(TargetTableNmUpdate);
                    foreach (DataColumn col in this.MstData.Table.Columns)
                    {
                        DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                        newcol.AllowDBNull = col.AllowDBNull;
                        dtSyouhin.Columns.Add(newcol);
                    }
                    DataRow row = dtSyouhin.NewRow();
                    foreach (DataColumn col in dtSyouhin.Columns)
                    {
                        row[col.ColumnName] = this.MstData[col.ColumnName];
                    }
                    dtSyouhin.Rows.Add(row);


                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, dtSyouhin));
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
            int iSyouhinId = 0;
            if (int.TryParse(商品ID, out iSyouhinId))
            {
                MstData["商品ID"] = iSyouhinId;

                DataTable dtSyouhin = new DataTable(TargetTableNmDelete);
                foreach (DataColumn col in this.MstData.Table.Columns)
                {
                    DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                    newcol.AllowDBNull = col.AllowDBNull;
                    dtSyouhin.Columns.Add(newcol);
                }
                DataRow row = dtSyouhin.NewRow();
                foreach (DataColumn col in dtSyouhin.Columns)
                {
                    row[col.ColumnName] = this.MstData[col.ColumnName];
                }
                dtSyouhin.Rows.Add(row);


                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, dtSyouhin));
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
            MST07020 mst07020 = new MST07020();

            mst07020.ShowDialog();
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

        #region イベント
        /// <summary>
        /// 商品IDテキストボックスロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iSyouhinId = 0;
                if (int.TryParse(商品ID, out iSyouhinId))
                {
                    //商品マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyouhinId }));
                }
            }
            catch (Exception)
            {
                return;
            }

        }
        #endregion


        #region 処理メソッド
        private void UcInitialize()
        {
            商品ID = string.Empty;
            MstData = null;

            LabelTextSyouhinId.Text1IsReadOnly = false;

            //フォーカス設定
            SetFocusToTopControl();
        }

        #endregion

    }
}
