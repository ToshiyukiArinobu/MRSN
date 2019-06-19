using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;

namespace KyoeiSystem.Framework.Windows.ViewBase
{

	public delegate void FinishWindowClosedHandler(object sender);
	public delegate void FinishWindowRenderedHandler(object sender);

	/// <summary>
	/// 画面基底クラス
	/// </summary>
	public interface IWindowViewBase : INotifyPropertyChanged
	{
		event FinishWindowClosedHandler OnFinishWindowClosed;
		event FinishWindowRenderedHandler OnFinishWindowRendered;

		/// <summary>
		/// 画面クローズ処理開始状態
		/// </summary>
		bool IsClosing { get; set; }

		bool IsLoadFinished { get; set; }

		/// <summary>
		/// DB接続用
		/// </summary>
		string ConnString { get; set; }

		/// <summary>
		/// 画面共有オブジェクト
		/// </summary>
		ViewsCommon viewsCommData { get; set; }
		///// <summary>
		///// 画面を閉じる時のイベント
		///// </summary>
		///// <param name="sender">イベント送信オブジェクト</param>
		///// <param name="e">イベントパラメータ</param>
		//void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e);

		/// <summary>
		/// タイマーループの開始
		/// </summary>
		/// <param name="time"></param>
		void TimerLoopStart(int time);

		/// <summary>
		/// データ処理要求を送信する
		/// </summary>
		/// <param name="comobj"></param>
		void SendRequest(CommunicationObject comobj);

		/// <summary>
		/// データ受信イベント
		/// </summary>
		/// <param name="message">受信データ</param>
		void OnReceived(CommunicationObject message);

		/// <summary>
		/// タイマーイベント受信時の処理
		/// </summary>
		/// <param name="message">受信データ</param>
		void OnReceivedTimer(CommunicationObject message);

		/// <summary>
		/// エラー受信時の処理
		/// </summary>
		/// <param name="message">受信データ</param>
		void OnReveivedError(CommunicationObject message);

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message">受信データ</param>
		void OnReceivedResponseData(CommunicationObject message);

		/// <summary>
		/// ストアドの実行結果受信処理
		/// </summary>
		/// <param name="message">受信データ</param>
		void OnReceivedResponseStored(CommunicationObject message);

		/// <summary>
		/// フォーカスを先頭のコントロールに移動する
		/// </summary>
		/// <returns>true:移動した、false:移動していない</returns>
		bool SetFocusToTopControl();

		/// <summary>
		/// キー項目としてマークされた項目の入力可否を切り替える
		/// </summary>
		/// <param name="flag">true:入力可、false:入力不可</param>
		void ChangeKeyItemChangeable(bool flag);

		/// <summary>
		/// Window内のValidatorプロパティを持つ全項目をチェックする
		/// </summary>
		/// <returns>true:全項目OK、false:チェックNG項目あり</returns>
		bool CheckAllValidation(bool setfocustop);

		/// <summary>
		/// Windowを入力不可にする（マウスカーソルを砂時計にする）
		/// </summary>
		void SetBusyForInput();

		/// <summary>
		/// Windowを入力可能にする（マウスカーソルを戻す）
		/// </summary>
		void SetFreeForInput();

		/// <summary>
		/// 指定されたコントロールの内側に存在するDataGridを探し、そのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		DataGridRow GetDataGridRow(object curctl);

		/// <summary>
		/// 現在フォーカスのあるDataGridのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		DataRowView GetCurrentDataGridRow(object curctl);

		/// <summary>
		/// 現在フォーカスのあるDataGridを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns></returns>
		DataGrid GetCurrentDataGrid(object curctl);

		/// <summary>
		/// DataGridCellのマウスクリックイベントのハンドラを定義する
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e);
	}


}
