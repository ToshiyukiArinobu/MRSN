using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using System.Data.SqlClient;
using System.Data.OleDb;

namespace KyoeiSystem.Framework.Windows.Controls
{

	/// <summary>
	/// UcDataGrid.xaml の相互作用ロジック
	/// </summary>
	public partial class UcDataGrid : FrameworkControl
	{
		/// <summary>
		/// 初期化
		/// </summary>
		public UcDataGrid()
		{
			InitializeComponent();
		}

		#region DataGrid Property


		#region AlternatingRowBackground
		/// <summary>
		/// 交互の行の背景ブラシを取得または設定します。 
		/// </summary>
		[Category("ブラシ")]
		public Brush cAlternatingRowBackground
		{
			get { return cDataGrid.AlternatingRowBackground; }
			set { cDataGrid.AlternatingRowBackground = value; }

		}
		#endregion

		#region AreRowDetailsFrozen
		/// <summary>
		/// 行の詳細は水平方向にスクロールできるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public bool cAreRowDetailsFrozen
		{
			get { return cDataGrid.AreRowDetailsFrozen; }
			set { cDataGrid.AreRowDetailsFrozen = value; }

		}
		#endregion


		#region AutoGenerateColumns
		/// <summary>
		/// 列が自動的に作成するかどうかを示す値を取得または設定します。 
		/// </summary>
		[Category("動作")]
		public bool cAutoGenerateColumns
		{
			get { return cDataGrid.AutoGenerateColumns; }
			set { cDataGrid.AutoGenerateColumns = value; }

		}
		#endregion


		#region Background
		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。 
		/// </summary>
		[Category("ブラシ")]
		public Brush cBackground
		{
			get { return cDataGrid.Background; }
			set { cDataGrid.Background = value; }

		}
		#endregion


		#region CanUserAddRows
		/// <summary>
		/// ユーザーは DataGridに新しい行を追加できるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cCanUserAddRows
		{
			get { return cDataGrid.CanUserAddRows; }
			set { cDataGrid.CanUserAddRows = value; }

		}
		#endregion


		#region CanUserDeleteRows
		/// <summary>
		/// ユーザーは DataGridから行を削除できるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cCanUserDeleteRows
		{
			get { return cDataGrid.CanUserDeleteRows; }
			set { cDataGrid.CanUserDeleteRows = value; }

		}
		#endregion


		#region CanUserReorderColumns
		/// <summary>
		/// ユーザーが列見出しをマウスでドラッグして列の表示順を変更できるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cCanUserReorderColumns
		{
			get { return cDataGrid.CanUserReorderColumns; }
			set { cDataGrid.CanUserReorderColumns = value; }

		}
		#endregion


		#region CanUserResizeColumns
		/// <summary>
		/// ユーザーがマウスを使用して列の幅を調整できるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cCanUserResizeColumns
		{
			get { return cDataGrid.CanUserResizeColumns; }
			set { cDataGrid.CanUserResizeColumns = value; }

		}
		#endregion


		#region CanUserSortColumns
		/// <summary>
		/// ユーザーが列見出しをクリックして、項目を並べ替えられるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cCanUserSortColumns
		{
			get { return cDataGrid.CanUserSortColumns; }
			set { cDataGrid.CanUserSortColumns = value; }

		}
		#endregion


		#region Columns
		/// <summary>
		/// DataGridのすべての列を含むコレクションを取得します。
		/// </summary>
		[Category("動作")]
		public System.Collections.ObjectModel.ObservableCollection<DataGridColumn> cColumns
		{
			get { return cDataGrid.Columns; }
			set { }

		}
		#endregion


		#region CommandBindings
		/// <summary>
		/// DataGridのすべての列を含むコレクションを取得します。
		/// </summary>
		[Category("動作")]
		public CommandBindingCollection cCommandBindings
		{
			get { return cDataGrid.CommandBindings; }
			set { }

		}
		#endregion


		#region DataContext
		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します。
		/// </summary>
		[Category("動作")]
		public object cDataContext
		{
			get { return cDataGrid.DataContext; }
			set { cDataGrid.DataContext = value; }

		}
		#endregion


		#region FrozenColumnCount
		/// <summary>
		/// 非スクロール列の数を取得または設定します。
		/// </summary>
		[Category("動作")]
		public int cFrozenColumnCount
		{
			get { return cDataGrid.FrozenColumnCount; }
			set { cDataGrid.FrozenColumnCount = value; }

		}
		#endregion


