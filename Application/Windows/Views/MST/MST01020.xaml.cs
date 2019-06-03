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
    /// 出荷先マスタ保守入力
	/// </summary>
	public partial class MST01020 : WindowMasterMainteBase
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
        public class ConfigMST01020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST01020 frmcfg = null;

        #endregion

        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M21_SYUK";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M21_SYUK_UPDATE";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M21_SYUK_DELETE";
        //住所取得
        private const string GET_UcZip = "UcZIP";

        #endregion

        #region 表示用データ

        private string _出荷先コード = string.Empty;
        public string 出荷先コード
        {
            get { return this._出荷先コード; }
            set { this._出荷先コード = value; NotifyPropertyChanged(); }
        }

        private string _出荷先名１ = string.Empty;
        public string 出荷先名１
        {
            get { return this._出荷先名１; }
            set { this._出荷先名１ = value; NotifyPropertyChanged(); }
        }
        private string _出荷先名２ = string.Empty;
        public string 出荷先名２
        {
            get { return this._出荷先名２; }
            set { this._出荷先名２ = value; NotifyPropertyChanged(); }
        }

        private string _出荷先カナ = string.Empty;
        public string 出荷先カナ
        {
            get { return this._出荷先カナ; }
            set { this._出荷先カナ = value; NotifyPropertyChanged(); }
        }

        private string _出荷先郵便番号 = string.Empty;
        public string 出荷先郵便番号
        {
            get { return this._出荷先郵便番号; }
            set { this._出荷先郵便番号 = value; NotifyPropertyChanged(); }
        }

        private string _出荷先住所１ = string.Empty;
        public string 出荷先住所１
        {
            get { return this._出荷先住所１; }
            set { this._出荷先住所１ = value; NotifyPropertyChanged(); }
        }
        private string _出荷先住所２ = string.Empty;
        public string 出荷先住所２
        {
            get { return this._出荷先住所２; }
            set { this._出荷先住所２ = value; NotifyPropertyChanged(); }
        }
        private string _出荷先電話番号 = string.Empty;
        public string 出荷先電話番号
        {
            get { return this._出荷先電話番号; }
            set { this._出荷先電話番号 = value; NotifyPropertyChanged(); }
        }
        private string _備考１ = string.Empty;
        public string 備考１
        {
            get { return this._備考１; }
            set { this._備考１ = value; NotifyPropertyChanged(); }
        }
        private string _備考２ = string.Empty;
        public string 備考２
        {
            get { return this._備考２; }
            set { this._備考２ = value; NotifyPropertyChanged(); }
        }

        //private int? _論理削除 = null;
        //public int? 論理削除
        //{
        //    get { return this._論理削除; }
        //    set { this._論理削除 = value; NotifyPropertyChanged(); }
        //}

        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }
        private string _削除者 = string.Empty;
        public string 削除者
        {
            get { return this._削除者; }
            set { this._削除者 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _登録日時 = null;
        public DateTime? 登録日時
        {
            get { return this._登録日時; }
            set { this._登録日時 = value; NotifyPropertyChanged(); }
        }
        private string _登録者 = string.Empty;
        public string 登録者
        {
            get { return this._登録者; }
            set { this._登録者 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _最終更新日時 = null;
        public DateTime? 最終更新日時
        {
            get { return this._最終更新日時; }
            set { this._最終更新日時 = value; NotifyPropertyChanged(); }
        }
        private string _最終更新者 = string.Empty;
        public string 最終更新者
        {
            get { return this._最終更新者; }
            set { this._最終更新者 = value; NotifyPropertyChanged(); }
        }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST01020
        /// <summary>
        /// 出荷先マスタ保守入力
        /// </summary>
        public MST01020()
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

            frmcfg = (ConfigMST01020)ucfg.GetConfigValue(typeof(ConfigMST01020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST01020();
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
            base.MasterMaintenanceWindowList.Add("M21_SYUK", new List<Type> { null, typeof(SCHM21_SYUK) });
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
                        if (tbl.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
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
                            //編集ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        }
                        else
                        {
                            tbl.Rows.Add();
                            MstData = tbl.Rows[0];
                            //新規ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        }

                        //キーをfalse
                        ChangeKeyItemChangeable(false);
                        SetFocusToTopControl();
                        break;

                    //更新処理受信
                    case TargetTableNmUpdate:
                        if ((int)data == -1)
                        {
                            MessageBoxResult result = MessageBox.Show("出荷先コード:" + 出荷先コード + "は既に使われています。", "確認");
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

                    case GET_UcZip:
                        // 住所情報取得
                        ShowAddresList(tbl);
                        break;


                }


            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }



        /// <summary>
        /// テーブルデータを各オブジェクトにセット
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            //表示データ
            MstData = tbl.Rows[0];

            出荷先コード = tbl.Rows[0]["出荷先コード"].ToString();
            出荷先名１ = tbl.Rows[0]["出荷先名１"].ToString();
            出荷先名２ = tbl.Rows[0]["出荷先名２"].ToString();
            出荷先カナ = tbl.Rows[0]["出荷先カナ"].ToString();
            出荷先郵便番号 = tbl.Rows[0]["出荷先郵便番号"].ToString();
            出荷先住所１ = tbl.Rows[0]["出荷先住所１"].ToString();
            出荷先住所２ = tbl.Rows[0]["出荷先住所２"].ToString();
            出荷先電話番号 = tbl.Rows[0]["出荷先電話番号"].ToString();
            備考１ = tbl.Rows[0]["備考１"].ToString();
            備考２ = tbl.Rows[0]["備考２"].ToString();
            DateTime Wk;
            削除日時 = DateTime.TryParse(tbl.Rows[0]["削除日時"].ToString(), out Wk) ? Wk : (DateTime?)null;
            登録日時 = DateTime.TryParse(tbl.Rows[0]["登録日時"].ToString(), out Wk) ? Wk : (DateTime?)null;
            最終更新日時 = DateTime.TryParse(tbl.Rows[0]["最終更新日時"].ToString(), out Wk) ? Wk : (DateTime?)null;
            削除者 = tbl.Rows[0]["削除者"].ToString();
            登録者 = tbl.Rows[0]["登録者"].ToString();
            最終更新者 = tbl.Rows[0]["最終更新者"].ToString();

            ChangeKeyItemChangeable(false);
            SetFocusToTopControl();
            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
        }
        #endregion

        #region 画面項目をMstDataに格納
        /// <summary>
        /// 画面項目を登録用データに格納
        /// </summary>
        private void DataToTable()
        {
            MstData["出荷先コード"] = 出荷先コード;
            MstData["出荷先名１"] = 出荷先名１;
            MstData["出荷先名２"] = 出荷先名２;
            MstData["出荷先カナ"] = 出荷先カナ;
            MstData["出荷先郵便番号"] = 出荷先郵便番号;
            MstData["出荷先住所１"] = 出荷先住所１;
            MstData["出荷先住所２"] = 出荷先住所２;
            MstData["出荷先電話番号"] = 出荷先電話番号;
            MstData["備考１"] = 備考１;
            MstData["備考２"] = 備考２;  
            //MstData["削除者"] = 削除者;
            //MstData["登録者"] = 登録者;
            //MstData["最終更新者"] = 最終更新者;
            //MstData["削除日時"] = 削除日時;
            //MstData["登録日時"] = 登録日時;
            //MstData["最終更新日時"] = 最終更新日時;

            NotifyPropertyChanged("MstData");
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
            MST01020_1 mst01020_1 = new MST01020_1();
            mst01020_1.ShowDialog(this);
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
            if (string.IsNullOrEmpty(this.出荷先コード))
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
        private void LabelTextSyukCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    // key項目のエラーチェック
                    if (!base.CheckKeyItemValidation())
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(出荷先コード))
                    {
                        //存在する出荷先マスタ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 出荷先コード, 0 }));
                    }
                    else
                    {
                        e.Handled = true;
                        this.ErrorMessage = "出荷先コードを入力してください。";
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
            出荷先コード = string.Empty;
            出荷先名１ = string.Empty;
            出荷先名２ = string.Empty;
            出荷先カナ = string.Empty;
            出荷先郵便番号 = string.Empty;
            出荷先住所１ = string.Empty;
            出荷先住所２ = string.Empty;
            出荷先電話番号 = string.Empty;
            備考１ = string.Empty;
            備考２ = string.Empty;
            //論理削除 = null;
            削除日時 = null;
            削除者 = null;
            登録日時 = null;
            登録者 = null;
            最終更新日時 = null;
            最終更新者 = null;

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
                string updateMessage;

                // 全項目エラーチェック
                if (!base.CheckAllValidation())
                {
                    SetFocusToTopControl();
                    return;
                }


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
   
                        DataToTable();
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {MstData
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

                if (!string.IsNullOrEmpty(出荷先コード))
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { 出荷先コード, ccfg.ユーザID }));
                }
                else
                {
                    this.ErrorMessage = "出荷先コードを入力してください。";
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

        /// <summary>
        /// 郵便番号変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YUBIN_NO_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (string.IsNullOrEmpty(出荷先郵便番号))
            {
                return;
            }

            try
            {
                // 住所情報取得
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_UcZip,
                        new object[] {
                           出荷先郵便番号
                        }));
            }
            catch { }
        }

        /// <summary>
        /// 住所情報取得
        /// </summary>
        /// <param name="tbl"></param>
        private void ShowAddresList(DataTable tbl)
        {
            foreach (DataRow dr in tbl.Rows)
            {
                // 取得データの１件目を対象として設定
                出荷先住所１ = dr["住所漢字"].ToString();
                //NotifyPropertyChanged("RowM01TOK");
                break;
            }

            // REMARKS:見つからなかった場合は何もしない

        }

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