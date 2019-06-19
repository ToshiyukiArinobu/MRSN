using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace KyoeiSystem.Framework.Reports.Preview
{
	/// <summary>
	/// MovingRect.xaml の相互作用ロジック
	/// </summary>
	public partial class MovingRect : UserControl
	{
		public bool IsMoving = false;
		private ReportPreview parentWindow;

		public MovingRect()
		{
			InitializeComponent();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.IsMoving = true;
		}

		private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.IsMoving = false;
		}

		private void Window_MouseMove(object sender, MouseEventArgs e)
		{
			parentWindow.OnMouseMove(sender, e);
		}
	}
}
