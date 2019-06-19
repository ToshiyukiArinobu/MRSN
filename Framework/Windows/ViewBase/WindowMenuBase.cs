using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;

using KyoeiSystem.Framework.Core;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// メニュー画面用モデルクラス
	/// </summary>
	public class WindowMenuBase : WindowViewBase
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WindowMenuBase()
			: base()
		{
            // 20150810 wada modify Loadedイベントを削除、ContentRendered追加
            // 20150806 wada add Loaded, Closedイベントを追加する。
            //this.Loaded += WindowMenuBase_Loaded;
			//this.Closed += WindowMenuBase_Closed;
			//this.ContentRendered += WindowMenuBase_ContentRendered;
        }

		//// 20150806 wada add タイマー用変数
		//private System.Timers.Timer timer;

        ///// <summary>
        ///// 20150806 wada add メニュー画面Load時処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void WindowMenuBase_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // ロード後、アクセス時刻を更新する。
        //    // ログイン時の時間からこの画面のロード後まで、端末によっては時間がかかり、
        //    // 所定の時間間隔をかなり過ぎる場合があるため。
        //    updateAccessDateTime();

        //    // タイマーイベントを追加する。1秒=1000, 4分=240000
        //    timer = new System.Timers.Timer(240000);
        //    timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        //    timer.Start();
        //}

		///// <summary>
		///// 20150807 wada add メニュー画面Close時処理
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void WindowMenuBase_Closed(object sender, EventArgs e)
		//{
		//	// タイマーを破棄する。
		//	timer.Dispose();
		//}

		///// <summary>
		///// 20150806 wada add タイマーイベント
		///// ログイン中のユーザーのアクセス時刻を更新する。
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		//{
		//	updateAccessDateTime();
		//}

		///// <summary>
		///// 20150807 wada add ログイン中のユーザーのアクセス時刻を更新する。
		///// </summary>
		//private void updateAccessDateTime()
		//{
		//	SendRequest(new CommunicationObject(MessageType.UpdateData, "updateAccessDateTime",
		//		new object[] { KyoeiSystem.Application.WCFService.CommonData.CommonDB_UserId }));
		//}

		///// <summary>
		///// 20150810 wada add メニュー画面描画後に実行する処理
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//void WindowMenuBase_ContentRendered(object sender, EventArgs e)
		//{
		//	// Loadedイベントでは、描画がされず固まる事象があったため、
		//	// ContentRenderedイベントに変更
		//	// タイマーイベントを追加する。1秒=1000, 4分=240000
		//	timer = new System.Timers.Timer(240000);
		//	timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
		//	timer.Start();
		//}

    }
}