		#region EnableRowVirtualization
		/// <summary>
		/// 行の仮想化が有効かどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cEnableRowVirtualization
		{
			get { return cDataGrid.EnableRowVirtualization; }
			set { cDataGrid.EnableRowVirtualization = value; }

		}
		#endregion

	
		#region HorizontalScrollBarVisibility
		/// <summary>
		/// 水平スクロール バーが DataGridでどのように表示されるかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public ScrollBarVisibility cHorizontalScrollBarVisibility
		{
			get { return cDataGrid.HorizontalScrollBarVisibility; }
			set { cDataGrid.HorizontalScrollBarVisibility = value; }

		}
		#endregion

	
		#region IsReadOnly
		/// <summary>
		/// ユーザーは DataGridの値を編集できるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public bool cIsReadOnly
		{
			get { return cDataGrid.IsReadOnly; }
			set { cDataGrid.IsReadOnly = value; }

		}
		#endregion


		#region ItemsSource
		/// <summary>
		/// DataGridのすべての列を含むコレクションを取得します。
		/// </summary>
		[Category("動作")]
		public System.Collections.IEnumerable cItemsSource
		{
			get { return cDataGrid.ItemsSource; }
			set { cDataGrid.ItemsSource = value; }
		}
		#endregion

		public static readonly DependencyProperty ItemSourcesProperty =
			DependencyProperty.Register("ItemSources", typeof(System.Collections.IEnumerable), typeof(UcDataGrid),new FrameworkPropertyMetadata(null,new PropertyChangedCallback(OnItemSourcesChanged)));

		private static void OnItemSourcesChanged(DependencyObject obj,DependencyPropertyChangedEventArgs e){
		UcDataGrid userControl = obj as UcDataGrid;
			if(userControl != null){
				System.Collections.IEnumerable value = (System.Collections.IEnumerable)e.NewValue;
				userControl.cItemsSource = value;
				userControl.cDataGrid.ItemsSource = value;
			}
		}


		public System.Collections.IEnumerable ItemSources
		{
			get { return (System.Collections.IEnumerable)GetValue(ItemSourcesProperty); }
			set { SetValue(ItemSourcesProperty, value);
			cDataGrid.ItemsSource = ItemSources;
			}
		}





		#region RowDetailsVisibilityMode
		/// <summary>
		/// 行の詳細セクションがいつ表示されるかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public DataGridRowDetailsVisibilityMode cRowDetailsVisibilityMode
		{
			get { return cDataGrid.RowDetailsVisibilityMode; }
			set { cDataGrid.RowDetailsVisibilityMode = value; }

		}
		#endregion


		#region RowBackground
		/// <summary>
		/// 行の背景の既定のブラシを取得または設定します。
		/// </summary>
		[Category("ブラシ")]
		public Brush cRowBackground
		{
			get { return cDataGrid.RowBackground; }
			set { cDataGrid.RowBackground = value; }

		}
		#endregion




		#region VerticalScrollBarVisibility
		/// <summary>
		/// 垂直スクロール バーが DataGridでどのように表示されるかを示す値を取得または設定します。
		/// </summary>
		[Category("動作")]
		public ScrollBarVisibility cVerticalScrollBarVisibility
		{
			get { return cDataGrid.VerticalScrollBarVisibility; }
			set { cDataGrid.VerticalScrollBarVisibility = value; }

		}
		#endregion


		#region Visibility
		/// <summary>
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。これは 依存関係プロパティです。
		/// </summary>
		[Category("デザイン")]
		public Visibility cVisibility
		{
			get { return cDataGrid.Visibility; }
			set { cDataGrid.Visibility = value; }

		}
		#endregion


		#region Width
		/// <summary>
		/// 要素の幅を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cWidth
		{
			get { return cDataGrid.Width; }
			set { cDataGrid.Width = value; }

		}
		#endregion

		private void Grid_KeyDown_1(object sender, KeyEventArgs e)
		{
			//keydownでイベントが発生しない？Enterが拾えない
		}


		#endregion

		/// <summary>
		/// key操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cDataGrid_PreviewKeyDown_1(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// セルの変更時イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cDataGrid_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
		{
		}

		private void cDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
		}

		private void cDataGrid_CurrentCellChanged(object sender, EventArgs e)
		{
		}
	}
}
