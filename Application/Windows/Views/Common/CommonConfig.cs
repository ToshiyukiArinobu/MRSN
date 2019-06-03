using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 画面用設定項目基底クラス
	/// </summary>
	/// <remarks>
	/// 各画面共通の設定項目を定義する。
	/// </remarks>
	public class FormConfigBase
	{
		public double Top;
		public double Left;

        public double Height;
        public double Width;
        public string Memo;
        public string PrinterName;
		public KyoeiSystem.Framework.Reports.Preview.FwPrinterInfo PrinterInfo = null;
	}

	public class CommonConfig
	{
		public int ユーザID = -1;
		public string ユーザ名 = string.Empty;
        public int 自社コード = -1;
        public int 自社販社区分 = -1;
		public int 権限 = 0;
		public DateTime ログイン時刻;
		public DateTime ログアウト時刻;
        public string ライセンスID;
        //権限関係追加
        public int?[] タブグループ番号;
        public string[] プログラムID;
        public Boolean[] 使用可能FLG;
        public Boolean[] データ更新FLG;
    }

	public class SpreadColumnInfo
	{
		public int InitIndex;
		public int Index;
		public double Width;
	}

}
