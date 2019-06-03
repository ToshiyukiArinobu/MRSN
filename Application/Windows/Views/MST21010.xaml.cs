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
    /// 支払先別距離別運賃マスタ
    /// </summary>
    public partial class MST21010 : WindowReportBase
    {
        //対象テーブル検索用
        private const string TargetTableNm = "M03_YTAN3_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M03_YTAN3_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M03_YTAN3_DEL";


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
        private string _距離 = string.Empty;
        public string 距離
        {
            get { return this._距離; }
            set { this._距離 = value; NotifyPropertyChanged(); }
        }
        private string _重量 = string.Empty;
        public string 重量
        {
            get { return this._重量; }
            set { this._重量 = value; NotifyPropertyChanged(); }
        }
        private string _運賃 = string.Empty;
        public string 運賃
        {
            get { return this._運賃; }
            set { this._運賃 = value; NotifyPropertyChanged(); }
        }
        #endregion
        /// <summary>
        /// 支払先別距離別運賃マスタ
        /// </summary>
        public MST21010()
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
                            距離 = MstData["距離"].ToString();
                            重量 = MstData["重量"].ToString();
                        }
                        //主キー変更不可
                        LabelTextShiharaisaki.Text1IsReadOnly = true;
                        LabelTextkyori.cIsReadOnly = true;
                        LabelTextomosa.cIsReadOnly = true;
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
                int iKyori = 0;
                decimal iOmosa = 0;

                if (int.TryParse(支払先ID, out iTokuisakiId)
                    && int.TryParse(距離, out iKyori)
                    && decimal.TryParse(重量, out iOmosa))
                {
                    //マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iTokuisakiId, iKyori, iOmosa }));
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
            距離 = string.Empty;
            重量 = string.Empty;
            LabelTextShiharaisaki.Text1IsReadOnly = false;
            LabelTextkyori.cIsReadOnly = false;
            LabelTextomosa.cIsReadOnly = false;
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
                int iShiharaiId = 0;
                int iKyori = 0;
                decimal iOmosa = 0;

                if (int.TryParse(支払先ID, out iShiharaiId)
                    && int.TryParse(距離, out iKyori)
                    && decimal.TryParse(重量, out iOmosa))
                {
                    MstData["取引先ID"] = iShiharaiId;
                    MstData["距離"] = iKyori;
                    MstData["重量"] = iOmosa;


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
            int iKyori = 0;
            decimal iOmosa = 0;

            if (int.TryParse(支払先ID, out iShiharaisakiId)
                && int.TryParse(距離, out iKyori)
                && decimal.TryParse(重量, out iOmosa))
            {
                MstData["取引先ID"] = iShiharaisakiId;
                MstData["距離"] = iKyori;
                MstData["重量"] = iOmosa;

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
