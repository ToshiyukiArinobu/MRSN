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
        public class ConfigMST07010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST07010 frmcfg = null;

        #endregion

        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M09_HIN_UC";
        //対象テーブル検索用類似
        private const string RTargetTableNm = "RM09_HIN_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M09_HIN_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M09_HIN_DEL";
        //自動採番
        private const string GetNextID = "M09_HIN_NEXT";

        #endregion

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

        private decimal? _商品重量 = null;
        public decimal? 商品重量
        {
            get { return this._商品重量; }
            set { this._商品重量 = value; NotifyPropertyChanged(); }
        }
        private decimal? _商品才数 = null;
        public decimal? 商品才数
        {
            get { return this._商品才数; }
            set { this._商品才数 = value; NotifyPropertyChanged(); }
        }

        //マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        private int? _類似ID = null;
        public int? 類似ID
        {
            get { return this._類似ID; }
            set { this._類似ID = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region MST07010
        /// <summary>
        /// 商品マスタ入力
        /// </summary>
        public MST07010()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region LOAD時
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
            frmcfg = (ConfigMST07010)ucfg.GetConfigValue(typeof(ConfigMST07010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST07010();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 100)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 100)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            //初期化
            ScreenClear();

            //F1 検索取得
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { null, typeof(SCH07010) });

        }
        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            this.MaintenanceMode = string.Empty;
            txt類似ID.Visibility = Visibility.Collapsed;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            ResetAllValidation();

            SetFocusToTopControl();

            MstData = null;
            商品ID = string.Empty;
            商品名 = string.Empty;
            商品ｶﾅ = string.Empty;
            商品単位 = string.Empty;
            商品重量 = null;
            商品才数 = null;

        }
        #endregion

        #region データ受信メソッド

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
                        //検索時処理
                        case TargetTableNm:
                            if (tbl.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                                {
                                    //this.ErrorMessage = "既に削除されているデータです。";
                                    //MessageBox.Show("既に削除されているデータです。");
                                    //ScreenClear();
                                    //return;

                                    MessageBoxResult result = MessageBox.Show("既に削除されているデータです。\nこのコードを復元しますか？",
                                                                                                                    "質問",
                                                                                                                   MessageBoxButton.YesNo,
                                                                                                                   MessageBoxImage.Exclamation,
                                                                                                                   MessageBoxResult.No);

                                    if (result == MessageBoxResult.No)
                                    {
                                        ScreenClear();
                                        return;
                                    }

                                }
                                SetTblData(tbl);
                            }

                            //リボンの状態表示
                            if (string.IsNullOrEmpty(商品名))
                            {
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                txt類似ID.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                                txt類似ID.Visibility = Visibility.Collapsed;
                            }

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();
                            break;

                        //検索時処理
                        case RTargetTableNm:
                            類似ID = null;
                            if (tbl.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                                {
                                    this.ErrorMessage = "既に削除されているデータです。";
                                    MessageBox.Show("既に削除されているデータです。");
                                    ScreenClear();
                                    return;
                                }
                                RSetTblData(tbl);
                            }
                            break;

                        //更新時処理
                        case TargetTableNmUpdate:
                            ScreenClear();
                            break;

                        //削除時処理
                        case TargetTableNmDelete:
                            ScreenClear();
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
                                int iNextCode = (int)data;
                                商品ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                txt類似ID.Visibility = Visibility.Visible;
                                SetFocusToTopControl();
                            }
                            break;
                        case TargetTableNmUpdate:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("商品ID: " + 商品ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i商品ID = AppCommon.IntParse(商品ID);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { i商品ID
                                                                                                                                    ,商品名
                                                                                                                                    ,商品ｶﾅ
                                                                                                                                    ,商品単位
                                                                                                                                    ,商品重量
                                                                                                                                    ,商品才数
                                                                                                                                    ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                                    ,true}));

                                break;
                            }

                            ScreenClear();
                            break;
                        case TargetTableNmDelete:
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
        #endregion

        #region 画面反映
        /// <summary>
        /// データを画面反映
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            //取得した値を表示
            MstData = tbl.Rows[0];
            商品ID = tbl.Rows[0]["商品ID"].ToString();
            商品名 = tbl.Rows[0]["商品名"].ToString();
            商品ｶﾅ = tbl.Rows[0]["かな読み"].ToString();
            商品単位 = tbl.Rows[0]["単位"].ToString();
            商品重量 = Convert.ToDecimal(tbl.Rows[0]["商品重量"]);
            商品才数 = Convert.ToDecimal(tbl.Rows[0]["商品才数"]);

        }
        /// <summary>
        /// データを画面反映類似用
        /// </summary>
        /// <param name="tbl"></param>
        private void RSetTblData(DataTable tbl)
        {
            //取得した値を表示
            MstData = tbl.Rows[0];
            商品名 = tbl.Rows[0]["商品名"].ToString();
            商品ｶﾅ = tbl.Rows[0]["かな読み"].ToString();
            商品単位 = tbl.Rows[0]["単位"].ToString();
            商品重量 = Convert.ToDecimal(tbl.Rows[0]["商品重量"]);
            商品才数 = Convert.ToDecimal(tbl.Rows[0]["商品才数"]);

        }
        #endregion

        #region エラーメッセージ
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region << <> >>ボタン
        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
        {

            //先頭データ検索
            try
            {
                //商品マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));

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
            try
            {
                //先頭行出力
                if (string.IsNullOrEmpty(商品ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                //前データ検索
                int iSyasyuId = 0;
                if (int.TryParse(商品ID, out iSyasyuId))
                {
                    //商品マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, -1 }));

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
                //最後尾出力
                if (string.IsNullOrEmpty(商品ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 2 }));
                    return;
                }

                //次データ検索
                int iSyasyuId = 0;
                if (int.TryParse(商品ID, out iSyasyuId))
                {
                    //商品マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, 1 }));

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
                //商品マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));

            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region リボン

        /// <summary>
        /// F1 リボン　検索
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
        /// F2 リボン 一覧入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
        }



        /// <summary>
        /// F8 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST07020 mst07020 = new MST07020();
            mst07020.ShowDialog(this);
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
            //メッセージボックス
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            //OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }
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
            try
            {
                //MstDataに値がなければメッセージ表示
                if (string.IsNullOrEmpty(this.商品ID))
                {
                    this.ErrorMessage = "登録内容がありません。";
                    MessageBox.Show("登録内容がありません。");
                    return;
                }

                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                {
                    this.ErrorMessage = "新規登録データは削除できません。";
                    MessageBox.Show("新規登録データは削除できません。");
                    SetFocusToTopControl();
                    return;
                }

                //メッセージボックス
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
            catch
            {
                this.ErrorMessage = "削除処理が出来ませんでした。";
            }
        }

        #endregion

        #region イベント

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

        /// <summary>
        /// 商品IDテキストボックスロストフォーカス
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
                        //自動採番
                        if (string.IsNullOrEmpty(商品ID))
                        {
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));
                            return;
                        }

                        if (string.IsNullOrEmpty(商品ID))
                        {
                            this.ErrorMessage = "商品IDは必須入力項目です。";
                            MessageBox.Show("商品IDは必須入力項目です。");
                            return;
                        }

                        int i商品ID = 0;

                        if (!int.TryParse(商品ID, out i商品ID))
                        {
                            this.ErrorMessage = "商品IDの入力形式が不正です。";
                            MessageBox.Show("商品IDの入力形式が不正です。");
                            return;
                        }


                        //最後尾検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i商品ID, 0 }));
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

        //商品重量
        private void UcKabelTextBox_Previewkeydown(object sender, KeyEventArgs e)
        {
            if (ShohinJyuryo.Text == string.Empty)
            {
                商品重量 = 0;
            }
            else
            {
                return;
            }
        }

        //商品才数
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ShohinSaisu.Text == string.Empty)
                {
                    商品才数 = 0;
                }
                OnF9Key(null, null);
            }
        }

        #endregion

        #region 処理メソッド

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {
                SetFocusToTopControl();
                int i商品ID = 0;
                if (string.IsNullOrEmpty(商品ID))
                {
                    this.ErrorMessage = "商品IDは入力必須項目です。";
                    MessageBox.Show("商品IDは入力必須項目です。");
                    return;
                }

                if (!int.TryParse(商品ID, out i商品ID))
                {
                    this.ErrorMessage = "商品IDの入力形式が不正です。";
                    MessageBox.Show("商品IDの入力形式が不正です。");
                    return;
                }

                if (string.IsNullOrEmpty(商品名))
                {
                    this.ErrorMessage = "商品名の入力形式が不正です。";
                    MessageBox.Show("商品名の入力形式が不正です。");
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
                if (yesno == MessageBoxResult.Yes)
                {
                    //値を送信
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { i商品ID
                                                                                                        ,商品名
                                                                                                        ,商品ｶﾅ
                                                                                                        ,商品単位
                                                                                                        ,商品重量
                                                                                                        ,商品才数
                                                                                                        ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                        ,false
                                                                                                        }));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }


        //削除メソッド
        private void Delete()
        {
            try
            {
                int i商品ID = 0;
                if (!int.TryParse(商品ID, out i商品ID))
                {
                    this.ErrorMessage = "商品IDの入力形式が不正です。";
                    MessageBox.Show("商品IDの入力形式が不正です。");
                    return;
                }
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { i商品ID }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
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
