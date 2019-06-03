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
using System.ComponentModel;
using System.Diagnostics;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Reports.Preview;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 得意先別品名単価マスタ
    /// </summary>
    public partial class MST21010 : WindowReportBase
    {
        //対象テーブル検索用
        private const string TargetTableNm = "M03_YTAN1_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M03_YTAN1_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M03_YTAN1_DEL";

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
        public class ConfigMST21010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST21010 frmcfg = null;

        #endregion

        #region データバインド用プロパティ
        private string _得意先ID = string.Empty;
        public string 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }
        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
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
        private string _計算区分 = string.Empty;
        public string 計算区分
        {
            get { return this._計算区分; }
            set { this._計算区分 = value; NotifyPropertyChanged(); }
        }
        private decimal? _支払単価 = null;
        public decimal? 支払単価
        {
            get { return this._支払単価; }
            set { this._支払単価 = value; NotifyPropertyChanged(); }
        }
        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }
        #endregion
        /// <summary>
        /// 得意先別品名単価マスタ
        /// </summary>
        public MST21010()
        {
            InitializeComponent();
            ScreenClear();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
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
            frmcfg = (ConfigMST21010)ucfg.GetConfigValue(typeof(ConfigMST21010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST21010();
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

            // 画面が表示される最後の段階で処理すべき内容があれば、ここに記述します。
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M08_TIK_UC", new List<Type> { typeof(MST03010), typeof(SCH03010) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST07010), typeof(SCH07010) });

            ScreenClear();

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
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        }
                        else
                        {
                            //削除データ　エラー処理
                            if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                            {
                                this.ErrorMessage = "既に削除されているデータです。";
                                MessageBox.Show("既に削除されたデータです。");
                                ScreenClear();
                                return;
                            }

                            支払単価 = AppCommon.DecimalParse(tbl.Rows[0]["支払単価"].ToString());
                            計算区分 = tbl.Rows[0]["計算区分"].ToString();
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        }
                        //主キー変更不可
                        ChangeKeyItemChangeable(false);
                        BtnKakunin.IsEnabled = true;
                        SetFocusToTopControl();
                        break;

                    //更新時処理
                    case TargetTableNmUpdate:
                        //コントロール初期化
                        ScreenClear();
                        break;

                    //削除時処理
                    case TargetTableNmDelete:
                        //コントロール初期化
                        ScreenClear();
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
        /// F6 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {

        }


        /// <summary>
        /// F8 リボン　リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST21020 mst21020 = new MST21020();
            mst21020.ShowDialog(this);
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
            if (string.IsNullOrEmpty(得意先ID + 発地ID + 着地ID + 商品ID))
            {
                return;
            }
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
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion


        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(得意先ID + 発地ID + 着地ID + 商品ID))
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

            this.Delete();
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
                int iHatutiId = 0;
                int iTyakutiId = 0;
                int iSyouhinId = 0;

                if (!(LabelTextShiharai.CheckValidation() && LabelTextHatuti.CheckValidation() && LabelTextTyakuti.CheckValidation() && LabelTextSyohin.CheckValidation()))
                {
                    return;
                }

				if (発地ID == "")
				{
					発地ID = "0";
				}
				if (着地ID == "")
				{
					着地ID = "0";
				}

                if (int.TryParse(得意先ID, out iTokuisakiId)
                    && int.TryParse(発地ID, out iHatutiId)
                    && int.TryParse(着地ID, out iTyakutiId)
                    && int.TryParse(商品ID, out iSyouhinId)
                    )
                {
                    //マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));
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
        private void ScreenClear()
        {

            得意先ID = string.Empty;
            発地ID = string.Empty;
            着地ID = string.Empty;
            商品ID = string.Empty;
            計算区分 = "0";
            支払単価 = null;
            ChangeKeyItemChangeable(true);
            BtnKakunin.IsEnabled = true;

            //ユーザーコントロールバリデータ
            this.ResetAllValidation();

            this.MaintenanceMode = string.Empty;

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
                int iTokuisakiId = 0;
                int iSyouhinId = 0;
                int iHatutiId = 0;
                int iTyakutiId = 0;

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (int.TryParse(得意先ID, out iTokuisakiId)
                    && int.TryParse(商品ID, out iSyouhinId)
                    && int.TryParse(発地ID, out iHatutiId)
                    && int.TryParse(着地ID, out iTyakutiId)
                    )
                {

                    var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.Yes)
                    {
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId, 支払単価, 計算区分 }));
                    }
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
            int iTokuisakiId = 0;
            int iSyouhinId = 0;
            int iHatutiId = 0;
            int iTyakutiId = 0;
            if (int.TryParse(得意先ID, out iTokuisakiId)
                && int.TryParse(商品ID, out iSyouhinId)
                && int.TryParse(発地ID, out iHatutiId)
                && int.TryParse(着地ID, out iTyakutiId)
                )
            {
                //メッセージボックス
                MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                                , "確認"
                                , MessageBoxButton.YesNo
                                , MessageBoxImage.Question
                                , MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));
                }
            }
        }
        #endregion

        /// <summary>
        /// 登録確認ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SCH19010 sch19010 = new SCH19010();
            sch19010.得意先ID = 得意先ID;
            sch19010.発地ID = 発地ID;
            sch19010.着地ID = 着地ID;
            sch19010.商品ID = 商品ID;
            sch19010.ShowDialog(this);

            if (sch19010.DialogResult == true)
            {
                得意先ID = sch19010.得意先ID;
                発地ID = sch19010.発地ID;
                着地ID = sch19010.着地ID;
                商品ID = sch19010.商品ID;

                try
                {
                    int iTokuisakiId = 0;
                    int iHatutiId = 0;
                    int iTyakutiId = 0;
                    int iSyouhinId = 0;
                    if (int.TryParse(得意先ID, out iTokuisakiId)
                        && int.TryParse(発地ID, out iHatutiId)
                        && int.TryParse(着地ID, out iTyakutiId)
                        && int.TryParse(商品ID, out iSyouhinId)
                        )
                    {
                        //マスタ
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void UcLabelTextRadioButton_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }

        //支払単価処理
        private void PreviewKEyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ShiharaiTanka.Text == string.Empty)
                {
                    支払単価 = 0;
                }
                else
                {
                    decimal Decit;
                    if (decimal.TryParse(ShiharaiTanka.Text, out Decit) == true)
                    {
                        支払単価 = Decit;
                    }
                }
            }
        }
    }
}
