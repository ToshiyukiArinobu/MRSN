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
using System.Windows.Forms;

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcReadFileButton.xaml の相互作用ロジック
	/// </summary>
	public partial class UcReadFileButton : FrameworkControl
	{
		/// <summary>
		/// ファイル参照ボタンを追加したコントロール
		/// </summary>
		public UcReadFileButton()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 参照ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cButton_Click_1(object sender, RoutedEventArgs e)
		{
			//OpenFileDialogクラスのインスタンスを作成
			OpenFileDialog ofd = new OpenFileDialog();

			//はじめのファイル名を指定する
			//はじめに「ファイル名」で表示される文字列を指定する
			ofd.FileName = "default.html";
			//はじめに表示されるフォルダを指定する
			//指定しない（空の文字列）の時は、現在のディレクトリが表示される
			ofd.InitialDirectory = @"C:\";
			//[ファイルの種類]に表示される選択肢を指定する
			//指定しないとすべてのファイルが表示される
			ofd.Filter =
				"HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
			//[ファイルの種類]ではじめに
			//「すべてのファイル」が選択されているようにする
			ofd.FilterIndex = 2;
			//タイトルを設定する
			ofd.Title = "開くファイルを選択してください";
			//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			ofd.RestoreDirectory = true;
			//存在しないファイルの名前が指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckFileExists = true;
			//存在しないパスが指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckPathExists = true;
			
			//ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				//OKボタンがクリックされたとき
				//選択されたファイル名を表示する
				Console.WriteLine(ofd.FileName);
				this.cTextBox.cText = ofd.FileName;
				

				//	this.cTextBox.cText = ofd.GetLifetimeService
			}


		}
	}
}
