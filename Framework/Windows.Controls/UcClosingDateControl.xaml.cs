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

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcClosingDateControl.xaml の相互作用ロジック
	/// </summary>
	public partial class UcClosingDateControl : FrameworkControl
	{

		#region CheckBox
		/// <summary>
		/// 
		/// </summary
		public UcClosingDateControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// チェックボックスの判定用
		/// </summary>
		public bool allCheckBox = false;
		/// <summary>
		/// 表示の有無を確認
		/// </summary>
		public bool ViewCheckBox
		{
			set
			{
				allCheckBox = value;
				CheckBoxJudgment(allCheckBox);
			}
			get { return allCheckBox; }
		}

		/// <summary>
		/// UIの表示切替
		/// </summary>
		/// <param name="check"></param>
		private void CheckBoxJudgment(bool check)
		{
			if (check == true)
			{
				ALLChackBox.Visibility = Visibility.Visible;

			}
			else
			{
				ALLChackBox.Visibility = Visibility.Collapsed;
			}
		}
		#endregion

		#region 締め日区間
		/// <summary>
		/// 当月締め日の期間の表示の有無判定
		/// </summary>
		private bool periodDay = false;
		
		/// <summary>
		/// 
		/// </summary>
		public bool ViewPeriodDay
		{
			set
			{
				periodDay = value;
				PeriodDayJudgment(periodDay);

			}
			get { return periodDay; }
		}

		/// <summary>
		/// UIの表示切替
		/// </summary>
		/// <param name="check">表示の有無</param>
		private void PeriodDayJudgment(bool check)
		{
			if (check == true)
			{
				this.PeriodDay.Visibility = Visibility.Visible;

			}
			else
			{
				this.PeriodDay.Visibility = Visibility.Collapsed;
			}
		}

		#endregion

		/// <summary>
		/// 全てのLabelのWidth用
		/// </summary>
		private double allLabelWidth;
		/// <summary>
		/// 全てのLabelの長さ
		/// </summary>
		public double AllLabelWidth 
		{
			set
			{
				this.allLabelWidth = value;
				this.ClosingDate.Label_Width = this.allLabelWidth;
				this.StartLabel.cWidth = this.allLabelWidth;
				this.EndLabel.cWidth = this.allLabelWidth;
			}
			get { return this.allLabelWidth; }
		}

		/// <summary>
		/// 対象区間を入力する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ViewPeriodDay_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (periodDay == true & (this.ClosingDate.Text as string) != "" & this.Year.cText != "" & this.Month.cText != "")
				{
					int Closing = int.Parse(this.ClosingDate.Text as string);
					int Year = int.Parse(this.Year.cText);
					int Month = int.Parse(this.Month.cText);
					
					DateTime EndDay = new DateTime(Year, Month, Closing);

					DateTime ClosingDay = new DateTime(Year, Month, Closing);
	
					DateTime d1 = DateTime.Today;
					//末日を求める
					DateTime d2 = new DateTime(d1.Year, d1.Month, DateTime.DaysInMonth(d1.Year, d1.Month));

					TimeSpan MonthTime = new TimeSpan(30,0,0,0);
					TimeSpan OneDay = new TimeSpan(1, 0, 0, 0);
					#region
		/*			
					if ( EndDay.Month != 4 | EndDay.Month != 6 | EndDay.Month != 9 | EndDay.Month != 11)
					{

						if (EndDay.Month == 2)
						{
							//閏年
							MonthTime = MonthTime - OneDay;
							if (EndDay.Year % 4 == 0)
							{
								//平年
								MonthTime = MonthTime - OneDay;
								if (EndDay.Year % 100 == 0)
								{
									//閏年
									MonthTime = MonthTime + OneDay;
									if (EndDay.Year % 400 == 0)
									{
										MonthTime = MonthTime - OneDay - OneDay;
									}
								}
							}
						}
						else
						{

							MonthTime = MonthTime + OneDay;
							
						}
					}
		*/			
					#endregion
					DateTime StartDay = EndDay - MonthTime;
					this.StartDay.cSelectedDate = StartDay;
					this.EndDay.cSelectedDate = EndDay;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// チェックボックスON/OFFイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ALLChackBox_Click_1(object sender, RoutedEventArgs e)
		{
			if (this.ALLChackBox.IsChecked == true)
			{
				this.Year.cIsReadOnly = true;
				this.Month.cIsReadOnly = true;
			}
			else
			{
				this.Year.cIsReadOnly = false;
				this.Month.cIsReadOnly = false;
			}
		}
	}
}
