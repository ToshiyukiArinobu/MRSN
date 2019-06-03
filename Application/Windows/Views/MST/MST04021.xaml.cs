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
using System.Text.RegularExpressions;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// シリーズマスタ保守入力
    /// </summary>
    public partial class MST04021 : WindowMasterMainteBase
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
        public class ConfigMST04021 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST04021 frmcfg = null;



        #endregion

        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M15_SERIES";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M15_SERIES_UPDATE";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M15_SERIES_DELETE";
        //<< <Pageing> >>
        private const string GetPagingCode = "M15_SERIES_GetPagingCode";

        #endregion

        #region 表示用データ

        private string _シリーズコード = string.Empty;
        public string シリーズコード
        {
            get { return this._シリーズコード; }
            set { this._シリーズコード = value; NotifyPropertyChanged(); }
        }

        private string _シリーズ名 = string.Empty;
        public string シリーズ名
        {
            get { return this._シリーズ名; }
            set { this._シリーズ名 = value; NotifyPropertyChanged(); }
        }

        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }

        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST04021
        /// <summary>
        /// シリーズマスタ保守入力
        /// </summary>
        public MST04021()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

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

            frmcfg = (ConfigMST04021)ucfg.GetConfigValue(typeof(ConfigMST04021));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST04021();
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
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { null, typeof(SCHM15_SERIES) });
        }

        #region データ受信
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
                    //通常データ検索
                    case TargetTableNm:
                        StrData(tbl);

                        break;

                    //更新処理受信
                    case TargetTableNmUpdate:
                        if ((int)data == -1)
                        {
                            MessageBoxResult result = MessageBox.Show("シリーズコード:" + シリーズコード + "は既に使われています。", "確認");
                            break;
                        }
                        MessageBox.Show(AppConst.COMPLETE_UPDATE);
                        //コントロール初期化
                        ScreenClear();
                        break;


                    //削除処理受信
                    case TargetTableNmDelete:
                        MessageBox.Show(AppConst.COMPLETE_DELETE);
                        //コントロール初期化
                        ScreenClear();
                        break;

                    case GetPagingCode:
                        this.ErrorMessage = string.Empty;
                        if (tbl == null)
                        {
                            this.ErrorMessage = "データが見つかりません。";
                            return;
                        }

                        if (tbl.Rows.Count > 0)
                        {
                            //編集ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            シリーズコード = tbl.Rows[0]["シリーズコード"].ToString();
                            シリーズ名 = tbl.Rows[0]["シリーズ名"].ToString();
                            かな読み = tbl.Rows[0]["かな読み"].ToString();
                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();
                            ResetAllValidation();
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }



        //データ受信メソッド
        private void StrData(DataTable tbl)
        {
            //データがある場合
            if (tbl.Rows.Count > 0)
            {
                //削除日時がnullのデータの時
                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
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
                        return;
                    }
                }

                //表示データ
                MstData = tbl.Rows[0];
                シリーズコード = tbl.Rows[0]["シリーズコード"].ToString();
                シリーズ名 = tbl.Rows[0]["シリーズ名"].ToString();
                かな読み = tbl.Rows[0]["かな読み"].ToString();
                DateTime Wk;
                削除日時 = DateTime.TryParse(tbl.Rows[0]["削除日時"].ToString(), out Wk) ? Wk : (DateTime?)null;

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }
            else
            {
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                //矢印＜対応
                if (string.IsNullOrEmpty(シリーズ名))
                {
                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                }
            }
        }
        #endregion

        #region エラー表示用
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
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
        /// F8 リスト一覧表　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST04021_1 mst04021_1 = new MST04021_1();
            mst04021_1.ShowDialog(this);
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
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消しても宜しいですか？"
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
            if (string.IsNullOrEmpty(this.シリーズコード))
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


        #endregion

        #region イベント

        /// <summary>
        /// 主キー検索時完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextSeriesCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    // 画面にエラーがあった場合
                    if (!base.CheckKeyItemValidation())
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(シリーズコード))
                    {
                        //存在するシリーズマスタ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { シリーズコード }));
                    }
                    else
                    {
                        e.Handled = true;
                        this.ErrorMessage = "シリーズコードを入力してください。";
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
        /// 登録項目の入力完了時に動作
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

        #endregion

        #region 処理メソッド
        private void ScreenClear()
        {
            シリーズコード = string.Empty;
            シリーズ名 = string.Empty;
            かな読み = string.Empty;
            削除日時 = null;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);

            //ボタンはFalse
            //btnEnableChange(true);

            SetFocusToTopControl();
            ResetAllValidation();
        }



        private void Update()
        {

            try
            {
                // 全項目エラーチェック
                if (!base.CheckAllValidation())
                {
                    SetFocusToTopControl();
                    return;
                }

                string updateMessage;

                // 削除されたデータを登録する場合
                if (削除日時 != null)
                {
                    updateMessage = AppConst.RESTORE_DATA;
                }
                else
                {
                    updateMessage = "入力内容を登録しますか？";
                }

                var yesno = MessageBox.Show(updateMessage, "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {シリーズコード
                                                                                                                        ,シリーズ名
                                                                                                                        ,かな読み
                                                                                                                        ,ccfg.ユーザID
                                                                                                                        ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                        ,false }));
                }
                else
                {
                    return;
                }

            }
            catch
            {
                //更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";
            }
            finally
            {
            }
        }

        //IME
        private void sekisai_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // 数字(0-9)は入力可
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            // 上記以外は入力不可
            e.Handled = true;
        }


        /// <summary>
        /// 削除メソッド
        /// </summary>
        private void Delete()
        {
            try
            {

                if (!string.IsNullOrEmpty(シリーズコード))
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { シリーズコード, ccfg.ユーザID }));
                }
                else
                {
                    this.ErrorMessage = "シリーズコードを入力してください。";
                }
            }
            catch
            {
                //削除登録エラー
                this.ErrorMessage = "削除時にエラーが発生しました。";
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

        #region コード送りボタン
        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstIdButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetPagingCode, new object[] { null, 0 }));
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
            try
            {
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetPagingCode, new object[] { シリーズコード, 1 }));
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
            try
            {


                if (!string.IsNullOrEmpty(シリーズコード))
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GetPagingCode, new object[] { シリーズコード, 2 }));
                }
                else
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GetPagingCode, new object[] { null, 0 }));

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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetPagingCode, new object[] { null, 3 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        /// <summary>
        /// バイト長を指定して文字列の先頭を切り出す
        /// </summary>
        /// <param name="value">対象となる文字列</param>
        /// <param name="encode">文字コードを指定する
        /// ShiftJISを明示的に指定したい場合は Encoding.GetEncoding(932)
        /// </param>
        /// <param name="size">バイト長</param>
        /// <returns></returns>
        public static bool GetStringByteErrMessage(string value, System.Text.Encoding encoding, int size)
        {
            if (encoding.GetByteCount(value) <= size)
            {
                // 指定サイズ以下の場合そのまま返す
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
