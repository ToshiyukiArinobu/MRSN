using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views
{
	public static class DriveTime
	{
		public static decimal ConvertIntToDec(int time)
		{
			decimal hh = time / 60;
			decimal mm = time % 60;

			return hh + (mm / 100);
		}

		public static int ConvertDecToInt(decimal time)
		{
			int hour = (int)Math.Truncate(time);
			int minute = (int)Math.Truncate((Math.Truncate(time * 100) - (decimal)(hour * 100)));

			return hour * 60 + minute;
		}
	}
}
