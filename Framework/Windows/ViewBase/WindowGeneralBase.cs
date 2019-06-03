using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KyoeiSystem.Framework.Core;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// 汎用画面基底クラス
	/// </summary>
	public class WindowGeneralBase : WindowViewBase
	{
		/// <summary>
		/// ロード時のイベント処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void WindowViewBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			base.WindowViewBase_Loaded(sender, e);
		}
	}
}
