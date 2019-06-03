using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// データアクセスライブラリの定義情報
	/// </summary>
	[Serializable()]
	public class WCFDataAccessConfig
	{
		/// <summary>
		/// 定義名：定義ファイル全体でユニークであることが必要
		/// </summary>
		public string Name = string.Empty;
		/// <summary>
		/// 説明：省略可能
		/// </summary>
		public string Descprition = string.Empty;
		/// <summary>
		/// 実行するクラスの所属する名前空間
		/// </summary>
		public string Namespace = string.Empty;
		/// <summary>
		/// 実行するメソッドの所属するクラス名
		/// </summary>
		public string ServiceClass = string.Empty;
		/// <summary>
		/// 実行するメソッド名
		/// </summary>
		public string MethodName = string.Empty;
		/// <summary>
		/// DLLファイル名：フルパス・相対パス指定またはDLL参照可能なフォルダに配置されていること
		/// </summary>
		public string Dll = string.Empty;
	}

	/// <summary>
	/// データアクセスライブラリの定義情報を操作するためのクラス
	/// </summary>
	[System.Xml.Serialization.XmlRoot("FrameworkConfig")]
	public class DataAccessConfig
	{
		/// <summary>
		/// 定義情報のコレクション
		/// </summary>
		public List<WCFDataAccessConfig> WCFDataAccessConfigs = new List<WCFDataAccessConfig>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DataAccessConfig()
		{
		}

		/// <summary>
		/// 指定された定義名に該当する定義情報を取得する
		/// </summary>
		/// <param name="name">定義名</param>
		/// <returns>指定された定義名に合致する定義情報</returns>
		public WCFDataAccessConfig GetConfig(string name)
		{
			WCFDataAccessConfig cfg = this.WCFDataAccessConfigs.Where(c => c.Name == name).FirstOrDefault();

			return cfg;
		}

	}

}
