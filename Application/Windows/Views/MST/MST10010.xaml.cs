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
    /// 部門マスタ入力
    /// </summary>
    public partial class MST10010 : WindowMasterMainteBase
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
        public class ConfigMST10010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST10010 frmcfg = null;

        #endregion

        #region 定数定義
        private const string TargetTableNm = "M71_BUM";
        private const string UpdateTable = "M71_BUM_UP";
        private const string DeleteTable = "M71_BUM_DEL";
        private const string GetNextID = "M71_BUM_NEXT";
        #endregion

        #region バインド用変数
        private string _自社部門コード = string.Empty;
        public string 自社部門コード
        {
            get { return this._自社部門コード; }
            set { this._自社部門コード = value; NotifyPropertyChanged(); }
        }
        private string _自社部門名 = string.Empty;
        public string 自社部門名
        {
            get { return this._自社部門名; }
            set { this._自社部門名 = value; NotifyPropertyChanged(); }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }

        private string _法人ナンバー = string.Empty;
        public string 法人ナンバー
        {
            get { return this._法人ナンバー; }
            set { this._法人ナンバー = value; NotifyPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// 部門マスタ入力
        /// </summary>
        public MST10010()
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

            frmcfg = (ConfigMST10010)ucfg.GetConfigValue(typeof(ConfigMST10010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST10010();
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

            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { null, typeof(SCH10010) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            自社部門コード = string.Empty;
            自社部門名 = string.Empty;
            かな読み = string.Empty;
            法人ナンバー = string.Empty;


            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            SetFocusToTopControl();

            this.ErrorMessage = string.Empty;
            ResetAllValidation();

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
            MST10020 view = new MST10020();
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

            if (string.IsNullOrEmpty(自社部門コード))
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

            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             ,MessageBoxResult.No );

            if (result == MessageBoxResult.Yes)
            {
                int i自社部門コード = 0;

                if (!int.TryParse(自社部門コード, out i自社部門コード))
                {
                    this.ErrorMessage = "自社部門IDの入力形式が不正です。";
                    MessageBox.Show("自社部門IDの入力形式が不正です。");
                    return;
                }


                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i自社部門コード }));
            }
            
        }
        #endregion

        #region リボン便利リンク

        /// <summary>
        /// リボン便利リンク　検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kensaku_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.yahoo.co.jp/");
        }

        /// <summary>
        /// リボン便利リンク　道路情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jartic.or.jp/");

        }

        /// <summary>
        /// リボン便利リンク　道路ナビボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroNabi_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://highway.drivenavi.net/");
        }

        /// <summary>
        /// リボン便利リンク　渋滞情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JyuutaiJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.mapfan.com/");
        }

        /// <summary>
        /// リボン便利リンク　天気ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tenki_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://weathernews.jp/");
        }

        /// <summary>
        /// リボン　WebHomeボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_WebHome_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C:\\Program Files (x86)/Internet Explorer/iexplore.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　メールボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Meil_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Program Files (x86)//Windows Live//Mail//wlmail.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　電卓ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Dentaku_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Windows//System32/calc.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        //部門データ取得
                        case TargetTableNm:
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

                            if (string.IsNullOrEmpty(自社部門名))
                            {
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }
                            else
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            }


                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();


                            break;

                        case UpdateTable:
                        case DeleteTable:
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
                                自社部門コード = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;
                        case UpdateTable:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("自社部門コード: " + 自社部門コード + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i自社部門コード = AppCommon.IntParse(自社部門コード);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i自社部門コード
                                                                                                                            ,自社部門名
                                                                                                                            ,かな読み
                                                                                                                            ,法人ナンバー
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
            
            自社部門コード = tbl.Rows[0]["自社部門ID"].ToString();
            自社部門名 = tbl.Rows[0]["自社部門名"].ToString();
            かな読み = tbl.Rows[0]["かな読み"].ToString();
            法人ナンバー = tbl.Rows[0]["法人ナンバー"].ToString();

        }


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
                //自社マスタ
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
                if (string.IsNullOrEmpty(自社部門コード))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                //前データ検索
                int iSyasyuId = 0;
                if (int.TryParse(自社部門コード, out iSyasyuId))
                {
                    //自社マスタ
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
                if (string.IsNullOrEmpty(自社部門コード))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 2 }));
                    return;
                }

                //次データ検索
                int iSyasyuId = 0;
                if (int.TryParse(自社部門コード, out iSyasyuId))
                {
                    //自社マスタ
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
                //自社マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));

            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion


        /// <summary>
        /// 部門コードキーダウンイベント時
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

                        if (string.IsNullOrEmpty(自社部門コード))
                        {
                            //自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));

                            return;
                        }
                        int i自社部門コード = 0;

                        if (!int.TryParse(自社部門コード, out i自社部門コード))
                        {
                            this.ErrorMessage = "自社部門コードの入力形式が不正です。";
                            MessageBox.Show("自社部門コードの入力形式が不正です。");
                            return;
                        }


                        //部門データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i自社部門コード, 0 }));
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


        #region 登録

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {

                int i自社部門コード = 0;


                if (!int.TryParse(自社部門コード, out i自社部門コード))
                {
                    this.ErrorMessage = "自社部門IDの入力形式が不正です。";
                    MessageBox.Show("自社部門IDの入力形式が不正です。");
                    return;
                }

                if (string.IsNullOrEmpty(自社部門名))
                {
                    this.ErrorMessage = "自社部門名は入力必須項目です。";
                    MessageBox.Show("自社部門名は入力必須項目です。");
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
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i自社部門コード
                                                                                                            ,自社部門名
                                                                                                            ,かな読み
                                                                                                            ,法人ナンバー
                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                            ,false}));
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました";
                return;
            }

        }
        #endregion

        /// 最終項目エンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                Update();
            }

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


    }
}
