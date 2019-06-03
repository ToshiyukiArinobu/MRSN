using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace KyoeiSystem.Framework.Common
{
	[System.Xml.Serialization.XmlRoot("FrameworkConfig")]
	public class AppConfig
	{
		public string ConnectionString = string.Empty;
		public List<WCFDataAccessConfig> WCFDataAccessConfigs = new List<WCFDataAccessConfig>();
		public List<WCFProcedureConfig> WCFProcedureConfigs = new List<WCFProcedureConfig>();

		public AppConfig()
		{
		}

		public WCFDataAccessConfig GetConfig(string name)
		{
			foreach (WCFDataAccessConfig cfg in this.WCFDataAccessConfigs)
			{
				if (cfg.Name == name)
				{
					return cfg;
				}
			}
			return null;
		}

		public WCFProcedureConfig GetProcedureConfig(string name)
		{
			foreach (WCFProcedureConfig cfg in this.WCFProcedureConfigs)
			{
				if (cfg.Name == name)
				{
					return cfg;
				}
			}
			return null;
		}


#if DEBUG
		public void Save()
		{
			System.Xml.Serialization.XmlSerializer srlzr
				= new System.Xml.Serialization.XmlSerializer(typeof(AppConfig));
			System.IO.FileStream fs1
				= new System.IO.FileStream(@"sample.xml", System.IO.FileMode.Create);
			srlzr.Serialize(fs1, this);
			fs1.Close();
		}
#endif

	}

	[Serializable()]
	public class WCFDataAccessConfig : WCFDataAccessConfigBase
	{
		public string DataMemberClass = string.Empty;
		public string GetDataMethod = string.Empty;
		public string PutDataMethod = string.Empty;
		public string DeleteMethod = string.Empty;
		public string InsertMethod = string.Empty;
	}

	[Serializable()]
	public class WCFProcedureConfig : WCFDataAccessConfigBase
	{
		public string ProcedureMethod = string.Empty;
		public string DataMemberClass = string.Empty;
	}

	public class WCFDataAccessConfigBase
	{
		public string Name = string.Empty;
		public string Type = string.Empty;
		public string Namespace = string.Empty;
		public string ServiceClass = string.Empty;
		public string Dll = string.Empty;
	}

}
