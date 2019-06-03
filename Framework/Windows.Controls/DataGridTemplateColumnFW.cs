using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KyoeiSystem.Framework.Windows.Controls
{
	public class DataGridTemplateColumnFW : DataGridTemplateColumn
	{
		public DataGridTemplateColumnFW()
		{
		}

		protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
		{
			editingElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
			return base.PrepareCellForEdit(editingElement, editingEventArgs);
		}

	}
}
