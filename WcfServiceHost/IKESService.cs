using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KyoeiSystem.Application.WCFService;

namespace WcfServiceHost
{
	// メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IService1" を変更できます。
	[ServiceContract]
	public interface IKESService
	{
		[OperationContract]
		object CallFunction(string fname, string kind, string cstr, string[] ptypes, object[] plist);
	
	}

}
