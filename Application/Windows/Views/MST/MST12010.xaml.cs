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
    /// 自社名マスタ入力
    /// </summary>
    public partial class MST12010 : WindowMasterMainteBase
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
        public class ConfigMST12010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST12010 frmcfg = null;

        #endregion

        #region 定数定義
        private const string TargetTableNm = "M70_JIS";
        private const string UpdateTable = "M70_JIS_UP";
        private const string DeleteTable = "M70_JIS_DEL";
        private const string GetNextID = "M70_JIS_NEXT";
        #endregion

        #region バインド用プロパティ
        private string _自社ID = string.Empty;
        public string 自社ID
        {
            get { return this._自社ID; }
            set { this._自社ID = value; NotifyPropertyChanged(); }
        }

        private string _自社名 = string.Empty;
        public string 自社名
        {
            get { return this._自社名; }
            set { this._自社名 = value; NotifyPropertyChanged(); }
        }
        private string _代表者名 = string.Empty;
        public string 代表者名
        {
            get { return this._代表者名; }
            set { this._代表者名 = value; NotifyPropertyChanged(); }
        }
        private string _郵便番号 = string.Empty;
        public string 郵便番号
        {
            get { return this._郵便番号; }
            set { this._郵便番号 = value; NotifyPropertyChanged(); }
        }
        private string _住所１ = string.Empty;
        public string 住所１
        {
            get { return this._住所１; }
            set { this._住所１ = value; NotifyPropertyChanged(); }
        }
        private string _住所２ = string.Empty;
        public string 住所２
        {
            get { return this._住所２; }
            set { this._住所２ = value; NotifyPropertyChanged(); }
        }
        private string _電話番号 = string.Empty;
        public string 電話番号
        {
            get { return this._電話番号; }
            set { this._電話番号 = value; NotifyPropertyChanged(); }
        }
        private string _ＦＡＸ番号 = string.Empty;
        public string ＦＡＸ番号
        {
            get { return this._ＦＡＸ番号; }
            set { this._ＦＡＸ番号 = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行１ = string.Empty;
        public string 振込銀行１
        {
            get { return this._振込銀行１; }
            set { this._振込銀行１ = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行２ = string.Empty;
        public string 振込銀行２
        {
            get { return this._振込銀行２; }
            set { this._振込銀行２ = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行３ = string.Empty;
        public string 振込銀行３
        {
            get { return this._振込銀行３; }
            set { this._振込銀行３ = value; NotifyPropertyChanged(); }
        }
        private string _法人ナンバー = string.Empty;
        public string 法人ナンバー
        {
            get { return this._法人ナンバー; }
            set { this._法人ナンバー = value; NotifyPropertyChanged(); }
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

        //マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST12010

        /// <summary>
        /// 自社名マスタ入力
        /// </summary>
        public MST12010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load イベント

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
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
            frmcfg = (ConfigMST12010)ucfg.GetConfigValue(typeof(ConfigMST12010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST12010();
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

            //画面初期化
            ScreenClear();
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCH12010) });

        }

        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            MstData = null;
            自社ID = string.Empty;
            自社名 = string.Empty;
            代表者名 = string.Empty;
            郵便番号 = string.Empty;
            住所１ = string.Empty;
            住所２ = string.Empty;
            電話番号 = string.Empty;
            ＦＡＸ番号 = string.Empty;
            振込銀行１ = string.Empty;
            振込銀行２ = string.Empty;
            振込銀行３ = string.Empty;
            法人ナンバー = string.Empty;
            DriverPicData = null;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            ResetAllValidation();

            SetFocusToTopControl();
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

                            if (string.IsNullOrEmpty(自社名))
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

                        //更新時処理
                        case UpdateTable:
                            break;

                        //削除時処理
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
                                自社ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;
                        case UpdateTable:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("自社ID: " + 自社ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i自社ID = AppCommon.IntParse(自社ID);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i自社ID
                                                                                                                            ,自社名
                                                                                                                            ,代表者名
                                                                                                                            ,郵便番号
                                                                                                                            ,住所１
                                                                                                                            ,住所２
                                                                                                                            ,電話番号
                                                                                                                            ,ＦＡＸ番号
                                                                                                                            ,振込銀行１
                                                                                                                            ,振込銀行２
                                                                                                                            ,振込銀行３
                                                                                                                            ,法人ナンバー
                                                                                                                            ,DriverPicData
                                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                            ,true
                                                                                                                            }));

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
            自社ID = tbl.Rows[0]["自社ID"].ToString();
            自社名 = tbl.Rows[0]["自社名"].ToString();
            代表者名 = tbl.Rows[0]["代表者名"].ToString();
            郵便番号 = tbl.Rows[0]["郵便番号"].ToString();
            住所１ = tbl.Rows[0]["住所1"].ToString();
            住所２ = tbl.Rows[0]["住所2"].ToString();
            電話番号 = tbl.Rows[0]["電話番号"].ToString();
            ＦＡＸ番号 = tbl.Rows[0]["FAX"].ToString();
            振込銀行１ = tbl.Rows[0]["振込銀行1"].ToString();
            振込銀行２ = tbl.Rows[0]["振込銀行2"].ToString();
            振込銀行３ = tbl.Rows[0]["振込銀行3"].ToString();
            法人ナンバー = tbl.Rows[0]["法人ナンバー"].ToString();
            if (tbl.Rows[0]["画像"] == DBNull.Value)
            {
                DriverPicData = null;
            }
            else
            {
                DriverPicData = (byte[])tbl.Rows[0]["画像"];

            }

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
                if (string.IsNullOrEmpty(自社ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                //前データ検索
                int iSyasyuId = 0;
                if (int.TryParse(自社ID, out iSyasyuId))
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
                if (string.IsNullOrEmpty(自社ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 2 }));
                    return;
                }

                //次データ検索
                int iSyasyuId = 0;
                if (int.TryParse(自社ID, out iSyasyuId))
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
        /// F8 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            //MST12020 mst12020 = new MST12020();
            //mst12020.ShowDialog(this);
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
                if (string.IsNullOrEmpty(this.自社ID))
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


                if (result == MessageBoxResult.Yes)
                {

                    int i自社ID = 0;
                    if (!int.TryParse(自社ID, out i自社ID))
                    {
                        this.ErrorMessage = "自社IDの入力形式が不正です。";
                        return;
                    }
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i自社ID }));

                }
                
            }
            catch
            {
                this.ErrorMessage = "削除処理が出来ませんでした。";
            }
        }

        #endregion

        #region 処理メソッド

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
        /// Update処理
        /// </summary>
        private void Update()
        {
            try
            {
                    int i自社ID = 0;
                    if (string.IsNullOrEmpty(自社ID))
                    {
                        this.ErrorMessage = "自社IDは入力必須項目です。";
                        MessageBox.Show("自社IDは入力必須項目です。");
                        return;
                    }

                    if (!int.TryParse(自社ID, out i自社ID))
                    {
                        this.ErrorMessage = "自社IDの入力形式が不正です。";
                        MessageBox.Show("自社IDの入力形式が不正です。");
                        return;
                    }

                    if (string.IsNullOrEmpty(自社名))
                    {
                        this.ErrorMessage = "自社名は入力必須項目です。";
                        MessageBox.Show("自社名は入力必須項目です。");
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
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i自社ID
                                                                                                                ,自社名
                                                                                                                ,代表者名
                                                                                                                ,郵便番号
                                                                                                                ,住所１
                                                                                                                ,住所２
                                                                                                                ,電話番号
                                                                                                                ,ＦＡＸ番号
                                                                                                                ,振込銀行１
                                                                                                                ,振込銀行２
                                                                                                                ,振込銀行３
                                                                                                                ,法人ナンバー
                                                                                                                ,DriverPicData
                                                                                                                ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                ,false
                                                                                                                }));
                    }
                    else
                    {
                        return;
                    }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました。";
            }

        }
        #endregion

        #region 画像処理

        //クリアボタンクリックで画像をOFF
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DriverPicData = null;
        }

        //画像ダブルクリック時(ダイアログ表示)
        private void ImageButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "";
            ofd.DefaultExt = "*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == true)
            {
                ChangeImageData(ofd.FileName);
            }
        }

        //画像変更
        private void ChangeImageData(string filename)
        {
            using (FileStream rdr = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    // ■ サイズの上限は、DBから取得したConfigによる

                    long max = 2 * 1024 * 1024;
                    long size = rdr.Length;
                    if (size > (max))
                    {
                        // 2MBまでとする
                        MessageBox.Show(string.Format("画像ファイルサイズが {0}MB を超えています。\r\n[{1}]", (max / 1024 / 1024), size));
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

       

        #endregion

        #region イベント

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
                        if (string.IsNullOrEmpty(自社ID))
                        {
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));
                            return;
                        }

                        if (string.IsNullOrEmpty(自社ID))
                        {
                            this.ErrorMessage = "自社IDは必須入力項目です。";
                            MessageBox.Show("自社IDは必須入力項目です。");
                            return;
                        }
                        int i自社ID = 0;

                        if (!int.TryParse(自社ID, out i自社ID))
                        {
                            this.ErrorMessage = "自社IDの入力形式が不正です。";
                            MessageBox.Show("自社IDの入力形式が不正です。");
                            return;
                        }


                        //最後尾検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i自社ID, 0 }));
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

        #endregion

        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

        //ドラッグで取得したものがファイルなら受け取り　ファイルではない場合破棄
        private void toggleEffectsToDragEvent(DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        //ファイルのパスを取得
        private string getFileNameToDragEvent(DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (System.IO.File.Exists(fileName[0]) == true)
            { 
                return fileName[0]; 
            }
            else 
            { 
                return null; 
            }
        }

        private void Drag_Enter(object sender, DragEventArgs e)
        {
            this.toggleEffectsToDragEvent(e);
        }

        //画像を表示
        private void DragDrop(object sender, DragEventArgs e)
        {
            string fileName = this.getFileNameToDragEvent(e);
            ChangeImageData(fileName);

        }



    }
}
