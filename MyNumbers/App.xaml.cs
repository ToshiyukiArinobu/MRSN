using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MyNumber
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		[STAThread]
		void StartupMain(object sender, StartupEventArgs e)
		{
			int sts = 1;
			try
			{
				MyNumberWindow frm = new MyNumberWindow();
				bool? ret = frm.ShowDialog();
				if (ret == true && frm.ExitCode == 0)
				{
					sts = 0;
				}
				else if (frm.ExitCode == 9)
				{
					sts = 9;
				}
				else
				{
					sts = 1;
				}
			}
			catch (Exception)
			{
				sts = 1;
			}
			finally
			{
			}
			System.Environment.Exit(sts);
		}
		
	}
}
