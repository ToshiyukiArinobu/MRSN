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
using System.Reflection;

using System.Windows.Threading;

namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 振替入力
    /// </summary>
    public partial class DLY04011 : RibbonWindowViewBase
    {

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
        public class ConfigDLY04011 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY04011 frmcfg = null;

        #endregion

        #region 定数定義

        /// <summary>移動情報取得</summary>
        private const string DLY04011_GetData = "DLY04011_GetData";
        /// <summary>移動情報更新</summary>
        private const string DLY04011_Update = "DLY04011_Update";
        /// <summary>伝票削除</summary>
        private const string DLY04011_Delete = "DLY04011_Delete";

        /// <summary>自社品番情報取得</summary>
        private const string GetMyProduct = "UcMyProduct";
        /// <summary>更新用_在庫数チェック</summary>
        private const string UpdateData_StockCheck = "DLY04011_UpdateData_CheckStock";
        /// <summary>削除用_在庫数チェック</summary>
        private const string DeleteData_StockCheck = "DLY04011_DeleteData_CheckStock";

        /// <summary>移動ヘッダ テーブル名</summary>
        private const string T05_HEADER_TABLE_NAME = "T05_IDOHD";
        /// <summary>移動明細 テーブル名</summary>
        private const string T05_SYUKO_TABLE_NAME = "T05_SYUKO_IDODTL";
        private const string T05_NYUKO_TABLE_NAME = "T05_NYUKO_IDODTL";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// 自社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        /// <summary>
        /// 商品分類 内包データ
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        #endregion

        #region バインディングデータ

        /// <summary>移動ヘッダ情報</summary>
        private DataRow _searchHeader;
        public DataRow SearchHeader
        {
            get { return _searchHeader; }
            set
            {
                _searchHeader = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>入庫明細情報</summary>
        private DataRow _inSearchDetail;
        public DataRow InSearchDetail
        {
            get { return _inSearchDetail; }
            set
            {
                _inSearchDetail = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>出庫明細情報</summary>
        private DataRow _outSearchDetail;
        public DataRow OutSearchDetail
        {
            get { return _outSearchDetail; }
            set
            {
                _outSearchDetail = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>色情報</summary>
        private string _出庫自社色情報 = string.Empty;
        public string 出庫自社色情報
        {
            get { return _出庫自社色情報; }
            set
            {
                _出庫自社色情報 = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>色情報</summary>
        private string _入庫自社色情報 = string.Empty;
        public string 入庫自社色情報
        {
            get { return _入庫自社色情報; }
            set
            {
                _入庫自社色情報 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << クラス変数定義 >>
        /// <summary>
        /// 1:入庫、2:出庫
        /// </summary>
        private int 入出庫フラグ = 0;

        #endregion

        #region << 初期処理群 >>

        /// <summary>
        /// 振替入力　コンストラクタ
        /// </summary>
        public DLY04011()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            #region "権限関係"

            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                // RibbonWindowViewBaseのプロパティに設定
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            frmcfg = (ConfigDLY04011)ucfg.GetConfigValue(typeof(ConfigDLY04011));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY04011();
                ucfg.SetConfigValue(frmcfg);
                //画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
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

            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { null, typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            ScreenClear();
            ChangeKeyItemChangeable(true);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt会社名.Text1 = ccfg.自社コード.ToString();
            this.txt会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.txt伝票番号.Focus();

        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// データ受信処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case DLY04011_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            this.txt移動日.Focus();
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    case UpdateData_StockCheck:
                        // 在庫数チェック結果受信
                        Dictionary<int, string> updateList = data as Dictionary<int, string>;
                        string zaiUpdateMessage = AppConst.CONFIRM_UPDATE;
                        var zaiMBImage = MessageBoxImage.Question;

                        int outNum = OutSearchDetail.Field<int>("行番号");
                        int inNum = InSearchDetail.Field<int>("行番号");

                        if (updateList.ContainsKey(outNum) == true && updateList.ContainsKey(inNum) == true)
                        {
                            zaiMBImage = MessageBoxImage.Warning;
                            zaiUpdateMessage = "入庫と出庫の在庫がマイナスになりますが、\r\n登録してもよろしいでしょうか？";
                        }
                        else if (updateList.ContainsKey(outNum) == true)
                        {
                            zaiMBImage = MessageBoxImage.Warning;
                            zaiUpdateMessage = "出庫の在庫がマイナスになりますが、\r\n登録してもよろしいでしょうか？";
                        }
                        else if (updateList.ContainsKey(inNum) == true)
                        {
                            zaiMBImage = MessageBoxImage.Warning;
                            zaiUpdateMessage = "入庫の在庫がマイナスになりますが、\r\n登録してもよろしいでしょうか？";
                        }



                        if (MessageBox.Show(zaiUpdateMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;

                        Update();
                        break;

                    case DeleteData_StockCheck:
                        // 在庫数チェック結果受信
                        Dictionary<int, string> deleteZaiList = data as Dictionary<int, string>;
                        string zaiDelMessage = "表示中の伝票を削除してもよろしいですか？";
                        var zaiDelMBImage = MessageBoxImage.Question;

                        if (deleteZaiList.Count > 0)
                        {
                            zaiDelMBImage = MessageBoxImage.Warning;
                            zaiDelMessage = "入庫の在庫がマイナスになりますが、\r\n表示中の伝票を削除してもよろしいでしょうか？";
                        }

                        if (MessageBox.Show(zaiDelMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiDelMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;

                        Delete();
                        break;

                    case DLY04011_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case DLY04011_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case GetMyProduct:

                        #region 自社品番 手入力時

                        DataTable ctbl = data as DataTable;
                        int? p商品コード = null;
                        int? p商品分類 = null;
                        string p商品名 = string.Empty;
                        string p自社色情報 = string.Empty;

                        // 対象データが存在する場合
                        if (ctbl != null && ctbl.Rows.Count > 0)
                        {
                            if (ctbl.Rows.Count == 1)
                            {
                                p商品コード = int.Parse(ctbl.Rows[0]["品番コード"].ToString());
                                p商品名 = ctbl.Rows[0]["自社品名"].ToString();
                                p商品分類 = int.Parse(ctbl.Rows[0]["商品分類"].ToString());
                                p自社色情報 = ctbl.Rows[0]["自社色"].ToString() + " " + ctbl.Rows[0]["自社色名"].ToString();
                            }
                            else
                            {
                                // 対象データが複数存在する場合
                                SCHM09_MYHIN myhin = new SCHM09_MYHIN();

                                myhin.txtCode.Text = 入出庫フラグ == 1 ? txt入庫自社品番.Text1 : txt出庫自社品番.Text1;
                                myhin.txtCode.IsEnabled = false;
                                myhin.TwinTextBox = new UcLabelTwinTextBox();
                                myhin.TwinTextBox.LinkItem = 0;
                                if (myhin.ShowDialog(this) == true)
                                {
                                    p商品コード = int.Parse(myhin.SelectedRowData["品番コード"].ToString());
                                    p商品名 = myhin.SelectedRowData["自社品名"].ToString();
                                    p商品分類 = int.Parse(myhin.SelectedRowData["商品分類"].ToString());
                                    p自社色情報 = myhin.SelectedRowData["自社色"].ToString() + " " + myhin.SelectedRowData["自社色名"].ToString();
                                }
                            }
                        }

                        if (入出庫フラグ == 1)
                        {
                            if (p商品コード == null)
                            {
                                InSearchDetail["品番コード"] = DBNull.Value;
                            }
                            else
                            {
                                InSearchDetail["品番コード"] = p商品コード;
                            }

                            if (p商品分類 == null)
                            {
                                InSearchDetail["商品分類"] = DBNull.Value;
                            }
                            else
                            {
                                InSearchDetail["商品分類"] = p商品分類;
                            }

                            txt入庫自社品番.Text2 = p商品名;
                            入庫自社色情報 = p自社色情報;

                        }
                        else if (入出庫フラグ == 2)
                        {
                            if (p商品コード == null)
                            {
                                OutSearchDetail["品番コード"] = DBNull.Value;
                            }
                            else
                            {
                                OutSearchDetail["品番コード"] = p商品コード;
                            }

                            if (p商品分類 == null)
                            {
                                OutSearchDetail["商品分類"] = DBNull.Value;
                            }
                            else
                            {
                                OutSearchDetail["商品分類"] = p商品分類;
                            }

                            txt出庫自社品番.Text2 = p商品名;
                            出庫自社色情報 = p自社色情報;
                        }

                        #endregion

                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                throw ex;
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
            DebugLog.WriteLine("=================================");
            DebugLog.WriteLine(message.GetParameters().GetValue(0));
            DebugLog.WriteLine("=================================");
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                var uctext = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(ctl as UIElement);

                if (uctext != null && uctext.DataAccessName == "M09_MYHIN")
                {

                    SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                    myhin.txtCode.Text = uctext.Text1;
                    myhin.TwinTextBox = new UcLabelTwinTextBox();
                    myhin.TwinTextBox.LinkItem = 0;
                    if (myhin.ShowDialog(this) == true)
                    {

                        // テキストボックス名で入庫か出庫を判定
                        if (uctext.Name == this.txt出庫自社品番.Name)
                        {
                            txt出庫自社品番.Text1 = myhin.SelectedRowData["自社品番"].ToString();
                            txt出庫自社品番.Text2 = myhin.SelectedRowData["自社品名"].ToString();
                            OutSearchDetail["品番コード"] = myhin.SelectedRowData["品番コード"].ToString();
                            OutSearchDetail["商品分類"] = int.Parse(myhin.SelectedRowData["商品分類"].ToString());
                            出庫自社色情報 = myhin.SelectedRowData["自社色"].ToString() + " " + myhin.SelectedRowData["自社色名"].ToString();
                            txt出庫賞味期限.Focus();
                        }
                        else if (uctext.Name == this.txt入庫自社品番.Name)
                        {
                            txt入庫自社品番.Text1 = myhin.SelectedRowData["自社品番"].ToString();
                            txt入庫自社品番.Text2 = myhin.SelectedRowData["自社品名"].ToString();
                            InSearchDetail["品番コード"] = int.Parse(myhin.SelectedRowData["品番コード"].ToString());
                            InSearchDetail["商品分類"] = int.Parse(myhin.SelectedRowData["商品分類"].ToString());
                            入庫自社色情報 = myhin.SelectedRowData["自社色"].ToString() + " " + myhin.SelectedRowData["自社色名"].ToString();
                            txt入庫賞味期限.Focus();
                        }
                    }
                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F9 登録
        /// <summary>
        /// F09　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {

            if (MaintenanceMode == null)
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation(true))
            {
                return;
            }

            // 業務入力チェックをおこなう
            if (!isFormValidation())
                return;

            //在庫ﾁｪｯｸ
            base.SendRequest(
               new CommunicationObject(
                   MessageType.RequestData,
                   UpdateData_StockCheck,
                   new object[] {
                            this.txt出庫倉庫.Text1,
                            setT05DataToDataSet(),
                            ccfg.ユーザID
                        }));

        }
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F10　リボン　入力取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            var yesno = MessageBox.Show(AppConst.CONFIRM_CANCEL, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
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
            if (this.MaintenanceMode == null)
                this.Close();

            else
            {
                if (DataUpdateVisible != Visibility.Hidden)
                {
                    var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (yesno == MessageBoxResult.No)
                        return;

                }

                this.Close();

            }

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
            if (this.MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
            {
                return;
            }

            int i伝票番号 = AppCommon.IntParse(this.txt伝票番号.Text);
            if (i伝票番号 == 0) return;

            // 入庫戻しの在庫ﾁｪｯｸ
            base.SendRequest(
               new CommunicationObject(
                   MessageType.RequestData,
                   DeleteData_StockCheck,
                   new object[] {
                          i伝票番号,
                        }));

        }

        #endregion

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 移動ヘッダ情報設定
            DataTable tblHd = ds.Tables[T05_HEADER_TABLE_NAME];
            if (tblHd.Select("出荷元倉庫コード > 0").Count() == 0)
            {
                // 新規追加
                SearchHeader = tblHd.Rows[0];

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
            }
            else
            {
                SearchHeader = tblHd.Rows[0];
                SearchHeader.AcceptChanges();

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }

            // 移動出庫明細
            DataTable tblOutDtl = ds.Tables[T05_SYUKO_TABLE_NAME];

            // データ状態から編集状態を設定
            if (tblOutDtl.Select("品番コード > 0").Count() == 0)
            {
                // 新規追加
                DataRow row = tblOutDtl.NewRow();
                row["伝票番号"] = AppCommon.IntParse(SearchHeader["伝票番号"].ToString());
                row["行番号"] = 2;
                row["数量"] = 0.00;
                tblOutDtl.Rows.Add(row);

                OutSearchDetail = tblOutDtl.Rows[0];
            }
            else
            {
                // 取得データをセット
                OutSearchDetail = tblOutDtl.Rows[0];
                OutSearchDetail.AcceptChanges();

                出庫自社色情報 = tblOutDtl.Rows[0]["自社色"].ToString() + " " + tblOutDtl.Rows[0]["自社色名"].ToString();
            }

            // 移動入庫明細
            DataTable tblInDtl = ds.Tables[T05_NYUKO_TABLE_NAME];
            if (tblInDtl.Select("品番コード > 0").Count() == 0)
            {
                // 新規追加
                DataRow row = tblInDtl.NewRow();
                row["伝票番号"] = AppCommon.IntParse(SearchHeader["伝票番号"].ToString());
                row["行番号"] = 1;
                row["数量"] = 0.00;
                tblInDtl.Rows.Add(row);
                InSearchDetail = tblInDtl.Rows[0];
            }
            else
            {
                // 取得データをセット
                InSearchDetail = tblInDtl.Rows[0];
                InSearchDetail.AcceptChanges();

                入庫自社色情報 = tblInDtl.Rows[0]["自社色"].ToString() + " " + tblInDtl.Rows[0]["自社色名"].ToString();
            }
        }

        /// <summary>
        /// 移動情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    DLY04011_Update,
                    new object[] {
                        setT05DataToDataSet(),
                        ccfg.ユーザID
                    }));

        }

        /// <summary>
        /// 売上情報の削除処理をおこなう
        /// </summary>
        private void Delete()
        {
            // 削除処理実行
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    DLY04011_Delete,
                    new object[] {
                        this.txt伝票番号.Text,
                        ccfg.ユーザID
                    }));

        }

        private DataSet setT05DataToDataSet()
        {

            DataSet dsResult = new DataSet();

            // 振替ヘッダー追加
            dsResult.Tables.Add(SearchHeader.Table.Copy());

            // 振替出庫明細追加
            dsResult.Tables.Add(OutSearchDetail.Table.Copy());

            // 振替入庫明細追加
            dsResult.Tables.Add(InSearchDetail.Table.Copy());

            return dsResult;

        }

        #endregion

        #region << 入力検証処理 >>

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.txt会社名.Text1))
            {
                base.ErrorMessage = "会社名が入力されていません。";
                return isResult;
            }

            return isResult = true;

        }

        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isFormValidation()
        {
            bool isResult = false;

            #region 【ヘッダ】必須入力チェック

            // 移動日
            if (string.IsNullOrEmpty(this.txt移動日.Text))
            {
                this.txt移動日.Focus();
                base.ErrorMessage = string.Format("移動日が入力されていません。");
                return isResult;
            }

            #endregion

            // 自社品番
            if (string.IsNullOrEmpty(this.txt出庫自社品番.Text2))
            {
                this.txt出庫自社品番.Focus();
                base.ErrorMessage = string.Format("出庫の自社品番が入力されていません。");
                return isResult;
            }

            // 【出庫明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            var 出庫商品分類 = OutSearchDetail.Field<int>("商品分類");
            var 出庫賞味期限 = OutSearchDetail.Field<DateTime?>("賞味期限");

            int stype = Convert.ToInt32(出庫商品分類);

            if (出庫賞味期限 == null)
            {
                // 賞味期限が空欄かつ商品分類が「食品」の場合はエラー
                if (stype.Equals((int)商品分類.食品))
                {
                    this.txt出庫賞味期限.Focus();
                    base.ErrorMessage = string.Format("出庫の自社品番の商品分類が『食品』の為、賞味期限の設定が必要です。");
                    return isResult;
                }
            }

            // 出庫数量
            if (string.IsNullOrEmpty(txt出庫数量.Text) || decimal.Parse(txt出庫数量.Text) == 0)
            {
                this.txt出庫数量.Focus();
                base.ErrorMessage = string.Format("出庫の数量が入力されていません。");
                return isResult;
            }

            // 出庫倉庫
            if (string.IsNullOrEmpty(this.txt出庫倉庫.Text1))
            {
                this.txt出庫倉庫.Focus();
                base.ErrorMessage = string.Format("出庫の倉庫が入力されていません。");
                return isResult;
            }

            // 自社品番
            if (string.IsNullOrEmpty(this.txt入庫自社品番.Text2))
            {
                this.txt入庫自社品番.Focus();
                base.ErrorMessage = string.Format("入庫の自社品番が入力されていません。");
                return isResult;
            }

            // 【入庫明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            var 入庫商品分類 = InSearchDetail.Field<int>("商品分類");
            var 入庫賞味期限 = InSearchDetail.Field<DateTime?>("賞味期限");

            int ntype = Convert.ToInt32(入庫商品分類);

            if (入庫賞味期限 == null)
            {
                // 賞味期限が空欄かつ商品分類が「食品」の場合はエラー
                if (ntype.Equals((int)商品分類.食品))
                {
                    this.txt入庫賞味期限.Focus();
                    base.ErrorMessage = string.Format("入庫の自社品番の商品分類が『食品』の為、賞味期限の設定が必要です。");
                    return isResult;
                }

            }

            // 入庫数量
            if (string.IsNullOrEmpty(txt入庫数量.Text) || decimal.Parse(txt入庫数量.Text) == 0)
            {
                this.txt入庫数量.Focus();
                base.ErrorMessage = string.Format("入庫の数量が入力されていません。");
                return isResult;
            }

            // 入庫倉庫
            if (string.IsNullOrEmpty(this.txt入庫倉庫.Text1))
            {
                this.txt入庫倉庫.Focus();
                base.ErrorMessage = string.Format("入庫の倉庫が入力されていません。");
                return isResult;
            }

            // 同一商品チェック
            int i出庫品番コード = OutSearchDetail.Field<int>("品番コード");
            int i入庫品番コード = InSearchDetail.Field<int>("品番コード");
            if (i出庫品番コード == i入庫品番コード && this.txt出庫賞味期限.Text == this.txt入庫賞味期限.Text && this.txt出庫倉庫.Text1 == this.txt入庫倉庫.Text1)
            {
                this.txt出庫自社品番.Focus();
                base.ErrorMessage = string.Format("出庫と入庫で同じ組み合わせの品番は登録できません。");
                return isResult;
            }

            return true;
        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            if (SearchHeader != null)
                SearchHeader = null;
            if (InSearchDetail != null)
                InSearchDetail = null;
            if (OutSearchDetail != null)
                OutSearchDetail = null;

            入庫自社色情報 = string.Empty;
            出庫自社色情報 = string.Empty;

            ChangeKeyItemChangeable(true);
            this.txt伝票番号.Focus();
            ResetAllValidation();

        }
        #endregion

        #region コントロールの入力可否変更
        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

        }
        #endregion

        #region << コントロールイベント >>

        #region 伝票番号イベント

        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // 検索項目検証
                if (!isKeyItemValidation())
                {
                    this.txt伝票番号.Focus();
                    return;
                }

                // 全項目エラーチェック
                if (!base.CheckKeyItemValidation())
                {
                    this.txt伝票番号.Focus();
                    return;
                }

                // 入力伝票番号で検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        DLY04011_GetData,
                        new object[] {
                            this.txt会社名.Text1,
                            this.txt伝票番号.Text,
                            ccfg.ユーザID
                        }));

            }

        }

        #endregion

        #region 自社品番イベント

        /// <summary>
        /// 自社品番が変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 自社品番_cText1Changed(object sender, RoutedEventArgs e)
        {
            var ctl = FocusManager.GetFocusedElement(this);
            var uctext = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(ctl as UIElement);

            // テキストボックス名で入庫か出庫を判定
            if (uctext.Name == this.txt出庫自社品番.Name)
            {
                txt出庫自社品番.Text2 = string.Empty;
                出庫自社色情報 = string.Empty;
            }
            else if (uctext.Name == this.txt入庫自社品番.Name)
            {
                txt入庫自社品番.Text2 = string.Empty;
                入庫自社色情報 = string.Empty;
            }
        }

        /// <summary>
        /// 自社品番（入庫）でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt自社品番_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                string p自社品番 = string.Empty;

                var ctl = FocusManager.GetFocusedElement(this);
                var uctext = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(ctl as UIElement);

                // テキストボックス名で入庫か出庫を判定
                if (uctext.Name == this.txt出庫自社品番.Name)
                {
                    p自社品番 = this.txt出庫自社品番.Text1;
                    入出庫フラグ = 2;
                }
                else if (uctext.Name == this.txt入庫自社品番.Name)
                {
                    p自社品番 = this.txt入庫自社品番.Text1;
                    入出庫フラグ = 1;
                }

                // 自社品番(または得意先品番)からデータを参照し、取得内容をテキストボックスに設定
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GetMyProduct,
                        new object[] {
                                p自社品番
                                ,null
                                ,null
                            }));
            }
        }

        #endregion

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY04011(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

        #endregion

    }

}
