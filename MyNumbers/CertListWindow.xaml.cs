using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyNumber
{
	#region バインド用データクラス
	/// <summary>
	/// クライアント証明書選択画面用バインドデータクラス
	/// </summary>
	class CertWindowData : INotifyPropertyChanged
	{
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

		private ObservableCollection<CertInfo> _CertInfoList = new ObservableCollection<CertInfo>();
		public ObservableCollection<CertInfo> CertInfoList
		{
			get { return _CertInfoList; }
			set { _CertInfoList = value; NotifyPropertyChanged(); }
		}

		private CertInfo _selectedCertInfo = null;
		public CertInfo SelectedCertInfo
		{
			get { return _selectedCertInfo; }
			set { _selectedCertInfo = value; NotifyPropertyChanged(); }
		}

	}

	#region CertInfo メンバー
	public class CertInfo : INotifyPropertyChanged
	{
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

		private string _issueName = null;
		public string IssuerName
		{
			get { return _issueName; }
			set { _issueName = value; NotifyPropertyChanged(); }
		}

		private string _subjectName = null;
		public string SubjectName
		{
			get { return _subjectName; }
			set { _subjectName = value; NotifyPropertyChanged(); }
		}

	}
	#endregion

	#endregion

	/// <summary>
	/// クライアント証明書選択画面
	/// </summary>
	public partial class CertListWindow : Window
	{
		CertWindowData cntxt = new CertWindowData();

		public string SelectedCertName { get; set; }

		public CertListWindow()
		{
			InitializeComponent();
			this.DataContext = cntxt;
		}

		private void window_loaded(object sender, RoutedEventArgs e)
		{
			var certlist = KyoeiSystem.Framework.Net.Cert.GetCertCollection();
			cntxt.CertInfoList.Clear();
			foreach (var cert in certlist)
			{
				cntxt.CertInfoList.Add(new CertInfo() { SubjectName = cert.SubjectName.Name, IssuerName = cert.IssuerName.Name, });
			}
		}

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cntxt.SelectedCertInfo == null)
			{
				SelectedCertName = null;
			}
			else
			{
				SelectedCertName = cntxt.SelectedCertInfo.SubjectName;
			}
		}

		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			if (cntxt.SelectedCertInfo == null)
			{
				e.Handled = true;
				MessageBox.Show("選択されていません。");
				return;
			}
			this.DialogResult = true;
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		
	}
}
