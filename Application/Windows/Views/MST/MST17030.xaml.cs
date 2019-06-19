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

using GrapeCity.Windows.SpreadGrid;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 得意先別距離別運賃マスタ
    /// </summary>
    public partial class MST17030 : WindowReportBase
    {
        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M50_RTBL_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M50_RTBL_UPD_test";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M50_RTBL_DEL";

        #endregion

        #region データバインド用プロパティ

        private DataTable _タリフデータ;
        public DataTable タリフデータ
        {
            get { return this._タリフデータ; }
            set { this._タリフデータ = value; NotifyPropertyChanged(); }
        }

        private string _タリフID = string.Empty;
        public string タリフID
        {
            get { return this._タリフID; }
            set { this._タリフID = value; NotifyPropertyChanged(); }
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

        #region MST17030
        /// <summary>
        /// 得意先別距離別運賃マスタ
        /// </summary>
        public MST17030()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Load
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
           
　　    }
        #endregion

        #region エラー受信イベント
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
        #endregion

        #region 画面初期化
        /// <summary>
        /// コントロール初期化
        /// </summary>
        private void UcInitialize()
        {
            タリフID = null;
            タリフデータ = null;
            ChangeKeyItemChangeable(true);
            SetFocusToTopControl();
        }
        #endregion

        #region データ受信メソッド

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
                    //検索時処理
                    case TargetTableNm:
                        this.タリフデータ = (data as DataTable);
                        //SetTblData(tbl);
                        ChangeKeyItemChangeable(false);
                        SetFocusToTopControl();
                        break;

                    //更新時処理
                    case TargetTableNmUpdate:
                       

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

        #endregion

        #region 画面反映プログラム

        private void SetTblData(DataTable tbl)
        {
            //int rows = 0;
            //int cols = 0;
            ////セル0,0のタイトル
            //gcSpreadGrid[rows, cols].Value = "     距離＼重量";



            ////**重量処理**//　横に一行表示　[Row:0 , Col:データ分]
            //int precol = 0;
            //for (int col = 1; col <= gcSpreadGrid.ColumnCount - 1; col++)
            //{
            //    gcSpreadGrid[0, col].Value = tbl.Rows[precol]["重量"];
            //    precol += 1;
            //}



            ////**距離処理**//　縦に一列表示　[Row:データ分 , Col:0]
            //int prerow = 0;
            //for (int row = 1; row < gcSpreadGrid.RowCount; row++)
            //{
            //    if (row >= 1)
            //    {
            //        if (gcSpreadGrid[prerow, 0].Value != null)
            //        {
            //            if (gcSpreadGrid[prerow, 0].Value.ToString() == tbl.Rows[row]["距離"].ToString())
            //            {
            //                continue;
            //            }
            //        }
            //    }
            //    gcSpreadGrid[prerow + 1, 0].Value = tbl.Rows[row]["距離"];
            //    prerow += 1;
            //}



            ////**運賃処理**//　運賃を表示 [Row:データ分 , Col:データ分]
            ////prerowは距離処理で使用したRowの数
            //int count = 0;
            //for (int row = 1; row <= prerow; row++)
            //{
            //    for (int col = 1; col < gcSpreadGrid.ColumnCount; col++)
            //        if (gcSpreadGrid[row, col].Value != null)
            //        {
            //            if (gcSpreadGrid[row, col].Value.ToString() == tbl.Rows[count]["距離"].ToString())
            //            {
            //                count += 1;
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            gcSpreadGrid[row, col].Value = tbl.Rows[count]["運賃"];
            //            count += 1;
            //        }
            //}


            //// 固定境界の外観を設定
            //gcSpreadGrid.FrozenLine = new BorderLine(Colors.LightGray, BorderLineStyle.Thick);

        }

        #endregion


        //UPDATE処理
        public void Update()
        {
            //int iタリフID = 0;
            //if (!int.TryParse(タリフID, out iタリフID))
            //{
            //    this.ErrorMessage = "タリフIDの入力形式が不正です。";
            //    return;
            //}
            

            //int 重量;
            //int 距離;
            //int 運賃;
            //for (int row = 1; row < gcSpreadGrid.RowCount; row++)
            //{
            //    for (int col = 1; col < gcSpreadGrid.ColumnCount; col++)
            //    {
            //        if (gcSpreadGrid[row, col].Value != null)
            //        {
            //            運賃 = Convert.ToInt32(gcSpreadGrid[row, col].Value);
            //            重量 = Convert.ToInt32(gcSpreadGrid[0, col].Value);
            //            距離 = Convert.ToInt32(gcSpreadGrid[row, 0].Value);
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { iタリフID , 重量, 距離, 運賃 }));

            //    }
            //}



            //*******ごみファイル
            ////**重量取得**//
            //int precol = 0;
            //int[] weight; //重量
            //int wNum = 0;　//配列変数[wNum]
            //weight = new int[wNum];
            //for (int col = 1; col <= gcSpreadGrid.ColumnCount - 1; col++)
            //{

            //    weight[wNum] = Convert.ToInt32(gcSpreadGrid[0, col].Value);  
            //    precol += 1;
            //    wNum += 1;
            //}


            //    //**距離取得**//
            //    int prerow = 0;
            //    int[] miles;　//距離
            //    int mNum = 0;　//配列変数[mNum]
            //    miles = new int[mNum];
            //    for (int row = 1; row < gcSpreadGrid.RowCount; row++)
            //    {
            //        if (gcSpreadGrid[row, 0].Value != null)
            //        {
            //            miles[mNum] = Convert.ToInt32(gcSpreadGrid[row, 0].Value);
            //            prerow += 1;
            //            mNum += 1;
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }


            //    //**運賃取得**//
            //    int value[wNum][mNum];

            //    for (int row = 1; row < gcSpreadGrid.RowCount; row++)
            //    {
            //        for (int col = 1; col <= gcSpreadGrid.ColumnCount - 1; col++)
            //        {



            //        }
            //    }
            //}

            }

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
        /// F6 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {

        }


        /// <summary>
        /// F8 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST17020 mst17020 = new MST17020();
            mst17020.ShowDialog(this);
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
            try
            {
                //MstDataに値がなければメッセージ表示
                if (this.タリフデータ == null)
                {
                    MessageBox.Show("登録データではない為削除出来ませんでした。");
                    return;
                }

                //メッセージボックス
                MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);
                //キャンセルなら終了
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }

                int iタリフID = 0;
                if (!int.TryParse(タリフID, out iタリフID))
                {
                    this.ErrorMessage = "タリフIDの入力形式が不正です。";
                    return;
                }
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { iタリフID }));
            }
            catch
            {
                this.ErrorMessage = "削除処理が出来ませんでした。";
            }
        }

     
        #endregion

        #region 処理メソッド
        /// <summary>
        /// 主キーテキストボックスロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_LostFocus2(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int iタリフID = 0;
                if (!int.TryParse(タリフID, out iタリフID))
                {
                    this.ErrorMessage = "タリフIDの入力形式が不正です。";
                    return;
                }

                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iタリフID }));

            }
            catch (Exception)
            {
                return;
            }

        }

        #endregion


        private void gcSpreadGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {

            int col;
            int row;

            int 重量;
            int 距離;
            int 運賃;
            int iタリフID = 0;
            if (!int.TryParse(タリフID, out iタリフID))
            {
                this.ErrorMessage = "タリフIDの入力形式が不正です。";
                return;
            }

            col = gcSpreadGrid.ActiveColumnIndex;
            row = gcSpreadGrid.ActiveRowIndex;

            距離 = Convert.ToInt32(gcSpreadGrid[row, 0].Value);
            重量 = Convert.ToInt32(gcSpreadGrid[row, 1].Value);
            運賃 = Convert.ToInt32(gcSpreadGrid[row, 2].Value);
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { iタリフID, 距離, 重量, 運賃 }));

        }
   
    }
}


