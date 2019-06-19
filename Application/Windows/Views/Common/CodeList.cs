using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views
{
	public class CodeData : System.ComponentModel.INotifyPropertyChanged
	{
		public string namearea;
		public string function;
		public string category;
		private int code;
		private int order;
		private string name;
		public int コード
		{
			get { return code; }
			set { code = value; NotifyPropertyChanged("コード"); }
		}
		public int 表示順
		{
			get { return order; }
			set { order = value; NotifyPropertyChanged("表示順"); }
		}
		public string 表示名
		{
			get { return name; }
			set { name = value; NotifyPropertyChanged("表示名"); }
		}

		#region INotifyPropertyChanged メンバー
		/// <summary>
		/// Binding機能対応（プロパティの変更通知イベント）
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Binding機能対応（プロパティの変更通知イベント送信）
		/// </summary>
		/// <param name="propertyName">Bindingプロパティ名</param>
		public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}

}
