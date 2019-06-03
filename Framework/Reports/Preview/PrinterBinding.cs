using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KyoeiSystem.Framework.Reports.Preview
{
	class PrinterBinding : INotifyPropertyChanged
	{
		private List<string> _printerNames;
		public List<string> PrinterNames
		{
			get
			{
				return this._printerNames;
			}
			set
			{
				this._printerNames = value;
				this.NotifyPropertyChanged();
			}
		}
		private List<string> _paperSizes;
		public List<string> PaperSizes
		{
			get
			{
				return this._paperSizes;
			}
			set
			{
				this._paperSizes = value;
				this.NotifyPropertyChanged();
			}
		}
		private List<string> _paperOrientations;
		public List<string> PaperOrientations
		{
			get
			{
				return this._paperOrientations;
			}
			set
			{
				this._paperOrientations = value;
				this.NotifyPropertyChanged();
			}
		}
		private List<string> _paperTrays;
		public List<string> PaperTrays
		{
			get
			{
				return this._paperTrays;
			}
			set
			{
				this._paperTrays = value;
				this.NotifyPropertyChanged();
			}
		}

		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

	}
}
