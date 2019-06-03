using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// マスタ検索時の子画面用基底クラス
	/// </summary>
	public class WindowMasterSearchBase : WindowViewBase
	{
		private UcLabelTwinTextBox _twinTextBox = null;
		/// <summary>
		/// Fキー呼出時のインタフェースコントロール
		/// </summary>
		/// <remarks>
		/// 親画面のTwinTextboxと連携する場合、このプロパティに親画面のコントロールをセットしてから子画面を表示する。
		/// </remarks>
		public UcLabelTwinTextBox TwinTextBox
		{
			get { return this._twinTextBox; }
			set { this._twinTextBox = value; OnChangedTargetMaster(); }
		}

		/// <summary>
		/// 未使用：廃止されたメソッドです。
		/// </summary>
		public virtual void OnChangedTargetMaster()
		{
		}

		/// <summary>
		/// 画面初期化完了時に発生するイベント
		/// </summary>
		/// <param name="e">イベント引数</param>
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			var datagrid = ViewBaseCommon.FindLogicalChildList<DataGrid>(this, false);
			foreach (var item in datagrid)
			{
				item.SelectionMode = DataGridSelectionMode.Single;
			}
		}
	}
}
