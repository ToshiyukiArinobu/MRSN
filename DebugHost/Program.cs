using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using WcfServiceHost;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// アプリケーションエントリポイントを持つクラスです。
    /// </summary>
    /// <remarks>
    /// このサンプルでは、WCFの最も基本的な処理を記述しています。
    /// また、本サンプルでは必要な処理部分を全てコードで記述しています。
    /// </remarks>
    class Program
    {
		private const string SERVICE_BASE_ADDRESS = @"https://localhost/WCFLIB/";
		private const string ENDPOINT_ADDRESS = "WcfServiceHost.KESService.svc";

        /// <summary>
        /// アプリケーションエントリポイントです。
        /// </summary>
        static void Main()
        {
            //
            // サービスホストの構築.
            //
            // WCFにて、ホスト側は以下の処理を行う必要がある。
            //
            //   1.ServiceHostの構築
            //   2.Endpointの追加
            //   3.ホストのOpen
            //
            // 本サンプルは、自己ホスト形式となっている。
            // 自己ホストとは、一つの処理にてホストの構築とサービスの定義の
            // 両方を行っているものを指す。
            //
            // 通常、WCFでは自己ホスト形式でサービスを作成せず、サービス定義部分は
            // ライブラリとして定義し、ホスト部分は別の処理にて作成する。
            //
            // さらに、エンドポイントアドレスの設定などは、アプリケーション構成ファイルに
            // 設定するのが通例となる。
            //
            using (ServiceHost host = CreateServiceHost())
            {
                //
                // サービスエンドポイントを追加.
                //   サービスは、「ベースアドレス＋サービスエンドポイント」で一意となる。
                // AddServiceEndpointメソッドには、ABCを指定する必要がある。
                //
                // アドレス (A) (ベースアドレスに対する相対パス)
                string           address  = ENDPOINT_ADDRESS;                
                // バインディング (B)
				WSHttpBinding binding = new WSHttpBinding();
                // コントラクト (C)
                Type             contract = typeof(IKESService);

                host.AddServiceEndpoint(contract, binding, address);

                //
                // サービスをオープン.
                //
                host.Open();
                Console.WriteLine("テスト用サービスを開始しました。");
                Console.WriteLine("この画面で何かキーを押すとテスト用サービスを終了します。");
                Console.ReadKey();

                //
                // サービスを終了.
                //
                host.Close();
            }
        }

        /// <summary>
        /// ServiceHostを構築します。
        /// </summary>
        /// <returns>ServiceHostオブジェクト</returns>
        private static ServiceHost CreateServiceHost()
        {
            //
            // アプリケーション構成ファイルを利用せず、ServiceHostを構築する場合は
            // 以下の情報を与える必要がある。
            //
            //    ・サービスの型（インターフェースではなく、実装クラス）
            //    ・ベースアドレス
            //
            Type serviceType = typeof(KESService);
            Uri  baseAddress = new Uri(SERVICE_BASE_ADDRESS);
            
            return new ServiceHost(serviceType, baseAddress);
        }
    }
}
